using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Threading.Tasks;

using CloudBackup.Backup;
using AlphaFS = Alphaleonis.Win32.Filesystem;

namespace CloudBackup.Test.Backup
{
    [TestFixture]
    class Engine
    {
        [Test]
        public void TestBackup()
        {
            var engine = new CloudBackup.Backup.Engine();

            engine.RootPath = @"C:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\";

            using (var backup = engine.CreateBackup())
            {
                backup.CreateShadowCopy();

                var snapRoot = backup.GetSnapshotPath();
                var ret = backup.GetSnapshotElements();

                backup.DropBackup();

            }


        }

    }

}
