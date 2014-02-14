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
