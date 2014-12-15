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
    }
}
