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
using System.Data.SQLite;
using System.IO;
using System.Security.Cryptography;
using System.Text;
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

        static bool DisableEncryption()
        {
            var disableEncryptionStr = System.Configuration.ConfigurationManager.AppSettings["DisableEncryption"] ?? "False";
            bool disableEncryption = false;

            if (!bool.TryParse(disableEncryptionStr, out disableEncryption)) disableEncryption = false;

            return disableEncryption;
        }
        
        public Database ()
        {
            var cstr = (new SQLiteConnectionStringBuilder()
            {
                DataSource = Path.Combine(Program.Configuration.DatabasePath, "cbackup.db"),
                FailIfMissing = false,
            }).ConnectionString;

            log.DebugFormat("Database - Opening database [{0}]",cstr);
            _cnx = new SQLiteConnection(cstr);
            if (!DisableEncryption())
            {
                log.Info("Applying security password");
                _cnx.SetPassword(GetDatabasePassword());
            }
            else
                log.Warn("NOTICE - The database is not encrypted !");
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

        static byte[] GetDatabasePassword()
        {
            var secret = new byte[128];
            byte[] buffer;

            if (File.Exists("cbackup.secure"))
            {
                using (var fle = File.OpenRead("cbackup.secure"))
                {
                    buffer = new byte[(int)fle.Length];
                    fle.Read(buffer, 0, (int)fle.Length);
                }
                secret = ProtectedData.Unprotect(buffer, null, DataProtectionScope.LocalMachine);
            }
            else
            {
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(secret);
                    buffer = ProtectedData.Protect(secret, null, DataProtectionScope.LocalMachine);
                    using (var fle = File.OpenWrite("cbackup.secure")) fle.Write(buffer, 0, buffer.Length);
                }
            }
            return secret;
        }

    }
}
