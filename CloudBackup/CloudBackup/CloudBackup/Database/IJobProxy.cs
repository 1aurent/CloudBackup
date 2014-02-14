using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        [ColumnMap("schedule")]
        public string Schedule { get; set; }
        [ColumnMap("rootPath")]
        public string RootPath { get; set; }
        [ColumnMap("active")]
        public bool Active { get; set; }
    }

    public interface IJobProxy
    {
        [SqlStatement("SELECT Id JobUID, Name, Active from Schedule")]
        IEnumerator<JobOverview> GetJobOverview();

        [SqlStatement("SELECT id,name,schedule,rootPath,active from Schedule where id=@uid")]
        ScheduleRow LoadSchedule(int uid);

        [SqlStatement("SELECT id,name,schedule,rootPath,active from Schedule where name=@name")]
        ScheduleRow LoadSchedule(string name);

        [SqlStatement("SELECT id,name,schedule,rootPath,active from Schedule where active<>0")]
        IEnumerator<ScheduleRow> LoadAllActiveSchedule();

        [SqlStatement("SELECT ifnull(max(Id),0) from Schedule")]
        long GetJobMaxId();

        [SqlStatement("DELETE from Schedule where id=@uid;" +
                      "DELETE from SnapshotFile where sourceSchedule=@uid;" +
                      "DELETE from ArchiveFiles where sourceSchedule=@uid;")]
        void DropSchedule(int uid);

        [SqlStatement("DELETE FROM Schedule WHERE id=@uid;" +
                      "INSERT INTO Schedule " +
                      "(id,name,schedule,rootPath,active) " +
                      "values (@uid,@name,@schedule,@rootPath,@active)")]
        void InsertSchedule(int uid, string name, string schedule, string rootPath, bool active);
    }
}
