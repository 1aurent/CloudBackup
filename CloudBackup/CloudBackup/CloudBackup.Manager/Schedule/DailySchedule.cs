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
using System.Windows.Forms;
using CloudBackup.API;

namespace CloudBackup.Manager.Schedule
{
    public partial class DailySchedule : UserControl, IScheduleEditor
    {
        public DailySchedule()
        {
            InitializeComponent();
        }

        public void LoadJobSchedule(JobSchedule schedule)
        {
            if (schedule == null) return;

            mtbRuntime.Text = schedule.Daily.RunAt.ToString();
            mtbEvery.Text = schedule.Daily.Every.ToString("00");
        }

        public void UpdateJobSchedule(JobSchedule schedule)
        {
            schedule.Daily.Every = int.Parse(mtbEvery.Text.Trim());
            schedule.Daily.RunAt =
                new HourMinute
                {
                    Hour = int.Parse(mtbRuntime.Text.Substring(0, 2)),
                    Minute = int.Parse(mtbRuntime.Text.Substring(3, 2))
                };
        }

        public void EnableEdit(bool status)
        {
            mtbRuntime.Enabled =
            mtbEvery.Enabled = status;
        }
    }
}
