using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudBackup.Utils;

namespace CloudBackup.Database
{
    class Database : IDisposable
    {
        static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(Database));
        static readonly Version DbVersion = new Version(1,0,0,0);

        readonly object _lock =new object();

        private SQLiteConnection _cnx;
        private int _nextJobUid;
        
        public Database ()
        {
            var cstr = (new SQLiteConnectionStringBuilder()
            {
                DataSource = Path.Combine(Program.Configuration.DatabasePath, "cbackup.db"),
                FailIfMissing = false
            }).ConnectionString;

            log.DebugFormat("Database - Opening database [{0}]",cstr);
            _cnx = new SQLiteConnection(cstr);
            _cnx.Open();


            bool needInitDb = false;
            using (var cmd = _cnx.CreateCommand())
            {
                cmd.CommandText = "SELECT count(*) FROM sqlite_master WHERE tbl_name='Version' and type='table'";
                var count = cmd.ExecuteScalar();
                if (Convert.ToInt32(count) != 1)
                {
                    log.Info("Version table not found - Assume database is empty");
                    needInitDb = true;
                }
                else
                {
                    cmd.CommandText = "SELECT value FROM Version WHERE id=2";
                    var verText = cmd.ExecuteScalar().ToString();
                    var version = new Version(verText);

                    log.DebugFormat("Database - Version [{0}] ({1})", verText, version);
                    if (version.CompareTo(DbVersion) > 0)
                    {
                        log.Fatal("This database is incompatible - Stopping");
                        throw new Exception("Fatal - Incompatible database");
                    }
                }
            }

            if (needInitDb)
            {
                log.Info("Database initialisation required - Running start script");

                // ReSharper disable once AssignNullToNotNullAttribute
                using (var strm = System.Reflection.Assembly.GetEntryAssembly().GetManifestResourceStream(
                    typeof(Database),"SetupDb.sql"))
                using (var srdr = new StreamReader(strm))
                using (var cmd = _cnx.CreateCommand())
                {
                    cmd.CommandText = srdr.ReadToEnd();
                    cmd.ExecuteNonQuery();
                }
            }

            log.Info("Database is ready");

            _nextJobUid = (int)( JobProxy.GetJobMaxId() + 1 );
        }

        public void Dispose()
        {
            if (_cnx != null)
            {
                _cnx.Close();
                _cnx = null;
            }
        }

        public int GenerateJobUid()
        {
            lock (_lock)
            {
                return ++_nextJobUid;
            }
        }

        public ISnapProxy CreateSnapProxy()
        {
            var snapProxy = BridgeCompiler.CreateInstance<ISnapProxy>(_cnx);
            snapProxy.Transaction = _cnx.BeginTransaction();
            return snapProxy;
        }

        public IJobProxy JobProxy { get { return BridgeCompiler.CreateInstance<IJobProxy>(_cnx); } }
        public ISettings Settings { get { return BridgeCompiler.CreateInstance<ISettings>(_cnx); } }
    }
}
