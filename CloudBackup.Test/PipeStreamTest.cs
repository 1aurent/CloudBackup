using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CloudBackup.Utils;
using NUnit.Framework;

namespace CloudBackup.Test
{
    [TestFixture]
    public class PipeStreamTest
    {

        void WriteSide(object rawWrite)
        {
            var buffer = new byte[10*10*1024];
            using (var stream = (Stream) rawWrite)
            {
                stream.Write(buffer, 0,   512);
                stream.Write(buffer, 512, 512);
                stream.Write(buffer, 0, 10*1024);
            }
        }

        void ReadSide(object rawRead)
        {
            int total = 0;
            var buffer = new byte[1024];
            using (var stream = (Stream)rawRead)
            {
                int r;
                do
                {
                    r = stream.Read(buffer, 0, buffer.Length);
                    total += r;
                } while (r == buffer.Length);
            }

        }


        [Test]
        public void Test()
        {
            Stream write, read;

            PipeStream.CreatePipe(out write,out read);

            var writeThread = new Thread(WriteSide);
            var readThread = new Thread(ReadSide);

            readThread.Start(read);
            writeThread.Start(write);

            writeThread.Join();
            readThread.Join();
        }
    }
}
