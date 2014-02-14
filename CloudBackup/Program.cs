using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudBackup.Backup;

[assembly: log4net.Config.XmlConfigurator(Watch = false)]

namespace CloudBackup
{
    partial class Program : System.ServiceProcess.ServiceBase
    {
        static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(Program));

        #region <Change console>
        static Program()
        {
            try
            {
                Console.BufferHeight = Console.BufferWidth = 1024;
                Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            }
// ReSharper disable once EmptyGeneralCatchClause
            catch
            { }
        }
        #endregion

        public static class Configuration 
        {
            static Configuration()
            {
                DebugMode = false;
                DisableScheduler = false;
                DisableMaintenance = false;
                DisableAPI = false;

                DatabasePath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            }


            public static bool DebugMode { get; set; }
            public static bool DisableScheduler { get; set; }
            public static bool DisableMaintenance { get; set; }
            public static bool DisableAPI { get; set; }
            public static string DatabasePath { get; set; }
        }

        public static Database.Database Database { get; set; }
        public static Backup.Scheduler Scheduler { get; set; }

        static void ParseArgs(string[] args)
        {
            for (int u = 0; u < args.Length; u++)
            {
                if (string.Compare(args[u], "-debug", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    Configuration.DebugMode =  true;
                    log.Warn("Flag: DebugMode set");
                    continue;
                }
                if (string.Compare(args[u], "-noscheduler", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    Configuration.DisableScheduler =  true;
                    log.Warn("Flag: DisableScheduler set");
                    continue;
                }
                if (string.Compare(args[u], "-nomaintenance", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    Configuration.DisableMaintenance = true;
                    log.Warn("Flag: DisableMaintenance set");
                    continue;
                }
                if (string.Compare(args[u], "-noapi", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    Configuration.DisableAPI = true;
                    log.Warn("Flag: DisableAPI set");
                    continue;
                } 
                if (string.Compare(args[u], "-pause", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    Console.WriteLine("*** PRESS A KEY - I'M PAUSED ***");
                    Console.ReadLine();
                    continue;
                }
                throw new ArgumentException("Unknown argument " + args[u]);
            }
        }

        protected override void OnStart(string[] args)
        {
            log.Info("OnStart");

            Database = new Database.Database();
            if (Configuration.DisableAPI)
                log.InfoFormat("API is disabled");
            else
                API.API.ActivateApi();

            Scheduler = new Scheduler();
        }

        protected override void OnStop()
        {
            log.Info("OnStop");

            if (Scheduler != null) { Scheduler.Dispose(); Scheduler = null; }
            if (Database != null) { Database.Dispose(); Database = null; }
        }

        static void ServiceEmu(object oMreEnd)
        {
            try
            {
                log.Info("ServiceEmu - Program Start");
                var prg = new Program();
                prg.OnStart(null);
                log.Info("ServiceEmu - Wait for stop event");
                ((System.Threading.ManualResetEvent)oMreEnd).WaitOne();
                log.Info("ServiceEmu - Stopping");
                prg.OnStop();
            }
            catch (Exception ex)
            {
                log.Fatal("ServiceEmu",ex);
                throw;
            }
        }


        static void Main(string[] args)
        {
            log.Info("Laurent Dupuis's CloudBackup - Service [" + System.Reflection.Assembly.GetEntryAssembly().FullName + "]");
            if (args.Length > 0)
            {
                if (string.Compare(args[0], "-install", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    Install(true, InstallerOptions.Parse(args));
                    return;
                }
                if (string.Compare(args[0], "-uninstall", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    Install(false, null);
                    return;
                }
            }

            ParseArgs(args);

            if (Configuration.DebugMode)
            {
                var mreEnd = new System.Threading.ManualResetEvent(false);
                System.Threading.ThreadPool.QueueUserWorkItem(Program.ServiceEmu,mreEnd);
                Console.WriteLine("xxx Service Running xxx");
                Console.ReadLine();
                mreEnd.Set();
                return;                
            }

            Run(new Program());
        }
    }
}
