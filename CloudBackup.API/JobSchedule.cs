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
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace CloudBackup.API
{
    public enum JobScheduleType
    {
        Daily,
        Weekly,
        Monthly
    }

    [Flags]
    public enum JobDays
    {
        Monday=1,
        Tuesday=2,
        Wednesday=4,
        Thursday=8,
        Friday=16,
        Saturday=32,
        Sunday=64
    }

    public enum JobRelativeTo
    {
        BeginOfTheMonth,
        EndOfTheMonth,
        FirstSundayOfTheMonth
    }

    [Serializable]
    public struct HourMinute
    {
        public int Hour;
        public int Minute;

        public override string ToString()
        {
            return string.Format("{0:00}:{1:00}",Hour,Minute);
        }

        public static HourMinute Parse(string text)
        {
            var rx = new Regex("^([0-9]+):([0-9]+)$",RegexOptions.Singleline);
            var m = rx.Match(text);
            if (!m.Success) throw new Exception("Invalid HourMinute string");
            return new HourMinute
            {
                Hour = int.Parse(m.Groups[1].Value),
                Minute = int.Parse(m.Groups[2].Value)
            };
        }
    }

    [Serializable]
    public class JobSchedule : ISerializable, IXmlSerializable 
    {
        public JobSchedule()
        {
            ScheduleType = JobScheduleType.Weekly;
            Daily = new DailySchedule();
            Weekly = new WeeklySchedule();
            Monthly = new MonthlySchedule();
        }

        public JobScheduleType ScheduleType { get; set; }
        public bool ForceFullBackup { get; set; }

        public DailySchedule Daily     { get; private set; }
        public WeeklySchedule  Weekly  { get; private set; }
        public MonthlySchedule Monthly { get; private set; }

        public override string ToString()
        {
            var type = ForceFullBackup ? "Full " : "Incr ";
            switch (ScheduleType)
            {
            case JobScheduleType.Daily:
                    return string.Concat(type,"Daily ", Daily.ToString());
            case JobScheduleType.Weekly:
                    return string.Concat(type, "Weekly ", Weekly.ToString());
            case JobScheduleType.Monthly:
                    return string.Concat(type, "Monthly ", Monthly.ToString());
            }
            return "Invalid " + ScheduleType;
        }

        protected JobSchedule(SerializationInfo info, StreamingContext context)
        {
            ScheduleType = (JobScheduleType)Enum.Parse(typeof(JobScheduleType), info.GetString("type"));
            ForceFullBackup = info.GetBoolean("fullBackup");
            Daily = (DailySchedule) info.GetValue("daily", typeof (DailySchedule));
            Weekly = (WeeklySchedule)info.GetValue("weekly", typeof(WeeklySchedule));
            Monthly = (MonthlySchedule)info.GetValue("monthly", typeof(MonthlySchedule));            
        }
        [SecurityPermissionAttribute(SecurityAction.Demand,SerializationFormatter = true)]
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("type",  ScheduleType.ToString());
            info.AddValue("fullBackup",ForceFullBackup);
            info.AddValue("daily", Daily);
            info.AddValue("weekly", Weekly);
            info.AddValue("monthly", Monthly);
        }

        public XmlSchema GetSchema() { return null; }

        public void ReadXml(XmlReader reader)
        {
            ScheduleType = (JobScheduleType) Enum.Parse(typeof (JobScheduleType),
                reader.GetAttribute("type") ?? "Weekly",
                true);
            ForceFullBackup = XmlConvert.ToBoolean(reader.GetAttribute("fullBackup") ?? "False");

            reader.Read(); if(reader.Name!="Daily") throw new Exception("Expecting Daily node");
            Daily.RunAt = HourMinute.Parse(reader.GetAttribute("runAt"));
            Daily.Every = int.Parse(reader.GetAttribute("every")??"1");
            reader.Read(); if (reader.Name != "Weekly") throw new Exception("Expecting Weekly node");
            Weekly.RunAt = HourMinute.Parse(reader.GetAttribute("runAt"));
            Weekly.Every = int.Parse(reader.GetAttribute("every") ?? "1");
            var iJobDays = int.Parse(reader.GetAttribute("days") ?? "0");
            Weekly.JobDays = (JobDays) iJobDays;
            reader.Read(); if (reader.Name != "Monthly") throw new Exception("Expecting Monthly node");
            Monthly.RunAt = HourMinute.Parse(reader.GetAttribute("runAt"));
            Monthly.Every = int.Parse(reader.GetAttribute("every") ?? "1");
            Monthly.RelativeTo = (JobRelativeTo)Enum.Parse(typeof(JobRelativeTo),
                reader.GetAttribute("relTo") ?? "BeginOfTheMonth",
                true);
            Monthly.DayOffset = int.Parse(reader.GetAttribute("offset") ?? "0");
            reader.Read();
            reader.ReadEndElement();
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString("type", ScheduleType.ToString());
            writer.WriteAttributeString("fullBackup", XmlConvert.ToString(ForceFullBackup));
            writer.WriteStartElement("Daily");
            writer.WriteAttributeString("runAt", Daily.RunAt.ToString());
            writer.WriteAttributeString("every", Daily.Every.ToString());
            writer.WriteEndElement();
            writer.WriteStartElement("Weekly");
            writer.WriteAttributeString("runAt", Weekly.RunAt.ToString());
            writer.WriteAttributeString("every", Weekly.Every.ToString());
            writer.WriteAttributeString("days",  ((int)Weekly.JobDays).ToString());
            writer.WriteEndElement();
            writer.WriteStartElement("Monthly");
            writer.WriteAttributeString("runAt", Monthly.RunAt.ToString());
            writer.WriteAttributeString("every", Monthly.Every.ToString());
            writer.WriteAttributeString("relTo", Monthly.RelativeTo.ToString());
            writer.WriteAttributeString("offset", Monthly.DayOffset.ToString());
            writer.WriteEndElement();
        }
    }

    [Serializable]
    public class DailySchedule
    {
        public DailySchedule()
        {
            Every = 1;
        }

        public HourMinute RunAt { get; set; }
        public int Every { get; set; }

        public override string ToString()
        {
            return string.Format("{0} every {1} day{2}",
                RunAt,Every,Every==1?"":"s");
        }
    }

    [Serializable]
    public class WeeklySchedule
    {
        public WeeklySchedule()
        {
            JobDays = JobDays.Monday |
                JobDays.Tuesday |
                JobDays.Wednesday |
                JobDays.Thursday |
                JobDays.Friday;
            Every = 1;
        }

        public HourMinute RunAt { get; set; }
        public int Every { get; set; }
        public JobDays JobDays { get; set; }

        public override string ToString()
        {
            return string.Format("{0} every {1} week{2}",
                RunAt, Every, Every == 1 ? "" : "s");
        }
    }

    [Serializable]
    public class MonthlySchedule
    {
        public MonthlySchedule()
        {
            Every = 1;
        }

        public HourMinute RunAt { get; set; }
        public int Every { get; set; }
        public JobRelativeTo RelativeTo { get; set; }
        public int DayOffset { get; set; }

        public override string ToString()
        {
            return string.Format("{0} every {1} month{2}",
                RunAt, Every, Every == 1 ? "" : "s");
        }
    }

}
