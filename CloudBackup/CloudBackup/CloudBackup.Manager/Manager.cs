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
            cbTargetProtocol.SelectedItem = 0;
            Utils.EnableDisablePanel(spltCtrlArchive.Panel2, false);

            var clientChannel = new IpcChannel();
            ChannelServices.RegisterChannel(clientChannel, true);

            _serverLink = (IAPI)Activator.GetObject(typeof(IAPI), "ipc://localhost:19888/API");

            try
            {
                ReloadJobList();
            }
            catch (RemotingException)
            {
                MessageBox.Show(this,
                    "Unable to communicate with the CloudBackup service. Please check that the service is running, close this window and try again",
                    "Unable to access service", MessageBoxButtons.OK);

                btnNewArchive.Enabled = false;

            }
        }

        void ReloadJobList()
        {
            lbAllSchedule.Items.Clear();
            foreach (var job in _serverLink.GetJobs())
            {
                lbAllSchedule.Items.Add(job.Name);
            }
            if (lbAllSchedule.Items.Count == 0)
            {
                _archiveJob = null;
                SyncGuiWithArchive();
            }
            else
            {
                lbAllSchedule.SelectedIndex = 0;
            }
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

        void SyncGuiWithArchive()
        {
            if (_archiveJob == null)
            {
                Utils.EnableDisablePanel(spltCtrlArchive.Panel2, false);

                tbArchiveJobName.Text =
                    tbRootFolder.Text = "";
                cbAllSchedules.Items.Clear();

                panelSchedule.Controls.Clear();
                return;
            }

            Utils.EnableDisablePanel(spltCtrlArchive.Panel2, true);
            btnResetStatus.Enabled =
            btnRunNow.Enabled = _archiveJob.JobUID.HasValue;

            rdSchedDaily.Checked = false;
            rdSchedWeekly.Checked = false;
            rdSchedMonthly.Checked = false;

            tbArchiveJobName.Text = _archiveJob.UniqueJobName;
            tbRootFolder.Text = _archiveJob.JobRootPath;

            cbTargetProtocol.Text = _archiveJob.JobTarget.TargetServer.Scheme;
            txtTargetUser.Text = _archiveJob.JobTarget.TargetServer.UserInfo;
            txtTargetHost.Text = _archiveJob.JobTarget.TargetServer.Host;
            txtTargetPort.Text = _archiveJob.JobTarget.TargetServer.Port.ToString();
            txtTargetPath.Text = _archiveJob.JobTarget.TargetServer.AbsolutePath;
            txtTargetPwd.Text = _archiveJob.JobTarget.Password;
            txtTargetZipPwd.Text = _archiveJob.JobTarget.ZipPassword;

            if (_archiveJob.JobTarget.ProxyServer != null)
            {
                cbUseSshProxy.Checked = true;

                txtSshProxyHost.Text = _archiveJob.JobTarget.ProxyServer.Host;
                txtSshProxyPort.Text = _archiveJob.JobTarget.ProxyServer.Port.ToString();
                txtSshProxyUser.Text = _archiveJob.JobTarget.ProxyServer.UserInfo;
                txtSshProxyPwd.Text = _archiveJob.JobTarget.ProxyPassword;
            }

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

            _archiveJob.JobTarget.TargetServer = new Uri(
                string.Format("{0}://{1}@{2}:{3}{4}{5}",
                cbTargetProtocol.Text,
                Uri.EscapeUriString(txtTargetUser.Text),
                Uri.EscapeUriString(txtTargetHost.Text),
                Uri.EscapeUriString(txtTargetPort.Text),
                txtTargetPath.Text.StartsWith("/")?"":"/",
                Uri.EscapeUriString(txtTargetPath.Text)
                )
            );
            _archiveJob.JobTarget.Password = txtTargetPwd.Text;
            _archiveJob.JobTarget.ZipPassword = txtTargetZipPwd.Text;

            if (cbUseSshProxy.Checked)
            {
                _archiveJob.JobTarget.ProxyServer = new Uri(
                    string.Format("ssh://{0}@{1}:{2}",
                       Uri.EscapeUriString(txtSshProxyUser.Text),
                       Uri.EscapeUriString(txtSshProxyHost.Text),
                        txtSshProxyPort.Text
                        ));
            }

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

        private void btnResetStatus_Click(object sender, EventArgs e)
        {
            if (_archiveJob == null) return;
            if (!_archiveJob.JobUID.HasValue) return;

            var result = MessageBox.Show(
                this,
                @"WARNING:
All file status will be dropped and the next scheduled or force backup 
will be a full image. Existing backup will not be dropped. Do you really want to proceed?",
                "WARNING - Are you sure that you want to go head?",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2
                );

            if (result != DialogResult.Yes) return;

            _serverLink.ResetArchiveJob(_archiveJob.JobUID.Value);
        }

        private void cbTargetShowPass_CheckedChanged(object sender, EventArgs e)
        {
            txtSshProxyPwd.UseSystemPasswordChar =
                txtTargetPwd.UseSystemPasswordChar =
                    txtTargetZipPwd.UseSystemPasswordChar =
                        !cbTargetShowPass.Checked;
        }

    }
}
