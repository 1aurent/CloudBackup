using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudBackup.API;

namespace CloudBackup.Manager.Schedule
{
    interface IScheduleEditor
    {
        void LoadJobSchedule(JobSchedule schedule);
        void UpdateJobSchedule(JobSchedule schedule);
        void EnableEdit(bool status);
    }
}
