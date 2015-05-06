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
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;
using System.Threading;

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

            var job = ArchiveJob.LoadSchedule(archiveJob.Description);
            job.JobUID = uid;
            job.UniqueJobName = archiveJob.Name;
            job.Active = archiveJob.Active;

            return job;
        }

        public ArchiveJob LoadArchiveJob(string name)
        {
            var archiveJob = Program.Database.JobProxy.LoadSchedule(name);
            if (archiveJob == null) return null;

            var job = ArchiveJob.LoadSchedule(archiveJob.Description);
            job.JobUID = archiveJob.Uid;
            job.UniqueJobName = archiveJob.Name;
            job.Active = archiveJob.Active;
            log.InfoFormat("Loading job [{0}]:[{1}] {2}", job.JobUID, job.UniqueJobName, job);

            return job;
        }

        public int SaveOrUpdateArchiveJob(ArchiveJob job)
        {
            log.InfoFormat("Saving/Updating job [{0}]:[{1}] {2}",job.JobUID,job.UniqueJobName,job);
            if (!job.JobUID.HasValue)
            {
                job.JobUID = Program.Database.GenerateJobUid();
            }

            var xmlSchedule = ArchiveJob.SaveSchedule(job);
            log.InfoFormat("Schedule : {0}", job.ToString());

            Program.Database.JobProxy.InsertSchedule(
                job.JobUID.Value,
                job.UniqueJobName,
                xmlSchedule,
                job.Active
            );

            Program.Scheduler.ReLoadSchedule();

           return job.JobUID.Value;
        }

        public DataTable GetBackupReports(int uid)
        {
            var ret = new DataTable("Reports");

            ret.Columns.Add("Runtime",   typeof(DateTime));
            ret.Columns.Add("Success",   typeof(bool));
            ret.Columns.Add("Operation", typeof(string));
            var jobReports = Program.Database.JobProxy.GetBackupReports(uid);

            while(jobReports.MoveNext())
            {
                ret.Rows.Add(new DateTime(jobReports.Current.Runtime),
                    jobReports.Current.Success,
                    jobReports.Current.Operation);
            }

            return ret;
        }

        public string GetBackupReport(int uid, long runtime)
        {
            return Program.Database.JobProxy.GetBackupReport(uid, runtime);
        }

        public void DropArchiveJob(int uid)
        {
            log.InfoFormat("Dropping job [{0}]", uid);
            Program.Database.JobProxy.DropSchedule(uid);
        }

        public void ResetArchiveJob(int uid)
        {
            log.InfoFormat("Resetting job [{0}]", uid);
            Program.Database.JobProxy.ResetStatus(uid);
            Program.Database.JobProxy.BackupReport(uid, DateTime.Now.Ticks, true,
                "Archive Job Reset",
                "The Archive Job history was resetted");
        }

        void RunJobThread(object objJob)
        {
            var job = (dynamic)objJob;
            Backup.Process.RunBackup(job.Job, job.ForceFullBackup, "Manual Backup");
        }

        public void RunJobNow(int uid,bool forceFullBackup)
        {
            var job = LoadArchiveJob(uid);
            ThreadPool.QueueUserWorkItem(RunJobThread,
                new { Job = job, ForceFullBackup = forceFullBackup }
                );
        }

        public static void ActivateApi()
        {
            log.Info("Activating API");

            var serverSinkProvider = new BinaryServerFormatterSinkProvider
            {
                TypeFilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full
            };

            IDictionary properties = new Hashtable();
            properties["portName"] = System.Configuration.ConfigurationManager.AppSettings["ApiPort"] ?? "localhost:19888";
            properties["authorizedGroup"] = System.Configuration.ConfigurationManager.AppSettings["ApiAccessGroup"] ?? "Interactive";

            var serverChannel = new IpcChannel(properties, null, serverSinkProvider);
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
