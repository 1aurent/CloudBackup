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

using CloudBackup.API;
using CloudBackup.Utils;

namespace CloudBackup.Database
{
    public class ScheduleRow
    {
        [ColumnMap("id")]
        public int Uid { get; set; }
        [ColumnMap("name")] 
        public string Name      { get; set; }
        [ColumnMap("description")]
        public string Description { get; set; }
        [ColumnMap("active")]
        public bool Active { get; set; }
    }

    public class BackupReport
    {
        [ColumnMap("runtime")]
        public long Runtime { get; set; }
        [ColumnMap("success")]
        public bool Success { get; set; }
    }

    public interface IJobProxy
    {
        [SqlStatement("SELECT Id JobUID, Name, Active from Schedule")]
        IEnumerator<JobOverview> GetJobOverview();

        [SqlStatement("SELECT id,name,description,active from Schedule where id=@uid")]
        ScheduleRow LoadSchedule(int uid);

        [SqlStatement("SELECT id,name,description,active from Schedule where name=@name")]
        ScheduleRow LoadSchedule(string name);

        [SqlStatement("SELECT id,name,description,active from Schedule where active<>0")]
        IEnumerator<ScheduleRow> LoadAllActiveSchedule();

        [SqlStatement("SELECT ifnull(max(Id),0) from Schedule")]
        long GetJobMaxId();

        [SqlStatement("SELECT runtime,success from BackupReport where sourceSchedule=@uid order by runtime desc")]
        IEnumerator<BackupReport> GetBackupReports(int uid);

        [SqlStatement("SELECT status from BackupReport where sourceSchedule=@uid and runtime=@runtime")]
        string GetBackupReport(int uid, long runtime);

        [SqlStatement("DELETE from Schedule where id=@uid;" +
                      "DELETE from SnapshotFile where sourceSchedule=@uid;" +
                      "DELETE from ArchiveFiles where sourceSchedule=@uid;" +
                      "DELETE from BackupReport where sourceSchedule=@uid;")]
        void DropSchedule(int uid);

        [SqlStatement("DELETE from ArchiveFiles where sourceSchedule=@uid;")]
        void ResetStatus(int uid);

        [SqlStatement("INSERT OR REPLACE INTO Schedule " +
                      "(id,name,description,active) " +
                      "values (@uid,@name,@description,@active)")]
        void InsertSchedule(int uid, string name, string description, bool active);
    }
}
