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

    public class SnapshotFile
    {
        [ColumnMap("id")]
        public long Id { get; set; }
        [ColumnMap("name")]
        public string FileName { get; set; }
    }

    public interface ISnapProxy 
    {
        IDbTransaction Transaction { get; set; }

        [SqlStatement("INSERT INTO SnapshotFile (id,sourceSchedule,name,created) " +
                      "VALUES (@id,@schedule,@name,@created)")]
        void CreateSnapshotFile(long id, int schedule, string name, long created);

        [SqlStatement("SELECT id,name FROM SnapshotFile sf WHERE" +
                      " (SELECT COUNT(*) FROM ArchiveFiles af WHERE sf.id=af.lastSnapshot) = 0")]
        IEnumerator<SnapshotFile> GetSnapshotFileToClear(int schedule);

        [SqlStatement("DELETE FROM SnapshotFile WHERE sourceSchedule=@schedule AND id=@id")]
        void DeleteSnapshotFile(int schedule,long id);

        [SqlStatement("SELECT sourcePath,sourceSchedule,fileSize,created,modified,notedHash " +
                      "FROM ArchiveFiles WHERE sourceSchedule=@schedule AND sourcePath=@path")]
        ArchiveFile FindFile(int schedule, string path);

        [SqlStatement("UPDATE ArchiveFiles SET seen=0 WHERE sourceSchedule=@schedule")]
        void ClearSeenFlags(int schedule);

        [SqlStatement("UPDATE ArchiveFiles SET seen=1 WHERE sourceSchedule=@schedule AND sourcePath=@path")]
        void SetSeenFlags(int schedule, string path);

        [SqlStatement("SELECT sourcePath,fileSize FROM ArchiveFiles WHERE seen=0 AND sourceSchedule=@schedule")]
        IEnumerator<DeleteFile> GetDeletedFiles(int schedule);

        [SqlStatement("DELETE FROM ArchiveFiles WHERE sourceSchedule=@schedule")]
        void ClearAllFiles(int schedule);

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
