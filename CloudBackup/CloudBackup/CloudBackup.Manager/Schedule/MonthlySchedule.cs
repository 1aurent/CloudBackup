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
