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
