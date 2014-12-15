using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CloudBackup.API;
using Renci.SshNet;

namespace CloudBackup.Backend
{
    class SCP : Backend
    {
        static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(SFTP));

        ScpClient _scp;
        readonly string _targetPath;

        public SCP(Target target)
        {
            _scp = new ScpClient(target.TargetServer.Host,
                target.TargetServer.UserInfo,
                target.Password);

            log.InfoFormat("SCP - Connecting to {0}", target.TargetServer);
            _scp.Connect();

            _targetPath = target.TargetServer.LocalPath;
            if (!_targetPath.EndsWith("/")) _targetPath += "/";
        }
        public override void Dispose()
        {
            if (_scp != null)
            {
                _scp.Dispose();
                _scp = null;
            }
        }

        public override void Upload(string fileName, Stream source)
        {
            _scp.Upload(source,_targetPath+fileName);
        }
    }
}
