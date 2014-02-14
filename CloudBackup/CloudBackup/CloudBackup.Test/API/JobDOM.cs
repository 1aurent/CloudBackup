using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using CloudBackup.API;
using NUnit.Framework;

namespace CloudBackup.Test.API
{
    [TestFixture]
    public class JobDOM
    {
        [Test]
        public void TestHMParser()
        {
            var hm = new HourMinute();
            hm = HourMinute.Parse("12:3");
            Assert.AreEqual(hm.Hour,12);
            Assert.AreEqual(hm.Minute, 3);
        }

        [Test]
        public void SaveSchedule()
        {
            var schedule = new ArchiveJob();
            schedule.Schedules.Add(new JobSchedule{ScheduleType = JobScheduleType.Daily});

            string result;
            var serializer = new XmlSerializer(typeof (ArchiveJob));
            using (TextWriter writer = new StringWriter())
            {
                serializer.Serialize(writer,schedule);
                result= writer.ToString();
            }

            ArchiveJob decSchedule;
            serializer = new XmlSerializer(typeof(ArchiveJob));
            using (TextReader reader = new StringReader(result))
            {
                decSchedule = (ArchiveJob) serializer.Deserialize(reader);
            }
        }
    }
}
