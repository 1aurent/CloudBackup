using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudBackup.Utils;

namespace CloudBackup.Database
{
    public class AllSettings
    {
        [ColumnMap("id")]
        public string Setting { get; set; }
        [ColumnMap("value")]
        public string Value { get; set; }
    }

    public interface ISettings
    {
        [SqlStatement("SELECT id,value FROM Settings")]
        IEnumerator<AllSettings> GetAllSettings();

        [SqlStatement("INSERT OR REPLACE Settings (id,value) VALUES (@setting,@value)")]
        void Set(string setting,string value);

        [SqlStatement("SELECT value FROM Settings WHERE id=@setting")]
        string GetSetting(string setting);
    }
}
