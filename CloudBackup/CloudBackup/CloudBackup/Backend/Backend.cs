using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.FtpClient;
using System.Text;
using CloudBackup.API;

namespace CloudBackup.Backend
{
    abstract class Backend : IDisposable
    {
        public abstract void Dispose();

        public abstract void Upload(string fileName, Stream source);
        public abstract void Delete(string fileName);


        public static Backend OpenBackend(Target target)
        {
            if (target.ProxyServer != null) return new Proxy(target);
            return OpenBackend(target.TargetServer, target.Password);
        }

        public static Backend OpenBackend(Uri targetServer,string password)
        {
            switch (targetServer.Scheme.ToLowerInvariant())
            {
                case "sftp":
                    return new SFTP(targetServer, password);
                case "ftp":
                    return new FTP(targetServer, password, FtpEncryptionMode.None);
                case "ftps":
                    return new FTP(targetServer, password, FtpEncryptionMode.Implicit);
                case "ftpes":
                    return new FTP(targetServer, password, FtpEncryptionMode.Explicit);


                default:
                    throw new Exception("Unsupported scheme "+targetServer.Scheme);
            }
        }

    }
}
