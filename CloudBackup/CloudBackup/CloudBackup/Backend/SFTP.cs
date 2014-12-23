using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CloudBackup.API;
using Renci.SshNet;

namespace CloudBackup.Backend
{
    class SFTP : Backend
    {
        static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(SFTP));

        SftpClient _sftp;

        public SFTP(Uri targetServer,string password)
        {
            _sftp = new SftpClient(targetServer.Host,targetServer.Port,
                targetServer.UserInfo,
                password);

            log.InfoFormat("SFTP - Connecting to {0}", targetServer);
            _sftp.Connect();
            log.InfoFormat("SFTP - Changing dir to {0}", targetServer.LocalPath);
            _sftp.ChangeDirectory(targetServer.LocalPath);
        }

        public override void Dispose()
        {
            if (_sftp != null)
            {
                _sftp.Dispose();
                _sftp = null;
            }
        }

        public override void Upload(string fileName, Stream source)
        {
            _sftp.UploadFile(source,fileName,true);
        }

        public override void Delete(string fileName)
        {
            _sftp.DeleteFile(fileName);
        }
    }
}
