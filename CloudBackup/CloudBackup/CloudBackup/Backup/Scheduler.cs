using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CloudBackup.API;

namespace CloudBackup.Backup
{
    public class Scheduler : IDisposable
    {
        static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(Scheduler));

        public class ScheduleJob
        {
            static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(ScheduleJob));
            readonly ArchiveJob _job;

            public ScheduleJob(Database.ScheduleRow rawSchedule)
            {
                _job = new ArchiveJob
                {
                    JobUID = rawSchedule.Uid,
                    UniqueJobName = rawSchedule.Name,
                    JobRootPath = rawSchedule.RootPath,
                    Active = rawSchedule.Active
                };
                _job.LoadSchedule(rawSchedule.Schedule);
            }

            public ScheduleJob(ArchiveJob job)
            {
                _job = job;
            }

            public DateTime NextRunTime { get; set; }
            public ArchiveJob JobSpec { get { return _job; }}

            public DateTime ComputeNextRuntime(DateTime now)
            {
                var next = DateTime.MaxValue;

                foreach (var jobSchedule in _job.Schedules)
                {
                    DateTime scheduleTime;

                    switch (jobSchedule.ScheduleType)
                    {
                        case JobScheduleType.Daily:
                            scheduleTime = GetRuntimeForDaily(now, jobSchedule.Daily);
                            break;
                        case JobScheduleType.Weekly:
                            scheduleTime = GetRuntimeForWeekly(now, jobSchedule.Weekly);
                            break;
                        case JobScheduleType.Monthly:
                            scheduleTime = GetRuntimeForMonthly(now, jobSchedule.Monthly);
                            break;
                        default:
                            scheduleTime = DateTime.MaxValue;
                            break;
                    }

                    if (scheduleTime < next) next = scheduleTime;
                }

                return next;
            }


            static int AdjustEvery(int v, int e)
            {
                return (e - ((v - 1)%e))%e;
            }

            DateTime GetRuntimeForMonthly(DateTime now, MonthlySchedule schedule)
            {
                for (;;)
                {
                    var time = new DateTime(now.Year, now.Month, 1);
                    time = time.AddMonths(AdjustEvery(now.Month,schedule.Every));
                    switch (schedule.RelativeTo)
                    {
                        case JobRelativeTo.BeginOfTheMonth:
                            time = time.AddDays(schedule.DayOffset);
                            break;
                        case JobRelativeTo.EndOfTheMonth:
                            time = time.AddMonths(1).AddDays(-(schedule.DayOffset + 1));
                            break;
                        case JobRelativeTo.FirstSundayOfTheMonth:
                            if (time.DayOfWeek != DayOfWeek.Sunday) time = time.AddDays(7 - ((int) time.DayOfWeek));
                            time = time.AddDays(schedule.DayOffset);
                            break;
                    }

                    time = time.AddMinutes(schedule.RunAt.Hour*60 + schedule.RunAt.Minute);
                    if (time > now) return time;

                    now = now.AddMonths(1);
                }
            }

            static int GetIso8601WeekOfYear(DateTime time)
            {
                // Seriously cheat.  If its Monday, Tuesday or Wednesday, then it'll 
                // be the same week# as whatever Thursday, Friday or Saturday are,
                // and we always get those right
                var day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
                if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
                {
                    time = time.AddDays(3);
                }

                // Return the week of our adjusted day
                return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            }

