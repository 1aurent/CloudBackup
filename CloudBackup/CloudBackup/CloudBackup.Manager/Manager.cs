using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CloudBackup.API;
using CloudBackup.Manager.Schedule;
using DailySchedule = CloudBackup.Manager.Schedule.DailySchedule;
using MonthlySchedule = CloudBackup.Manager.Schedule.MonthlySchedule;
using WeeklySchedule = CloudBackup.Manager.Schedule.WeeklySchedule;

namespace CloudBackup.Manager
{
    public partial class Manager : Form
    {
        IScheduleEditor _current;
        JobSchedule _schedule;
        int _scheduleItemCurSel;
        ArchiveJob _archiveJob;
        bool _suspendJobCheckEvents;

        IAPI _serverLink;

        public Manager()
        {
            InitializeComponent();

            tbArchiveJobName.Enabled = false;
            tbRootFolder.Enabled = false;
            cbAllSchedules.Enabled = false;
            btnNewSchedule.Enabled = false;
            btnDelSchedule.Enabled = false;
            btnSaveSchedule.Enabled = false;
            btnRunNow.Enabled = false;

            rdSchedDaily.Enabled = false;
            rdSchedWeekly.Enabled = false;
            rdSchedMonthly.Enabled = false;

            var clientChannel = new IpcChannel();
            ChannelServices.RegisterChannel(clientChannel, true);

            _serverLink = (IAPI)Activator.GetObject(typeof(IAPI), "ipc://localhost:19888/API");

            try
            {
                ReloadSettings();
                ReloadJobList();
            }
            catch (RemotingException ex)
            {
                MessageBox.Show(this,
                    "Unable to communicate with the CloudBackup service. Please check that the service is running, close this window and try again",
                    "Unable to access service", MessageBoxButtons.OK);

                btnApplyChanges.Enabled = 
                btnNewArchive.Enabled = 
                txtSshHost.Enabled = 
                txtSshUsername.Enabled = 
                txtSshPwd.Enabled = 
                txtSshRootPath.Enabled = 
                txtMasterPassword.Enabled = 
                cbIsGlacier.Enabled= false;
            }
        }

        void ReloadJobList()
        {
            lbAllSchedule.Items.Clear();
            foreach (var job in _serverLink.GetJobs())
            {
                lbAllSchedule.Items.Add(job.Name);
            }            
        }

        void SetSetting(Dictionary<string,string> settings,
            string setting, TextBox box, string @default)
        {
            string value;
            if (!settings.TryGetValue(setting, out value)) value = @default;
            box.Text = value;
        }

        void ReloadSettings()
        {
            var settings = _serverLink.GetAllSettings()
                .ToDictionary(x => x.Key, x => x.Value, StringComparer.InvariantCultureIgnoreCase);

            SetSetting(settings, "SshHost", txtSshHost, "changeme");
            SetSetting(settings, "SshUser", txtSshUsername, "changeme");
            SetSetting(settings, "SshPwd", txtSshPwd, "changeme");
            SetSetting(settings, "SshPath", txtSshRootPath, "/home/changeme/");
            SetSetting(settings, "ZipPwd", txtMasterPassword, "changeme");

            string isGlacierStr;
            if (!settings.TryGetValue("IsGlacier", out isGlacierStr)) isGlacierStr = "False";
            bool isGlacier;
            if (!bool.TryParse(isGlacierStr, out isGlacier)) isGlacier = false;
            cbIsGlacier.Checked = isGlacier;
        }

        private void btnApplyChanges_Click(object sender, EventArgs e)
        {
            _serverLink.SaveSetting("SshHost", txtSshHost.Text);
            _serverLink.SaveSetting("SshUser", txtSshUsername.Text);
            _serverLink.SaveSetting("SshPwd", txtSshPwd.Text);
            _serverLink.SaveSetting("SshPath", txtSshRootPath.Text);
            _serverLink.SaveSetting("ZipPwd", txtMasterPassword.Text);
            _serverLink.SaveSetting("IsGlacier", cbIsGlacier.Enabled.ToString());
        }


        private void rdSchedDaily_CheckedChanged(object sender, EventArgs e)
        {
            if (_suspendJobCheckEvents || !rdSchedDaily.Checked) return;
            if (_suspendJobCheckEvents) return;
            if (_current != null && _schedule != null) _current.UpdateJobSchedule(_schedule);

            panelSchedule.Controls.Clear();
            _current = new DailySchedule();
            panelSchedule.Controls.Add((UserControl)_current);

            var itm = (JobSchedule)cbAllSchedules.SelectedItem;
            itm.ScheduleType = JobScheduleType.Daily;
            _current.LoadJobSchedule(itm);
            _current.EnableEdit(true);
            cbAllSchedules.Items[cbAllSchedules.SelectedIndex] = itm;
        }

