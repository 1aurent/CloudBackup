using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace CloudBackup.API
{
    [Serializable]
    public class ArchiveJob : ISerializable, IXmlSerializable
    {
        public ArchiveJob()
        {
            UniqueJobName = "New Job";
            JobRootPath = "C:\\";
            Schedules = new List<JobSchedule>();
            Active = true;

            var defSchedule = new JobSchedule();
            Schedules.Add(defSchedule);
        }

        public int? JobUID { get; set; }
        public string UniqueJobName { get; set; }
        public string JobRootPath { get; set; }
        public bool Active { get; set; }

        public List<JobSchedule> Schedules { get; private set; }

        public void LoadSchedule(string xmlSchedule)
        {
            var serializer = new XmlSerializer(typeof(List<JobSchedule>));
            using (TextReader reader = new StringReader(xmlSchedule))
            {
                Schedules = (List<JobSchedule>)serializer.Deserialize(reader);
            }
        }

        public string SaveSchedule()
        {
            var serializer = new XmlSerializer(typeof(List<JobSchedule>));
            using (TextWriter writer = new StringWriter())
            {
                serializer.Serialize(writer,Schedules);
                return writer.ToString();
            }            
        }

        protected ArchiveJob(SerializationInfo info, StreamingContext context)
        {
            var haveUid = info.GetBoolean("uidf");
            JobUID = haveUid ? info.GetInt32("uid"): (int?) null; 
            
            UniqueJobName = info.GetString("name");
            JobRootPath = info.GetString("path");
            Active = info.GetBoolean("active");
            Schedules = (List<JobSchedule>)info.GetValue("schedules", typeof(List<JobSchedule>));
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("uidf",JobUID.HasValue);
            if(JobUID.HasValue) info.AddValue("uid",JobUID.Value);
            info.AddValue("name", UniqueJobName);
            info.AddValue("path", JobRootPath);
            info.AddValue("active", Active);
            info.AddValue("schedules", Schedules);
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            var val = reader.GetAttribute("uid");
            JobUID = string.IsNullOrEmpty(val) ? null : (int?)int.Parse(val);
            UniqueJobName = reader.GetAttribute("name");
            JobRootPath = reader.GetAttribute("path");
            Active = int.Parse(reader.GetAttribute("active")??"0") != 0;
            reader.Read(); if (reader.Name != "Schedules") throw new Exception("Expecting Schedules node");
            Schedules.Clear();
            reader.Read(); 
            for (;;)
            {
                if(reader.NodeType== XmlNodeType.EndElement) break;
                var schedule = new JobSchedule();
                schedule.ReadXml(reader);
                Schedules.Add(schedule);
            }
            reader.ReadEndElement();
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString("uid", JobUID.HasValue? JobUID.Value.ToString(): "");
            writer.WriteAttributeString("name", UniqueJobName);
            writer.WriteAttributeString("path", JobRootPath);
            writer.WriteAttributeString("active", Active?"1":"0");
            writer.WriteStartElement("Schedules");
            foreach (var jobSchedule in Schedules)
            {
                writer.WriteStartElement("Schedule");
                jobSchedule.WriteXml(writer);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }
    }
}
