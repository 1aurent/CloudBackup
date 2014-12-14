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
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CloudBackup.API;

namespace CloudBackup.Manager.Schedule
{
    public partial class MonthlySchedule : UserControl, IScheduleEditor
    {
        public MonthlySchedule()
        {
            InitializeComponent();
        }

        public void LoadJobSchedule(JobSchedule schedule)
        {
            if (schedule == null) return;

            mtbRuntime.Text = schedule.Monthly.RunAt.ToString();
            mtbEvery.Text = schedule.Monthly.Every.ToString("00");
            mtbDay.Text = schedule.Monthly.DayOffset.ToString("00");

            rbFromBegin.Checked = schedule.Monthly.RelativeTo == JobRelativeTo.BeginOfTheMonth;
            rbFromEnd.Checked = schedule.Monthly.RelativeTo == JobRelativeTo.EndOfTheMonth;
            rbFromSunday.Checked = schedule.Monthly.RelativeTo == JobRelativeTo.FirstSundayOfTheMonth;
        }

        public void EnableEdit(bool status)
        {
            mtbRuntime.Enabled =
            rbFromBegin.Enabled =
            rbFromEnd.Enabled =
            rbFromSunday.Enabled =
            mtbDay.Enabled =
            mtbEvery.Enabled = status;
        }

        public void UpdateJobSchedule(JobSchedule schedule)
        {
            schedule.Monthly.Every = int.Parse(mtbEvery.Text.Trim());
            schedule.Monthly.DayOffset = int.Parse(mtbDay.Text.Trim());
            schedule.Monthly.RunAt =
                new HourMinute
                {
                    Hour = int.Parse(mtbRuntime.Text.Substring(0, 2)),
                    Minute = int.Parse(mtbRuntime.Text.Substring(3, 2))
                };

            if(rbFromBegin.Checked)         schedule.Monthly.RelativeTo = JobRelativeTo.BeginOfTheMonth;
            else if(rbFromEnd.Checked)      schedule.Monthly.RelativeTo = JobRelativeTo.EndOfTheMonth;
            else if(rbFromSunday.Checked)   schedule.Monthly.RelativeTo = JobRelativeTo.FirstSundayOfTheMonth;
        }
    }
}