        private void rdSchedWeekly_CheckedChanged(object sender, EventArgs e)
        {
            if (_suspendJobCheckEvents || !rdSchedWeekly.Checked) return;
            if (_current != null && _schedule != null) _current.UpdateJobSchedule(_schedule);

            panelSchedule.Controls.Clear();
            _current = new WeeklySchedule();
            panelSchedule.Controls.Add((UserControl)_current);

            var itm = (JobSchedule)cbAllSchedules.SelectedItem;
            itm.ScheduleType = JobScheduleType.Weekly;
            _current.LoadJobSchedule(itm);
            _current.EnableEdit(true);
            cbAllSchedules.Items[cbAllSchedules.SelectedIndex] = itm;
        }

        private void rdSchedMonthly_CheckedChanged(object sender, EventArgs e)
        {
            if (_suspendJobCheckEvents || !rdSchedMonthly.Checked) return;
            if (_current != null && _schedule != null) _current.UpdateJobSchedule(_schedule);

            panelSchedule.Controls.Clear();
            _current = new MonthlySchedule();
            panelSchedule.Controls.Add((UserControl)_current);

            var itm = (JobSchedule)cbAllSchedules.SelectedItem;
            itm.ScheduleType = JobScheduleType.Monthly;
            _current.LoadJobSchedule(itm);
            _current.EnableEdit(true);
            cbAllSchedules.Items[cbAllSchedules.SelectedIndex] = itm;
        }

        private void cbDisplayPasswords_CheckedChanged(object sender, EventArgs e)
        {
            var showText = !cbDisplayPasswords.Checked;
            txtMasterPassword.UseSystemPasswordChar = showText;
            txtSshPwd.UseSystemPasswordChar = showText;
        }

        void SyncGuiWithArchive()
        {
            if (_archiveJob == null)
            {
                tbArchiveJobName.Enabled = 
                tbRootFolder.Enabled = 
                cbAllSchedules.Enabled = 
                btnNewSchedule.Enabled = 
                btnDelSchedule.Enabled = 
                btnSaveSchedule.Enabled = 
                rdSchedDaily.Enabled = 
                rdSchedWeekly.Enabled = 
                rdSchedMonthly.Enabled =
                btnRunNow.Enabled = false;
                rdSchedDaily.Checked = 
                rdSchedWeekly.Checked = 
                rdSchedMonthly.Checked = false;

                tbArchiveJobName.Text =
                    tbRootFolder.Text = "";
                cbAllSchedules.Items.Clear();

                panelSchedule.Controls.Clear();
                return;
            }

            tbArchiveJobName.Enabled = true;
            tbRootFolder.Enabled = true;
            cbAllSchedules.Enabled = true;
            btnNewSchedule.Enabled = true;
            btnDelSchedule.Enabled = true;
            btnSaveSchedule.Enabled = true;
            btnRunNow.Enabled = _archiveJob.JobUID.HasValue;

            rdSchedDaily.Enabled = true;
            rdSchedWeekly.Enabled = true;
            rdSchedMonthly.Enabled = true;
            rdSchedDaily.Checked = false;
            rdSchedWeekly.Checked = false;
            rdSchedMonthly.Checked = false;

            tbArchiveJobName.Text = _archiveJob.UniqueJobName;
            tbRootFolder.Text = _archiveJob.JobRootPath;

            cbAllSchedules.Items.Clear();
            for (var i=0;i<_archiveJob.Schedules.Count;++i)
            {
                cbAllSchedules.Items.Add(_archiveJob.Schedules[i]);
            }
            cbAllSchedules.SelectedIndex = 0;

            if (_archiveJob.Schedules.Count == 1) btnDelSchedule.Enabled = false;
        }

        void SyncArchiveWithGui()
        {
            if (_current != null && _schedule != null) _current.UpdateJobSchedule(_schedule);
            _archiveJob.UniqueJobName = tbArchiveJobName.Text;
            _archiveJob.JobRootPath = tbRootFolder.Text;
            _archiveJob.Schedules.Clear();
            foreach (var item in cbAllSchedules.Items)
            {
                _archiveJob.Schedules.Add((JobSchedule)item);
            }
        }

