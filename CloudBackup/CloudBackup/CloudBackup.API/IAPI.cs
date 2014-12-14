using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudBackup.API
{
    [Serializable]
    public class JobOverview
    {
        public int JobUID { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
    }

// ReSharper disable once InconsistentNaming
    public interface IAPI
    {
        JobOverview[] GetJobs();
        ArchiveJob LoadArchiveJob(int uid);
        ArchiveJob LoadArchiveJob(string name);
        int SaveOrUpdateArchiveJob(ArchiveJob job);
        void DropArchiveJob(int uid);
        void ResetArchiveJob(int uid);
        void RunJobNow(int uid);

        KeyValuePair<string, string>[] GetAllSettings();
        void SaveSetting(string setting, string value);
    }
}
