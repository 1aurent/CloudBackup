using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.FtpClient;
using System.Text;
using CloudBackup.API;

namespace CloudBackup.Backend
{
    class FTP : Backend
    {
        static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(FTP));

        FtpClient _ftp;

        public FTP(Uri target, string password, FtpEncryptionMode mode)
        {
            _ftp = new FtpClient
            {
                EncryptionMode = mode,
                Host = target.Host,
                Port = target.Port,
                Credentials = new NetworkCredential(
                    target.UserInfo,
                    password
                    )
            };

            log.InfoFormat("FTP - Connecting to {0}", target);
            _ftp.Connect();

            log.InfoFormat("FTP - Changing dir to {0}", target.LocalPath);
            _ftp.SetWorkingDirectory(target.LocalPath);
        }

        public override void Dispose()
        {
            if (_ftp != null)
            {
                _ftp.Dispose();
                _ftp = null;
            }
        }

        public Stream GetWriteStream(string fileName)
        {
            if (_ftp.FileExists(fileName))
            {
                log.InfoFormat("FTP - Deleting target file {0}", fileName);
                _ftp.DeleteFile(fileName);
            }
            return _ftp.OpenWrite(fileName, FtpDataType.Binary);
        }

        public override void Upload(string fileName, Stream source)
        {
            using (var dest = GetWriteStream(fileName))
            {
                source.CopyTo(dest);
            }
        }

        public override void Delete(string fileName)
        {
            _ftp.DeleteFile(fileName);
        }
    }
}
