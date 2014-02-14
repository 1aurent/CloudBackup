using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudBackup.Utils;

namespace CloudBackup.Database
{
    public class ArchiveFile
    {
        [ColumnMap("sourcePath")]
        public string SourcePath { get; set; }
        [ColumnMap("sourceSchedule")]
        public int Schedule { get; set; }
        [ColumnMap("fileSize")]
        public long FileSize { get; set; }
        [ColumnMap("created")]
        public long Created { get; set; }
        [ColumnMap("modified")]
        public long Modified { get; set; }
        [ColumnMap("notedHash")]
        public long Hash { get; set; }
    }

    public class DeleteFile
    {
        [ColumnMap("sourcePath")]
        public string SourcePath { get; set; }
        [ColumnMap("fileSize")]
        public long FileSize { get; set; }
    }


    public interface ISnapProxy 
    {
        IDbTransaction Transaction { get; set; }

        [SqlStatement("INSERT INTO SnapshotFile (id,sourceSchedule,name,created) " +
                      "VALUES (@id,@schedule,@name,@created)")]
        void CreateSnapshotFile(long id, int schedule, string name, long created);

        [SqlStatement("SELECT sourcePath,sourceSchedule,fileSize,created,modified,notedHash " +
                      "FROM ArchiveFiles WHERE sourceSchedule=@schedule AND sourcePath=@path")]
        ArchiveFile FindFile(int schedule, string path);

        [SqlStatement("UPDATE ArchiveFiles SET seen=0 WHERE sourceSchedule=@schedule")]
        void ClearSeenFlags(int schedule);

        [SqlStatement("UPDATE ArchiveFiles SET seen=1 WHERE sourceSchedule=@schedule AND sourcePath=@path")]
        void SetSeenFlags(int schedule, string path);

        [SqlStatement("SELECT sourcePath,fileSize FROM ArchiveFiles WHERE seen=0 AND sourceSchedule=@schedule")]
        IEnumerator<DeleteFile> GetDeletedFiles(int schedule);

        [SqlStatement("DELETE FROM ArchiveFiles WHERE seen=0 AND sourceSchedule=@schedule")]
        void ClearDeleteFiles(int schedule);
            
        [SqlStatement("INSERT INTO ArchiveFiles (sourcePath,sourceSchedule,fileSize,created,modified,notedHash,lastSnapshot,seen) " +
                      "VALUES (@sourcePath,@schedule,@fileSize,@created,@modified,@hash,@lastSnapshot,1)")]
        void AddFile(string sourcePath, int schedule, long fileSize,long created, long modified, long hash, long lastSnapshot);

        [SqlStatement("UPDATE ArchiveFiles SET fileSize=@fileSize,created=@created,modified=@modified,notedHash=@hash,lastSnapshot=@lastSnapshot,seen=1 " +
                      "WHERE sourcePath=@sourcePath AND sourceSchedule=@schedule")]
        void UpdateFile(string sourcePath, int schedule, long fileSize,long created, long modified, long hash, long lastSnapshot);


    }
}
