/* ........................................................................
 * copyright 2015 Laurent Dupuis
 * ........................................................................
 * < This program is free software: you can redistribute it and/or modify
 * < it under the terms of the GNU General Public License as published by
 * < the Free Software Foundation, either version 3 of the License, or
 * < (at your option) any later version.
 * < 
 * < This program is distributed in the hope that it will be useful,
 * < but WITHOUT ANY WARRANTY; without even the implied warranty of
 * < MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * < GNU General Public License for more details.
 * < 
 * < You should have received a copy of the GNU General Public License
 * < along with this program.  If not, see <http://www.gnu.org/licenses/>.
 * ........................................................................
 *
 */
using System;
using System.IO;
using System.Threading;
using System.Xml;
using CloudBackup.API;
using CloudBackup.Backend;
using CloudBackup.Utils;
using Ionic.Zip;
using Ionic.Zlib;
using AlphaFS = Alphaleonis.Win32.Filesystem;

namespace CloudBackup.Backup
{
    public class Process
    {
        static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(Process));

        class SnapshotAccess
        {
            static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(SnapshotAccess));
            readonly string _snapPath;
            public SnapshotAccess(string snapPath) { _snapPath = snapPath; }

            public Stream OpenDelegate(string entryName)
            {
                log.DebugFormat("SnapshotAccess Opening[{0}]",_snapPath);
                return AlphaFS.File.OpenRead(_snapPath);
            }

