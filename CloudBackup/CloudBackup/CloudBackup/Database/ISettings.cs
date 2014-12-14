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

        [SqlStatement("INSERT OR REPLACE INTO Settings (id,value) VALUES (@setting,@value)")]
        void Set(string setting,string value);

        [SqlStatement("SELECT value FROM Settings WHERE id=@setting")]
        string GetSetting(string setting);
    }
}
