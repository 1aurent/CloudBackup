using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudBackup.API;
using NUnit.Framework;

namespace CloudBackup.Test.Backup
{
    [TestFixture]
    class Scheduler
    {
        [Test]
        public void TestSchedulesWeekly()
        {
            var job = new ArchiveJob
            {
                Active = true,
            };
            job.Schedules[0] = new JobSchedule
            {
                ScheduleType = JobScheduleType.Weekly,
                Weekly =
                {
                    Every = 3,
                    RunAt = new HourMinute {Hour = 10, Minute = 30},
                    JobDays = JobDays.Tuesday | JobDays.Thursday | JobDays.Sunday
                }
            };

            var p = new CloudBackup.Backup.Scheduler.ScheduleJob(job);

            var t1 = p.ComputeNextRuntime(
                new DateTime(2014, 1, 1, 9, 30, 0)
                );
            Assert.IsTrue(t1 == new DateTime(2014, 1, 2, 10, 30, 0));
            t1 = p.ComputeNextRuntime(new DateTime(2014, 1, 8, 11, 30, 0));
            Assert.IsTrue(t1 == new DateTime(2014, 1, 14, 10, 30, 0));
            t1 = p.ComputeNextRuntime(new DateTime(2014, 1, 25, 23, 30, 0));
            Assert.IsTrue(t1 == new DateTime(2014, 1, 26, 10, 30, 0));
        }

        [Test]
        public void TestSchedulesMonthly()
        {
            var job = new ArchiveJob
            {
                Active = true,
            };
            job.Schedules[0] = new JobSchedule
            {
                ScheduleType = JobScheduleType.Monthly,
                Monthly =
                {
                    Every = 3,
                    RunAt = new HourMinute { Hour = 10, Minute = 30 },
                    RelativeTo = JobRelativeTo.FirstSundayOfTheMonth,
                    DayOffset = 3
                }
            };

            var p = new CloudBackup.Backup.Scheduler.ScheduleJob(job);

            var t1 = p.ComputeNextRuntime(
                new DateTime(2014, 1, 1, 9, 30, 0)
                );
            Assert.IsTrue(t1 == new DateTime(2014, 1, 8, 10, 30, 0));
            t1 = p.ComputeNextRuntime(new DateTime(2014, 2, 14, 23, 30, 0));
            Assert.IsTrue(t1 == new DateTime(2014, 4, 9, 10, 30, 0));

        }
    }
}
