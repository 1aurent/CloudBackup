using CloudBackup.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Renci.SshNet;
using System.IO;

namespace CloudBackup.Backend
{
    class Proxy : Backend
    {
        SshClient _client;
        string _localUri;
        Backend _subBackend;

        static int HackExtractPort(ForwardedPortLocal fwl)
        {
            var type = fwl.GetType();
            var listener = (System.Net.Sockets.TcpListener)type
                .GetField("_listener",System.Reflection.BindingFlags.NonPublic|System.Reflection.BindingFlags.Instance)
                .GetValue(fwl);

            return ((System.Net.IPEndPoint)listener.LocalEndpoint).Port;
        }


        public Proxy(Target target)
        {
            _client = new SshClient(target.ProxyServer.Host,
                target.ProxyServer.UserInfo,
                target.ProxyPassword);
            _client.Connect();

            var fwPort = new ForwardedPortLocal("127.0.0.1",0, target.TargetServer.Host, (uint) target.TargetServer.Port);
            _client.AddForwardedPort(fwPort);
            fwPort.Start();

            var port = HackExtractPort(fwPort); // TODO: Update the library to read it directly from BoundPort

            _localUri = string.Format("{0}://{1}@127.0.0.1:{2}{3}", target.TargetServer.Scheme,
                target.TargetServer.UserInfo,
                port,
                target.TargetServer.LocalPath);

            _subBackend = Backend.OpenBackend(new Uri(_localUri), target.Password);
        }

        public override void Dispose()
        {
            if (_subBackend != null)
            {
                _subBackend.Dispose();
                _subBackend = null;
            }

            if (_client != null)
            {
                _client.Dispose();
                _client = null;
            }
        }

        public override void Upload(string fileName, Stream source)
        {
            _subBackend.Upload(fileName, source);
        }

        public override void Delete(string fileName)
        {
            _subBackend.Delete(fileName);
        }

    }
}
