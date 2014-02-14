using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net.Config;

[assembly: CloudBackup.Test.Log4NetConf()]

namespace CloudBackup.Test
{
    [AttributeUsage(AttributeTargets.Assembly)]
    class Log4NetConf : Attribute
    {
        private const string MiniConf = @"
  <log4net>
    <appender name=""TraceAppender"" type=""log4net.Appender.TraceAppender"">
      <layout type=""log4net.Layout.PatternLayout"">
        <conversionPattern value=""%date [%-5thread] %-5level %logger - %message%newline""/>
      </layout>
      <filter type=""log4net.Filter.LevelRangeFilter"">
        <param name=""LevelMin"" value=""DEBUG""/>
        <param name=""LevelMax"" value=""FATAL""/>
      </filter>
    </appender>
    <root>
      <level value=""DEBUG""/>
      <appender-ref ref=""TraceAppender""/>
    </root>
  </log4net>
";


        public Log4NetConf()
        {
            using (var sr = new MemoryStream(Encoding.UTF8.GetBytes(MiniConf)))
            {
                XmlConfigurator.Configure(sr);    
            }
        }
    }

}