        private void btnNewArchive_Click(object sender, EventArgs e)
        {
            lbAllSchedule.SelectedItem = null;
            _suspendJobCheckEvents = true;
            _archiveJob = new ArchiveJob();
            _schedule = null;
            btnDelSchedule.Enabled = false;
            lbAllSchedule.SelectedItem = null;
            _suspendJobCheckEvents = false;
            SyncGuiWithArchive();
        }

        private void btnNewSchedule_Click(object sender, EventArgs e)
        {
            if (_schedule != null)
            {
                cbAllSchedules.Items[_scheduleItemCurSel] = _schedule;
            }

            var s  = cbAllSchedules.Items.Add(
                new JobSchedule()
                );
            cbAllSchedules.SelectedIndex = s;
            btnDelSchedule.Enabled = true;
        }

        private void btnDelSchedule_Click(object sender, EventArgs e)
        {
            _suspendJobCheckEvents = true;
            cbAllSchedules.Items.RemoveAt(cbAllSchedules.SelectedIndex);
            _schedule = null;
            _suspendJobCheckEvents = false;

            cbAllSchedules.SelectedIndex = 0;
            if (cbAllSchedules.Items.Count == 1) btnDelSchedule.Enabled = false;
        }

        private void cbAllSchedules_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(_suspendJobCheckEvents) return;
            _suspendJobCheckEvents = true;

            if (_schedule != null)
            {
                cbAllSchedules.Items[_scheduleItemCurSel] = _schedule;
            }

            _scheduleItemCurSel = cbAllSchedules.SelectedIndex;
            var itm = (JobSchedule)cbAllSchedules.SelectedItem;
            panelSchedule.Controls.Clear();

            if (_current != null && _schedule != null) _current.UpdateJobSchedule(_schedule);
            _schedule = itm;

            rdSchedDaily.Checked = false;
            rdSchedWeekly.Checked = false;
            rdSchedMonthly.Checked = false;
            switch (itm.ScheduleType)
            {
            case JobScheduleType.Daily:
                rdSchedDaily.Checked = true;
                _current = new DailySchedule();
                break;
            case JobScheduleType.Weekly:
                _current = new WeeklySchedule();
                rdSchedWeekly.Checked = true;
                break;
            case JobScheduleType.Monthly:
                _current = new MonthlySchedule();
                rdSchedMonthly.Checked = true;
                break;
            default:
                throw new Exception("Unexpected!");
            }
            panelSchedule.Controls.Add((UserControl)_current);
            _current.LoadJobSchedule(itm);
            _current.EnableEdit(true);
            _suspendJobCheckEvents = false;
        }

        private void btnSaveSchedule_Click(object sender, EventArgs e)
        {
            if(_archiveJob==null) return;

            var name = _archiveJob.UniqueJobName;
            SyncArchiveWithGui();
            _serverLink.SaveOrUpdateArchiveJob(_archiveJob);
            ReloadJobList();

            foreach (string item in lbAllSchedule.Items)
            {
                if (string.Compare(name, item) == 0)
                {
                    lbAllSchedule.SelectedItem = item;
                    break;
                }
            }
        }

        private void lbAllSchedule_SelectedIndexChanged(object sender, EventArgs e)
        {
            var active = lbAllSchedule.SelectedItem as string;
            _archiveJob = string.IsNullOrEmpty(active) ? null : _serverLink.LoadArchiveJob(active);
            _schedule = null;

            if (_archiveJob != null)
            {
                //btnDelSchedule.Enabled = true;
                btnDelArchive.Enabled = true;
            }
            SyncGuiWithArchive();
        }

        private void btnDelArchive_Click(object sender, EventArgs e)
        {
            if(_archiveJob==null) return;
            if(!_archiveJob.JobUID.HasValue) return;

            var result = MessageBox.Show(
                this,
                @"**THIS OPERATION CAN'T BE UNDONE**

The schedule and the file status database will be permanently dropped. You will have to request " +
                @"the file to be dropped on your cloud provider manually. Do you really want to proceed?",
                "WARNING - Are you sure that you want to go head?",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2
                );

            if(result!=DialogResult.Yes) return;
            
            _serverLink.DropArchiveJob( _archiveJob.JobUID.Value );
            ReloadJobList();
        }

        private void btnRunNow_Click(object sender, EventArgs e)
        {
            if(_archiveJob==null) return;
            if(!_archiveJob.JobUID.HasValue) return;

            _serverLink.RunJobNow(_archiveJob.JobUID.Value);
            MessageBox.Show(this, "Job has been triggered.");
        }

    }
}
