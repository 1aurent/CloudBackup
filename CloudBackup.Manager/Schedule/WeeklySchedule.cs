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
using System.Windows.Forms;
using CloudBackup.API;

namespace CloudBackup.Manager.Schedule
{
    public partial class WeeklySchedule : UserControl, IScheduleEditor
    {
        public WeeklySchedule()
        {
            InitializeComponent();
        }

        public void LoadJobSchedule(JobSchedule schedule)
        {
            if (schedule==null) return;

            mtbRuntime.Text = schedule.Weekly.RunAt.ToString();

            cbMonday.Checked = schedule.Weekly.JobDays.HasFlag(JobDays.Monday);
            cbTuesday.Checked = schedule.Weekly.JobDays.HasFlag(JobDays.Tuesday);
            cbWednesday.Checked = schedule.Weekly.JobDays.HasFlag(JobDays.Wednesday);
            cbThrusday.Checked = schedule.Weekly.JobDays.HasFlag(JobDays.Thursday);
            cbFriday.Checked = schedule.Weekly.JobDays.HasFlag(JobDays.Friday);
            cbSaturday.Checked = schedule.Weekly.JobDays.HasFlag(JobDays.Saturday);
            cbSunday.Checked = schedule.Weekly.JobDays.HasFlag(JobDays.Sunday);

            mtbEvery.Text = schedule.Weekly.Every.ToString("00");
        }

        public void EnableEdit(bool status)
        {
            mtbRuntime.Enabled =
            cbMonday.Enabled =
            cbTuesday.Enabled =
            cbWednesday.Enabled =
            cbThrusday.Enabled =
            cbFriday.Enabled =
            cbSaturday.Enabled =
            cbSunday.Enabled = 
            mtbEvery.Enabled = status;
        }

        public void UpdateJobSchedule(JobSchedule schedule)
        {
            schedule.Weekly.Every = int.Parse(mtbEvery.Text.Trim());
            schedule.Weekly.RunAt =
                new HourMinute
                {
                    Hour = int.Parse(mtbRuntime.Text.Substring(0, 2)),
                    Minute = int.Parse(mtbRuntime.Text.Substring(3, 2))
                };

            schedule.Weekly.JobDays = 0;

            if(cbMonday.Checked)  schedule.Weekly.JobDays|=JobDays.Monday;
            if(cbTuesday.Checked)  schedule.Weekly.JobDays|=JobDays.Tuesday;
            if(cbWednesday.Checked)  schedule.Weekly.JobDays|=JobDays.Wednesday;
            if(cbThrusday.Checked) schedule.Weekly.JobDays|=JobDays.Thursday;
            if(cbFriday.Checked)  schedule.Weekly.JobDays|=JobDays.Friday;
            if(cbSaturday.Checked)  schedule.Weekly.JobDays|=JobDays.Saturday;
            if(cbSunday.Checked)  schedule.Weekly.JobDays|=JobDays.Sunday;
        }
    }
}
