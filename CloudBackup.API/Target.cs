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

        public Uri ProxyServer { get; set; }
        public string ProxyPassword { get; set; }

        public Target()
        {
            TargetServer = new Uri("ssh://changeme@host.example.com:22/");
            Password = "changeme";
            ZipPassword = "";
            ProxyServer = null;
            ProxyPassword = null;
        }

        protected Target(SerializationInfo info, StreamingContext context)
        {
            TargetServer = new Uri(info.GetString("targetServer"));
            Password = info.GetString("password");
            ZipPassword = info.GetString("zipPassword");

            var usingProxy = info.GetBoolean("haveProxy");
            if (usingProxy)
            {
                ProxyServer = new Uri(info.GetString("proxyServer"));
                ProxyPassword = info.GetString("proxyPassword");
            }
        }


        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("targetServer",TargetServer.ToString());
            info.AddValue("password", Password);
            info.AddValue("zipPassword", ZipPassword);

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
            if (ProxyServer != null)
            {
                writer.WriteAttributeString("proxyServer", ProxyServer.ToString());
                writer.WriteAttributeString("proxyPassword", ProxyPassword);
            }
        }
    }
}
