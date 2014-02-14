using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CloudBackup.API
{
    class API : MarshalByRefObject, IAPI
    {
        static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(API));

        public JobOverview[] GetJobs()
        {
            var list = new List<JobOverview>();
            using (var @enum = Program.Database.JobProxy.GetJobOverview())
            {
                while (@enum.MoveNext())
                {
                    log.DebugFormat("GetJobs /  Found {0} : {1}",@enum.Current.JobUID,@enum.Current.Name);
                    list.Add(@enum.Current);
                }    
            }
            return list.ToArray();
        }

        public ArchiveJob LoadArchiveJob(int uid)
        {
            var archiveJob = Program.Database.JobProxy.LoadSchedule(uid);
            if (archiveJob == null) return null;

            var job = new ArchiveJob
            {
                JobUID = archiveJob.Uid,
                UniqueJobName = archiveJob.Name,
                JobRootPath = archiveJob.RootPath,
                Active = archiveJob.Active
            };

            job.LoadSchedule(archiveJob.Schedule);
            return job;
        }

        public ArchiveJob LoadArchiveJob(string name)
        {
            var archiveJob = Program.Database.JobProxy.LoadSchedule(name);
            if (archiveJob == null) return null;

            var job = new ArchiveJob
            {
                JobUID = archiveJob.Uid,
                UniqueJobName = archiveJob.Name,
                JobRootPath = archiveJob.RootPath,
                Active = archiveJob.Active
            };
            log.InfoFormat("Loading job [{0}]:[{1}] {2}", job.JobUID, job.UniqueJobName, job);

            job.LoadSchedule(archiveJob.Schedule);
            return job;
        }

        public int SaveOrUpdateArchiveJob(ArchiveJob job)
        {
            log.InfoFormat("Saving/Updating job [{0}]:[{1}] {2}",job.JobUID,job.UniqueJobName,job);
            if (!job.JobUID.HasValue)
            {
                job.JobUID = Program.Database.GenerateJobUid();
            }

            var xmlSchedule = job.SaveSchedule();
            log.InfoFormat("Schedule : {0}",xmlSchedule);

            Program.Database.JobProxy.InsertSchedule(
                job.JobUID.Value,
                job.UniqueJobName,
                xmlSchedule,
                job.JobRootPath,
                job.Active
            );

            Program.Scheduler.ReLoadSchedule();

           return job.JobUID.Value;
        }

        public void DropArchiveJob(int uid)
        {
            log.InfoFormat("Dropping job [{0}]", uid);
            Program.Database.JobProxy.DropSchedule(uid);
        }

        void RunJobThread(object objJob)
        {
            var job = (ArchiveJob) objJob;
            Backup.Process.RunBackup(job);
        }

        public void RunJobNow(int uid)
        {
            var job = LoadArchiveJob(uid);
            ThreadPool.QueueUserWorkItem(RunJobThread, job);
        }

        public static void ActivateApi()
        {
            log.Info("Activating API");
            var serverChannel = new IpcChannel("localhost:19888");
            ChannelServices.RegisterChannel(serverChannel,true);

            log.DebugFormat(" - The name of the channel is {0}.",serverChannel.ChannelName);
            log.DebugFormat(" - The priority of the channel is {0}.", serverChannel.ChannelPriority);

            var channelData = (ChannelDataStore)serverChannel.ChannelData;
            foreach (var uri in channelData.ChannelUris)
            {
                log.DebugFormat(" - The channel URI is {0}.", uri);
            }

            RemotingConfiguration.RegisterWellKnownServiceType(
                        typeof(API),"API",WellKnownObjectMode.Singleton
                    );

            var urls = serverChannel.GetUrlsForUri("API");
            if (urls.Length > 0)
            {
                string objectUrl = urls[0];
                string objectUri;
                string channelUri = serverChannel.Parse(objectUrl, out objectUri);
                log.DebugFormat("The object URI is {0}.", objectUri);
                log.DebugFormat("The channel URI is {0}.", channelUri);
                log.DebugFormat("The object URL is {0}.", objectUrl);
            }
        }
    }
}