            DateTime GetRuntimeForWeekly(DateTime now, WeeklySchedule schedule)
            {
                var @ref = new DateTime(now.Year, now.Month, now.Day).AddDays( 
                    now.DayOfWeek==DayOfWeek.Sunday? 1 : -((int)now.DayOfWeek-1) );
                @ref = @ref.AddDays(
                    AdjustEvery(GetIso8601WeekOfYear(@ref), schedule.Every)*7
                );
                for (;;)
                {
                    var time = new DateTime(@ref.Year, @ref.Month, @ref.Day,
                        schedule.RunAt.Hour,schedule.RunAt.Minute,0);
                    var endWeek = new DateTime(@ref.Year, @ref.Month, @ref.Day).AddDays(7);

                    while (time < endWeek)
                    {
                        if (time < now)
                        {
                            time = time.AddDays(1);
                            continue;
                        }

                        switch (time.DayOfWeek)
                        {
                        case DayOfWeek.Monday:
                                if (schedule.JobDays.HasFlag(JobDays.Monday)) return time;
                                break;
                        case DayOfWeek.Tuesday:
                                if (schedule.JobDays.HasFlag(JobDays.Tuesday)) return time;
                                break;
                        case DayOfWeek.Wednesday:
                                if (schedule.JobDays.HasFlag(JobDays.Wednesday)) return time;
                                break;
                        case DayOfWeek.Thursday:
                                if (schedule.JobDays.HasFlag(JobDays.Thursday)) return time;
                                break;
                        case DayOfWeek.Friday:
                                if (schedule.JobDays.HasFlag(JobDays.Friday)) return time;
                                break;
                        case DayOfWeek.Saturday:
                                if (schedule.JobDays.HasFlag(JobDays.Saturday)) return time;
                                break;
                        case DayOfWeek.Sunday:
                                if (schedule.JobDays.HasFlag(JobDays.Sunday)) return time;
                                break;
                        }

                        time = time.AddDays(1);
                    }

                    @ref = @ref.AddDays(schedule.Every*7);
                }
            }

            DateTime GetRuntimeForDaily(DateTime now, DailySchedule schedule)
            {
                var @ref = new DateTime(now.Year, now.Month, now.Day).AddDays((now.DayOfYear-1)%schedule.Every);
                @ref = @ref.AddMinutes(schedule.RunAt.Hour*60 + schedule.RunAt.Minute);
                if (@ref < now) @ref = @ref.AddDays(schedule.Every);
                return @ref;
            }

        }

        readonly Dictionary<int, ScheduleJob> _allJobs;
        readonly object _lock;
        bool _timerActive;
        Timer _timer;

        public Scheduler()
        {
            _allJobs = new Dictionary<int, ScheduleJob>();
            _lock = new object();

            ReLoadSchedule();

            _timer = new Timer(ProcessSchedule,null,10000,30000);
        }

        public void Dispose()
        {
            lock (_lock)
            {
                _allJobs.Clear();
            }
            if (_timer != null)
            {
                _timer.Dispose();
                _timer = null;
            }
        }


        public void ReLoadSchedule()
        {
            lock (_lock)
            {
                _allJobs.Clear();

                log.Info("Loading job schedule");
                using (var jobs = Program.Database.JobProxy.LoadAllActiveSchedule())
                {
                    var now = DateTime.Now;
                    while (jobs.MoveNext())
                    {
                        log.InfoFormat("Setting up job {0}:[{1}]", jobs.Current.Uid, jobs.Current.Name);
                        var sc = new ScheduleJob(jobs.Current);
                        sc.NextRunTime = sc.ComputeNextRuntime(now);
                        log.DebugFormat("Job {0}:[{1}] - Next run time @ {2}", jobs.Current.Uid, jobs.Current.Name,sc);
                        _allJobs.Add(jobs.Current.Uid, sc);
                    }
                }
            }
        }

        void RunJob(object jobObj)
        {
            var job = (ArchiveJob) jobObj;

            Process.RunBackup(job);
        }

        void ProcessSchedule(object unused)
        {
            lock (_lock)
            {
                if(_timerActive) return;
                _timerActive = true;
            }

            var now = DateTime.Now;
            ScheduleJob currentJob;

            log.InfoFormat("Starting processing @ {0}",now);
            do
            {
                currentJob = null;
                lock (_lock)
                {
                    foreach (var job in _allJobs.Values)
                    {
                        if (job.NextRunTime < now)
                        {
                            currentJob = job;
                            break;
                        }
                    }
                }

                if (currentJob != null)
                {
                    log.InfoFormat("Starting job {0}:[{1}]", 
                        currentJob.JobSpec.JobUID,
                        currentJob.JobSpec.UniqueJobName);

                    ThreadPool.QueueUserWorkItem(RunJob, currentJob.JobSpec);

                    currentJob.NextRunTime = currentJob.ComputeNextRuntime(now);
                    log.DebugFormat("Job {0}:[{1}] - Next run time @ {2}",
                        currentJob.JobSpec.JobUID, currentJob.JobSpec.UniqueJobName, 
                        currentJob.NextRunTime);

                }

            } while (currentJob != null);

            lock (_lock)
            {
                _timerActive = false;
            }

            log.InfoFormat("Schedule processing @ {0} completed", now);
        }


    }
}
