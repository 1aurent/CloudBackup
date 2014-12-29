/* ........................................................................
 * copyright 2015 Laurent Dupuis
 * ........................................................................
 * < This program is free software: you can redistribute it and/or modify
 * < it under the terms of the GNU General Public License as published by
 * < the Free Software Foundation, either version 3 of the License, or
 * < (at your option) any later version.
 * < 
 * < This program is distributed in the hope that it will be useful,
 * < but WITHOUT ANY WARRANTY; without even the implied warranty of
 * < MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * < GNU General Public License for more details.
 * < 
 * < You should have received a copy of the GNU General Public License
 * < along with this program.  If not, see <http://www.gnu.org/licenses/>.
 * ........................................................................
 *
 */
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
            JobTarget = new Target();
            Schedules = new List<JobSchedule>();
            Active = true;

            var defSchedule = new JobSchedule();
            Schedules.Add(defSchedule);
        }

        public int? JobUID { get; set; }
        public string UniqueJobName { get; set; }
        public string JobRootPath { get; set; }
        public bool Active { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("[ArchiveJob : <{0}> <{1}> | Path=<{2}> [{3}] ",
                JobUID, UniqueJobName, JobRootPath, Active);

            sb.Append(JobTarget.ToString());

            var serializer = new XmlSerializer(typeof(ArchiveJob));
            using (TextWriter writer = new StringWriter())
            {
                serializer.Serialize(writer, Schedules);
                sb.Append(writer.ToString());
            }  


            sb.Append("]");
            return sb.ToString();
        }

        public Target JobTarget { get; set; }

        public List<JobSchedule> Schedules { get; private set; }

        public static ArchiveJob LoadSchedule(string xmlSchedule)
        {
            var serializer = new XmlSerializer(typeof(ArchiveJob));
            using (TextReader reader = new StringReader(xmlSchedule))
            {
                return (ArchiveJob)serializer.Deserialize(reader);
            }
        }

        public static string SaveSchedule(ArchiveJob job)
        {
            var serializer = new XmlSerializer(typeof(ArchiveJob));
            using (TextWriter writer = new StringWriter())
            {
                serializer.Serialize(writer, job);
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
            JobTarget = (Target)info.GetValue("target", typeof(Target));
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
            info.AddValue("target", JobTarget);
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

            //- Read target
            reader.Read(); if (reader.Name != "Target") throw new Exception("Expecting Target node");
            JobTarget = new Target();
            JobTarget.ReadXml(reader);
            //reader.ReadEndElement();

            //- Read Schedule
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

            //- Write target
            writer.WriteStartElement("Target");
            JobTarget.WriteXml(writer);
            writer.WriteEndElement();

            //- Write schedule
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
