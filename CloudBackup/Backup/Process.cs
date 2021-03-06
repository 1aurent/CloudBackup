﻿/* ........................................................................
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
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Xml;
using CloudBackup.API;
using CloudBackup.Backend;
using CloudBackup.Utils;
using Ionic.Zip;
using Ionic.Zlib;
using AlphaFS = Alphaleonis.Win32.Filesystem;
using System.Text;

namespace CloudBackup.Backup
{
    public class Process : IDisposable
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
                xmlEm.InnerText = name;
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
            public void NotifyEmptyFile(string path)
            {
                var xmlEm = _manifest.CreateElement("emptyFile");
                xmlEm.InnerText = path;
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

        Process(ArchiveJob job, bool forceFullBackup,string reason)
        {
            _backupReason = reason;
            _forceFullBackup = forceFullBackup;
            _job = job;
            _now = DateTime.Now;
            var jid = job.JobUID.Value;
            _id = _now.ToUniversalTime().Ticks;
            _fileName = string.Format("BACKUP_{0:00000}_{8}_{1:0000}{2:00}{3:00}_{4:00}{5:00}{6:00}_{7:000}.{9}.zip", jid, _now.Year, _now.Month,
                _now.Day, _now.Hour,_now.Minute,_now.Second,
                _now.Millisecond,
                forceFullBackup?"FULL":"INCR",
                Environment.MachineName);

            _report = new StringBuilder();
            _report.AppendLine("Target filename: " + _fileName);

            _proxy = Program.Database.CreateSnapProxy();
            _proxy.CreateSnapshotFile(_id, jid, _fileName, _id);
            _proxy.ClearSeenFlags(jid);
        }

        void BuildConnectBackend()
        {
            _report.AppendLine("Connect to target: " + _job.JobTarget.TargetServer);
            _backend = Backend.Backend.OpenBackend(_job.JobTarget);
            _zipFile = new ZipFile()
            {
                CompressionLevel = CompressionLevel.BestCompression,
                UseZip64WhenSaving = Zip64Option.Always
            };

            if (!string.IsNullOrWhiteSpace(_job.JobTarget.ZipPassword))
            {
                _zipFile.Password = _job.JobTarget.ZipPassword;
                _zipFile.Encryption = EncryptionAlgorithm.WinZipAes256;
            }

            _zipFile.SaveProgress += zip_SaveProgress;            
        }

        readonly ArchiveJob _job;
        readonly string _fileName;
        readonly DateTime _now;
        readonly long _id;
        readonly bool _forceFullBackup;
        readonly string _backupReason;

        Database.ISnapProxy _proxy;
        Engine.IBackup _backup;
        Backend.Backend _backend;
        ZipFile _zipFile;

        StringBuilder _report;


        public void Dispose()
        {
            if(_zipFile!=null) { _zipFile.Dispose(); _zipFile=null;}
            if(_backend!=null) { _backend.Dispose(); _backend=null;}
            if(_backup!=null) { _backup.Dispose(); _backup=null;}
        }

        private void BuildSnapshot(Engine.IBackup backup)
        {
            log.Debug("Building backup snapshot");

            var manifest = new ManifestDocument(_job, _id);
            manifest.AddTag("date", XmlConvert.ToString(_now, XmlDateTimeSerializationMode.Utc));
            manifest.AddTag("source", Environment.MachineName);
            manifest.AddTag("from", _job.JobRootPath);
            manifest.AddTag("fullBackup", XmlConvert.ToString(_forceFullBackup));

            var jid = _job.JobUID.Value;

            if (_forceFullBackup)
            {
                _report.AppendLine("Note: Running a full backup");
                _proxy.ClearAllFiles(jid);
            }


            backup.CreateShadowCopy();
            var rootPath = backup.GetSnapshotPath();
            var elements = backup.GetSnapshotElements();

            _report.AppendLine();
            _report.AppendLine(" * Backup process :");
            _report.AppendLine();

            var countNewFiles = 0L;
            var countChangeFiles = 0L;
            var countDeletedFiles = 0L;

            var hash = 0L;
            foreach (var element in elements)
            {
                var file = _proxy.FindFile(jid, element.Path);
                if (element.IsDirectory)
                {
                    log.InfoFormat("[{0}] - Directory - Add entry", element.Path);
                    var entry = _zipFile.AddDirectoryByName(element.Path);
                    entry.CreationTime = element.Created;
                    entry.AccessedTime = element.LastAccessed;
                    entry.ModifiedTime = element.LastModified;

                    if (file != null)
                        _proxy.SetSeenFlags(jid, element.Path);
                    else
                        _proxy.AddFile(
                            element.Path, jid,
                            element.FileSize,
                            element.Created.Ticks,
                            element.LastModified.Ticks,
                            0, _id
                            );

                    continue;
                }

                if (file == null)
                {
                    log.InfoFormat("[{0}] - New file / Archive", element.Path);
                    if (element.FileSize != 0)
                    {
                        var entryDoc = new SnapshotAccess(rootPath + element.Path);
                        var entry = _zipFile.AddEntry(element.Path, entryDoc.OpenDelegate, entryDoc.CloseDelegate);
                        entry.CreationTime = element.Created;
                        entry.AccessedTime = element.LastAccessed;
                        entry.ModifiedTime = element.LastModified;
                        hash = entry.Crc;
                    }
                    else
                    {
                        hash = 0L;
                        manifest.NotifyEmptyFile(element.Path);
                    }
                        

                    _report.AppendLine("Backup new file " + element.Path);
                    ++countNewFiles;
                    _proxy.AddFile(
                        element.Path, jid,
                        element.FileSize,
                        element.Created.Ticks,
                        element.LastModified.Ticks,
                        hash,
                        _id
                        );
                    continue;
                }

                if (
                    file.Modified == element.LastModified.Ticks &&
                    file.Created == element.Created.Ticks &&
                    file.FileSize == element.FileSize
                    )
                {
                    log.DebugFormat("[{0}] - No change / No archive", element.Path);
                    _proxy.SetSeenFlags(jid, element.Path);
                    continue;
                }

                log.InfoFormat("[{0}] - File changed / Archiving", element.Path);
                _report.AppendLine("Backup modified file " + element.Path);
                ++countChangeFiles;

                if (element.FileSize != 0)
                {
                    var entryDoc2 = new SnapshotAccess(rootPath + element.Path);
                    var entry2 = _zipFile.AddEntry(element.Path, entryDoc2.OpenDelegate, entryDoc2.CloseDelegate);
                    entry2.CreationTime = element.Created;
                    entry2.AccessedTime = element.LastAccessed;
                    entry2.ModifiedTime = element.LastModified;
                    hash = entry2.Crc;
                }
                else
                {
                    hash = 0L;
                    manifest.NotifyEmptyFile(element.Path);
                }

                _proxy.UpdateFile(
                    element.Path, jid,
                    element.FileSize,
                    element.Created.Ticks,
                    element.LastModified.Ticks,
                    hash,
                    _id
                    );
            }

            using (var delFiles = _proxy.GetDeletedFiles(jid))
            {
                while (delFiles.MoveNext())
                {
                    _report.AppendLine("Deleted file " + delFiles.Current.SourcePath);
                    ++countDeletedFiles;
                    manifest.NotifyDelFile(delFiles.Current.SourcePath, delFiles.Current.FileSize);
                }
            }

            _report.AppendLine();
            _report.AppendLine(string.Format(" * Report: New files: {0} / Modified files: {1} / Deleted files: {2}", countNewFiles, countChangeFiles, countDeletedFiles));

            _zipFile.Comment = manifest.Manifest;
        }

        void PublishZipFile(Engine.IBackup backup)
        {
            log.InfoFormat("Process completed - Streaming ZIP file");

            _report.AppendLine();
            _report.AppendLine(" * Streaming ZIP file to target");

            var swTimer = new Stopwatch();
            swTimer.Start();

            bool transfertIsSuccess;
            Exception transferException;
            if (_backend is FTP)
            {
                try
                {
                    using (var sendStream = ((FTP)_backend).GetWriteStream(_fileName))
                    {
                        _zipFile.Save(sendStream);
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
                var param = new SshSinkParams { Target = _backend, Stream = read, File = _fileName };
                sshThread.Start(param);
                _zipFile.Save(write);
                write.Dispose();
                log.InfoFormat("Process completed - Joining");
                sshThread.Join();

                transfertIsSuccess = param.IsSuccess;
                transferException = param.Exception;
            }

            swTimer.Stop();

            log.InfoFormat("Process completed in {0} - Droping backup", swTimer.Elapsed);
            _report.AppendLine("   Total time for transfert : " + swTimer.Elapsed);
            backup.DropBackup();

            if (!transfertIsSuccess)
            {
                log.ErrorFormat("File transfert is not successfull");
                throw new Exception("Transfert failed", transferException);
            }
        }

        void CommitTransaction()
        {
            var jid = _job.JobUID.Value;
            _proxy.ClearDeleteFiles(jid);

            _report.AppendLine();
            _report.AppendLine("** Backup completed with success **");
            _report.AppendLine();
            _proxy.BackupReport(_job.JobUID.Value, _now.Ticks, true, _backupReason, _report.ToString());

            _proxy.Transaction.Commit();
            _proxy = null;
        }

        void ClearUnneededFiles()
        {
            log.InfoFormat("Post-Process - Removing unneeded files");

            var jid = _job.JobUID.Value;
            var proxy = Program.Database.CreateSnapProxy();
            try
            {
                var list = new List<long>();
                var unneededFiles = proxy.GetSnapshotFileToClear(jid);
                while (unneededFiles.MoveNext())
                {
                    var fileToClear = unneededFiles.Current;
                    log.InfoFormat("Clearing unneeded file [{0}]",fileToClear.FileName);

                    try
                    {
                        _backend.Delete(fileToClear.FileName);
                        list.Add(fileToClear.Id);
                    }
                    catch (Exception ex)
                    {
                        log.ErrorFormat("Unable to clear file {0} : {1}",fileToClear,ex);
                    }
                }

                foreach (var id in list)
                {
                    proxy.DeleteSnapshotFile(jid,id);
                }
            }
            catch (Exception ex)
            {
                log.Error("ClearUnneededFiles failed",ex);
            }
            finally
            {
                proxy.Transaction.Commit();
            }

        }

        public static void RunBackup(ArchiveJob job,bool forceFullBackup, string reason)
        {
            log.InfoFormat("Starting backup of [{0}]", job.JobRootPath);

            using (var process = new Process(job, forceFullBackup, reason))
            {
                try
                {
                    process.BuildConnectBackend();

                    var engine = new Engine { RootPath = job.JobRootPath };
                    using (var backup = engine.CreateBackup())
                    {
                        //- First create a snapshot
                        process.BuildSnapshot(backup);

                        //- Publish ZIP file
                        process.PublishZipFile(backup);
                    }

                    log.InfoFormat("Process completed - Commit Transaction");
                    process.CommitTransaction();

                    // Post process
                    if (job.JobTarget.ManageTargetFiles)
                    {
                        process.ClearUnneededFiles();
                    }
                }
                catch(Exception ex)
                {
                    process._proxy.Transaction.Rollback();
                    process._proxy = null;

                    log.Error("Process failed ! - Rollback Transaction", ex);
                    process._report.AppendLine("** FAILED ** Exception: " + ex);

                    var proxy = Program.Database.CreateSnapProxy();
                    proxy.BackupReport(job.JobUID.Value, process._now.Ticks, false,reason, process._report.ToString());
                    proxy.Transaction.Commit();
                }
            }
        }

        static void zip_SaveProgress(object sender, SaveProgressEventArgs e)
        {
            if(e.EventType==ZipProgressEventType.Saving_AfterRenameTempArchive)
            {
                log.InfoFormat("Zip Save [{0}]", e.ArchiveName);
            }
                
        }

    }
}
