using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CloudBackup.API;

namespace CloudBackup.Backend
{
    abstract class Backend : IDisposable
    {
        public abstract void Dispose();

        public abstract void Upload(string fileName, Stream source);

        public static Backend OpenBackend(Target target)
        {
            switch (target.TargetServer.Scheme.ToLowerInvariant())
            {
                case "sftp":
                    return new SFTP(target);
                case "scp":
                    return new SCP(target);


                default:
                    throw new Exception("Unsupported scheme "+target.TargetServer.Scheme);
            }
        }

    }
}