            public void CloseDelegate(string entryName, Stream stream)
            {
                //log.DebugFormat("SnapshotAccess Close[{0}]", _snapPath);
            }
        }

        class ManifestDocument
        {
            readonly XmlDocument _manifest;
            readonly XmlElement _tagsRoot;
            readonly ArchiveJob _job;

            public ManifestDocument(ArchiveJob job,long jobId)
            {
                _manifest = new XmlDocument();
                _manifest.LoadXml(string.Format("<manifest id=\"{0}\" jid=\"{1}\" />", jobId, job.JobUID.Value));
                _job = job;
                _tagsRoot = _manifest.CreateElement("tags");
                _manifest.DocumentElement.AppendChild(_tagsRoot);   
            }

            public void AddTag(string name, string value)
            {
                var xmlEm = _manifest.CreateElement("tag");       
                var nmeAttr = _manifest.CreateAttribute("name");
                xmlEm.InnerText = value;
                nmeAttr.InnerText = value;
                xmlEm.Attributes.SetNamedItem(nmeAttr);
                _tagsRoot.AppendChild(xmlEm);       
            }

            public void NotifyDelFile(string path, long fileSize)
            {
                var xmlEm = _manifest.CreateElement("delFile");
                xmlEm.InnerText = path;
                var szAttr = _manifest.CreateAttribute("size");
                szAttr.InnerText = fileSize.ToString();
                xmlEm.Attributes.SetNamedItem(szAttr);
                _manifest.DocumentElement.AppendChild(xmlEm);                
            }

            public string Manifest { get { return _manifest.OuterXml; }}
        }

        class SshSinkParams
        {
            public Backend.Backend Target;
            public Stream Stream;
            public string File;
            public bool IsSuccess;
            public Exception Exception;
        }

        static void SshSinkThread(object rawparams)
        {
            var @params = (SshSinkParams)rawparams;
            try
            {
                @params.Target.Upload(@params.File, @params.Stream);
                @params.IsSuccess = true;
            }
            catch (Exception ex)
            {
                log.Fatal("Unable to send message",ex);
                @params.IsSuccess = false;
                @params.Exception = ex;
            }
        }


        public static void RunBackup(ArchiveJob job)
        {
            log.InfoFormat("Starting backup of [{0}]", job.JobRootPath);
            var engine = new Engine {RootPath = job.JobRootPath};


            var now = DateTime.Now;
            var jid = job.JobUID.Value;
            var id = now.ToUniversalTime().Ticks;
            var fle = string.Format("{0:00000}_Archive_{1:0000}{2:00}{3:00}_{4:X}.zip", jid, now.Year, now.Month,
                now.Day, id);

            var proxy = Program.Database.CreateSnapProxy();
            proxy.CreateSnapshotFile(id, jid, fle, id);
            proxy.ClearSeenFlags(jid);

            using (var backend = Backend.Backend.OpenBackend(job.JobTarget))
            {
                using (var zip = new ZipFile())
                {
                    zip.CompressionLevel = CompressionLevel.BestCompression;
                    zip.SaveProgress += zip_SaveProgress;
                    zip.MaxOutputSegmentSize = 10*1024*1024;

                    log.Debug("Building backup snapshot");

                    var manifest = new ManifestDocument(job, id);
                    manifest.AddTag("date", XmlConvert.ToString(now, XmlDateTimeSerializationMode.Utc));
                    manifest.AddTag("source", Environment.MachineName);
                    manifest.AddTag("from", job.JobRootPath);


                    using (var backup = engine.CreateBackup())
                    {
                        backup.CreateShadowCopy();
                        var rootPath = backup.GetSnapshotPath();

                        var elements = backup.GetSnapshotElements();

                        foreach (var element in elements)
                        {
                            var file = proxy.FindFile(jid, element.Path);
                            if (element.IsDirectory)
                            {
                                log.InfoFormat("[{0}] - Directory - Add entry", element.Path);
                                var entry = zip.AddDirectoryByName(element.Path);
                                entry.CreationTime = element.Created;
                                entry.AccessedTime = element.LastAccessed;
                                entry.ModifiedTime = element.LastModified;

                                if (file != null)
                                    proxy.SetSeenFlags(jid, element.Path);
                                else
                                    proxy.AddFile(
                                        element.Path, jid,
                                        element.FileSize,
                                        element.Created.Ticks,
                                        element.LastModified.Ticks,
                                        0, id
                                        );

                                continue;
                            }

                            if (file == null)
                            {
                                log.InfoFormat("[{0}] - New file / Archive", element.Path);
                                var entryDoc = new SnapshotAccess(rootPath + element.Path);
                                var entry = zip.AddEntry(element.Path, entryDoc.OpenDelegate, entryDoc.CloseDelegate);
                                entry.CreationTime = element.Created;
                                entry.AccessedTime = element.LastAccessed;
                                entry.ModifiedTime = element.LastModified;

                                proxy.AddFile(
                                    element.Path, jid,
                                    element.FileSize,
                                    element.Created.Ticks,
                                    element.LastModified.Ticks,
                                    entry.Crc,
                                    id
                                    );
                                continue;
                            }

                            if (
                                file.Modified == element.LastModified.Ticks &&
                                file.Created == element.Created.Ticks &&
                                file.FileSize == element.FileSize
                                )
                            {
                                log.InfoFormat("[{0}] - No change / No archive", element.Path);
                                proxy.SetSeenFlags(jid, element.Path);
                                continue;
                            }

                            log.InfoFormat("[{0}] - File changed / Archiving", element.Path);
                            var entryDoc2 = new SnapshotAccess(rootPath + element.Path);
                            var entry2 = zip.AddEntry(element.Path, entryDoc2.OpenDelegate, entryDoc2.CloseDelegate);
                            entry2.CreationTime = element.Created;
                            entry2.AccessedTime = element.LastAccessed;
                            entry2.ModifiedTime = element.LastModified;

                            proxy.UpdateFile(
                                element.Path, jid,
                                element.FileSize,
                                element.Created.Ticks,
                                element.LastModified.Ticks,
                                entry2.Crc,
                                id
                                );
                        }

                        using (var delFiles = proxy.GetDeletedFiles(jid))
                        {
                            while (delFiles.MoveNext())
                            {
                                manifest.NotifyDelFile(delFiles.Current.SourcePath, delFiles.Current.FileSize);
                            }
                        }

                        zip.Comment = manifest.Manifest;

                        log.InfoFormat("Process completed - Streaming ZIP file");

                        bool transfertIsSuccess;
                        Exception transferException;
                        if (backend is FTP)
                        {
                            try
                            {
                                using (var sendStream = ((FTP)backend).GetWriteStream(fle))
                                {
                                    zip.Save(sendStream);
                                }
                                transferException = null;
                                transfertIsSuccess = true;
                            }
                            catch (Exception ex)
                            {
                                transferException = ex;
                                transfertIsSuccess = false;
                            }
                        }
                        else
                        {
                            Stream write, read;
                            PipeStream.CreatePipe(out write, out read);

                            var sshThread = new Thread(SshSinkThread);
                            var param = new SshSinkParams { Target = backend, Stream = read, File = fle };
                            sshThread.Start(param);
                            zip.Save(write);
                            write.Dispose();
                            log.InfoFormat("Process completed - Joining");
                            sshThread.Join();

                            transfertIsSuccess = param.IsSuccess;
                            transferException = param.Exception;
                        }
                        log.InfoFormat("Process completed - Droping backup");
                        backup.DropBackup();

                        if (!transfertIsSuccess)
                        {
                            log.ErrorFormat("File transfert is not successfull");
                            throw new Exception("Transfert failed", transferException);
                        }
                    }
                }
            }
            log.InfoFormat("Process completed - Commit Transaction");
            proxy.Transaction.Commit();
        }

        static void zip_SaveProgress(object sender, SaveProgressEventArgs e)
        {
            if(e.EventType==ZipProgressEventType.Saving_AfterRenameTempArchive)
                log.InfoFormat("Zip Save [{0}]",e.ArchiveName);
        }
    }
}
