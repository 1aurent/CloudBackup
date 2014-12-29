using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace CloudBackup.API
{
    [Serializable]
    public class Target : ISerializable, IXmlSerializable
    {
        public Uri TargetServer { get; set; }
        public string Password { get; set; }
        public string ZipPassword { get; set; }
        public bool ManageTargetFiles { get; set; }

        public Uri ProxyServer { get; set; }
        public string ProxyPassword { get; set; }

        public Target()
        {
            TargetServer = new Uri("SFTP://changeme@host.example.com:22/");
            Password = "changeme";
            ZipPassword = "";
            ProxyServer = null;
            ProxyPassword = null;
            ManageTargetFiles = true;
        }

        protected Target(SerializationInfo info, StreamingContext context)
        {
            TargetServer = new Uri(info.GetString("targetServer"));
            Password = info.GetString("password");
            ZipPassword = info.GetString("zipPassword");
            ManageTargetFiles = info.GetBoolean("isFilesManaged");

            var usingProxy = info.GetBoolean("haveProxy");
            if (usingProxy)
            {
                ProxyServer = new Uri(info.GetString("proxyServer"));
                ProxyPassword = info.GetString("proxyPassword");
            }
        }

        public override string ToString()
        {
            return string.Format("[Target {0} via {1}]",
                TargetServer.ToString(),
                ProxyServer == null ? "direct" : ProxyServer.ToString());
        }


        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("targetServer",TargetServer.ToString());
            info.AddValue("password", Password);
            info.AddValue("zipPassword", ZipPassword);
            info.AddValue("isFilesManaged", ManageTargetFiles);

            if (ProxyServer != null)
            {
                info.AddValue("haveProxy", true);
                info.AddValue("proxyServer", ProxyServer.ToString());
                info.AddValue("proxyPassword", ProxyPassword);
            }
            else
            {
                info.AddValue("haveProxy", false);
            }
        }

        public XmlSchema GetSchema() { return null; }

        public void ReadXml(XmlReader reader)
        {
            TargetServer = new Uri(reader.GetAttribute("targetServer"));
            Password = reader.GetAttribute("password");
            ZipPassword = reader.GetAttribute("zipPassword");
            ManageTargetFiles = XmlConvert.ToBoolean(reader.GetAttribute("isFilesManaged") ?? "False");
            var val = reader.GetAttribute("proxyServer");
            if (val != null)
            {
                ProxyServer = new Uri(val);
                ProxyPassword = reader.GetAttribute("proxyPassword");
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString("targetServer", TargetServer.ToString());
            writer.WriteAttributeString("password", Password);
            writer.WriteAttributeString("zipPassword", ZipPassword);
            writer.WriteAttributeString("isFilesManaged", XmlConvert.ToString(ManageTargetFiles));
            if (ProxyServer != null)
            {
                writer.WriteAttributeString("proxyServer", ProxyServer.ToString());
                writer.WriteAttributeString("proxyPassword", ProxyPassword);
            }
        }
    }
}
