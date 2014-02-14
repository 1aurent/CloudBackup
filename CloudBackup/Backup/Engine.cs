using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Alphaleonis.Win32.Vss;
using AlphaFS = Alphaleonis.Win32.Filesystem;

namespace CloudBackup.Backup
{
    public class Engine
    {
        static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(Engine));

        public class SnapshotElement
        {
            public string Path;
            public string AlternateFileName;
            public AlphaFS.FileAttributes Attributes;
            public DateTime Created;
            public string FileName;
            public long FileSize;
            public bool IsDirectory;
            public bool IsFile;
            public bool IsMountPoint;
            public bool IsReparsePoint;
            public bool IsSymbolicLink;
            public DateTime LastAccessed;
            public DateTime LastModified;
        }


        public interface IBackup : IDisposable
        {
            string Xml { get; }
            Guid SetId { get; }
            Guid SnapId { get; }
            string SnapshotDeviceObject { get; }

            void CreateShadowCopy();
            string GetSnapshotPath();
            IList<SnapshotElement> GetSnapshotElements();
            
            void DropBackup();
        }


        public string RootPath { get; set; }


        private class BackupProcessor : IBackup
        {
            static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(BackupProcessor));

            readonly string _root, _volRoot;
            readonly IVssImplementation _vss;
            IVssBackupComponents _backup;

            public string Xml { get; private set; }
            public Guid SetId { get; private set; }
            public Guid SnapId { get; private set; }
            public string SnapshotDeviceObject { get; private set; }

            public BackupProcessor(string root)
            {
                log.Info("Creating Backup Component");
                _vss = VssUtils.LoadImplementation();
                _backup = _vss.CreateVssBackupComponents();

                _backup.InitializeForBackup(null);
                _backup.GatherWriterMetadata();
                _backup.SetContext(VssSnapshotContext.Backup);

                _root = root;
                if (!_root.EndsWith("\\")) _root += "\\";

                _volRoot = AlphaFS.Path.GetPathRoot(_root);
                log.DebugFormat("Path actual root is [{0}]",_volRoot);
                if (!_backup.IsVolumeSupported(_volRoot))
                {
                    throw new Exception("Volume îs not supported by the backup API");
                }
            }

            public void ExamineComponents()
            {
                var writerMds = _backup.WriterMetadata;
                foreach (var metadata in writerMds)
                {
                    log.DebugFormat("Writer : {0}", metadata.WriterName);
                    foreach (var comp in metadata.Components)
                    {
                        log.DebugFormat(" - Component : {0} [{1}]", comp.ComponentName,comp.Caption);
                        
                        foreach (var file in comp.Files)
                        {
                            log.DebugFormat("  - File : {0}\\{1}", file.Path,file.FileSpecification);

                            var targetPath = Environment.ExpandEnvironmentVariables(file.Path);
                            if (!targetPath.EndsWith("\\")) targetPath += "\\";

                            if (targetPath.StartsWith(_root, StringComparison.InvariantCultureIgnoreCase))
                            {
                                log.InfoFormat("Activate Component {0} [{1}]", comp.ComponentName, comp.Caption);
                                _backup.AddComponent(
                                    metadata.InstanceId,metadata.WriterId,comp.Type,null,comp.ComponentName
                                );
                                break;
                            }
                        }
                    }

                }
                SetId = _backup.StartSnapshotSet();
                log.DebugFormat("Backup Set ID [{0}]", SetId);
                SnapId = _backup.AddToSnapshotSet(_volRoot);
                log.DebugFormat("Read for Snap [{0}]", SnapId);

            }

            public void CreateShadowCopy()
            {
                _backup.SetBackupState(true,false,VssBackupType.Full,false);
                _backup.PrepareForBackup();
                _backup.DoSnapshotSet();

                foreach (var writers in _backup.WriterComponents)
                foreach (var components in writers.Components)
                {
                    log.InfoFormat("TimeStamp : [{0}]", components.BackupStamp);
                    _backup.SetBackupSucceeded(
                        writers.InstanceId,
                        writers.WriterId,
                        components.ComponentType,
                        null,
                        components.ComponentName,
                        true);
                }

                _backup.FreeWriterMetadata();

                Xml = _backup.SaveAsXml();
                log.InfoFormat("Result XML : [{0}]",Xml);
                _backup.BackupComplete();

                var prop = _backup.GetSnapshotProperties(SnapId);
                SnapshotDeviceObject = prop.SnapshotDeviceObject;
                log.InfoFormat("Snap location [{0}]",prop.SnapshotDeviceObject);
            }

            public string GetSnapshotPath()
            {
                var localPath = _root.Substring(_volRoot.Length);
                var slash = new string(Path.DirectorySeparatorChar,1);
                if (!SnapshotDeviceObject.EndsWith(slash) && !localPath.StartsWith(slash))
                    localPath = localPath.Insert(0, slash);
                localPath = localPath.Insert(0, SnapshotDeviceObject);
                if (!localPath.EndsWith(slash)) localPath = localPath + slash;
                return localPath;
            }

            public IList<SnapshotElement> GetSnapshotElements()
            {
                var rootPath = GetSnapshotPath();
                var entryInfos = AlphaFS.Directory.GetFullFileSystemEntries(rootPath, "*.*", SearchOption.AllDirectories);

                return entryInfos.Select(entryInfo => new SnapshotElement
                {
                    Path = entryInfo.FullPath.Substring(rootPath.Length) + (entryInfo.IsDirectory?"\\":""), 
                    AlternateFileName   = entryInfo.AlternateFileName, 
                    Attributes          = entryInfo.Attributes, 
                    Created             = entryInfo.Created, 
                    FileName            = entryInfo.FileName, 
                    FileSize            = entryInfo.FileSize, 
                    IsDirectory         = entryInfo.IsDirectory, 
                    IsFile              = entryInfo.IsFile, 
                    IsMountPoint        = entryInfo.IsMountPoint, 
                    IsReparsePoint      = entryInfo.IsReparsePoint, 
                    IsSymbolicLink      = entryInfo.IsSymbolicLink, 
                    LastAccessed        = entryInfo.LastAccessed, 
                    LastModified        = entryInfo.LastModified
                }).ToList();
            }

            public void Dispose()
            {
                if (_backup != null)
                {
                    foreach (var writers in _backup.WriterComponents)
                        foreach (var components in writers.Components)
                        {
                            _backup.SetBackupSucceeded(
                                writers.InstanceId,
                                writers.WriterId,
                                components.ComponentType,
                                null,
                                components.ComponentName,
                                false);
                        }
                    _backup.FreeWriterMetadata();

                    _backup.Dispose(); 
                    _backup = null;
                }

            }
            public void DropBackup()
            {
                _backup.DeleteSnapshotSet(SetId, true);
            }
        }

        public IBackup CreateBackup()
        {
            log.Info("Preparing Backup");
            var backup = new BackupProcessor(RootPath);
            backup.ExamineComponents();
            return backup;
        }

        public void DeleteBackup(Guid setId)
        {
            log.Info("Creating Backup Component");
            var vss = VssUtils.LoadImplementation();
            var backup = vss.CreateVssBackupComponents();

            backup.InitializeForBackup(null);
            backup.GatherWriterMetadata();
            backup.SetContext(VssSnapshotContext.All);
            backup.DeleteSnapshotSet(setId, true);
        }
    }
}

