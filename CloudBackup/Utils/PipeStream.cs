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
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace CloudBackup.Utils
{

    public class PipeStream
    {
        const int WriteBufferSize = 16384*128;

        readonly object _lock = new object();
        readonly LinkedList<byte[]> _buffers;
        byte[] _writeBuffer;
        int _writeBufferPos;
        byte[] _readBuffer;
        int _readBufferPos;
        bool _isClosed = false;

        private PipeStream()
        {
            _buffers = new LinkedList<byte[]>();
            _writeBufferPos = 0;
            _writeBuffer = new byte[WriteBufferSize];
        }


        public void PushBuffer(byte[] buffer, int offset, int count)
        {
            lock (_lock) { while (_buffers.Count > 10) Monitor.Wait(_lock); }

            if (count >= WriteBufferSize && _writeBufferPos == 0) goto fastFlush;

            if ((count + _writeBufferPos) > WriteBufferSize)
            {
                var nbA = WriteBufferSize - _writeBufferPos;
                Buffer.BlockCopy(buffer, offset, _writeBuffer, _writeBufferPos,nbA);
                //Trace.WriteLine(string.Format("Push {0} - A",_writeBuffer.Length));
                lock (_lock) { _buffers.AddLast(_writeBuffer); Monitor.Pulse(_lock); }
                _writeBufferPos = 0;
                _writeBuffer = new byte[WriteBufferSize];

                count -= nbA;
                offset += nbA;
            }
        fastFlush:
            if (count >= WriteBufferSize)
            {
                var block = new byte[count];
                Buffer.BlockCopy(buffer, offset, block, 0, count);
                //Trace.WriteLine(string.Format("Push {0} - B", block.Length));
                lock (_lock) { _buffers.AddLast(block); Monitor.Pulse(_lock); }
            }
            else
            {
                Buffer.BlockCopy(buffer, offset, _writeBuffer, _writeBufferPos, count);
                _writeBufferPos += count;
            }
        }

        public int PopBuffer(byte[] buffer, int offset, int count)
        {
            int totalReaded = 0;
            do
            {
                if (_readBuffer == null)
                {
                    lock (_lock)
                    {
                        while (_buffers.First == null && !_isClosed) Monitor.Wait(_lock);
                        if (_buffers.First == null && _isClosed) return totalReaded;
                        // ReSharper disable once PossibleNullReferenceException
                        _readBuffer = _buffers.First.Value;
                        _buffers.RemoveFirst();
                        _readBufferPos = 0;
                        Monitor.Pulse(_lock);
                    }
                }

                var toCopy = Math.Min(count, _readBuffer.Length - _readBufferPos);
                Buffer.BlockCopy(_readBuffer, _readBufferPos, buffer, offset, toCopy);
                totalReaded += toCopy;
                _readBufferPos += toCopy;
                count -= toCopy;
                offset += toCopy;
                if (_readBufferPos >= _readBuffer.Length) _readBuffer = null;
            } while (count > 0);
            return totalReaded;
        }

        public void NotifyClose()
        {
            if (_writeBufferPos != 0)
            {
                var block = new byte[_writeBufferPos];
                Buffer.BlockCopy(_writeBuffer, 0, block, 0, _writeBufferPos);
                //Trace.WriteLine(string.Format("Push {0} - END", block.Length));
                lock (_lock) { _buffers.AddLast(block); _isClosed = true; Monitor.Pulse(_lock); }
                return;
            }
            //Trace.WriteLine("Push ZERO - END");
            lock (_lock) { _isClosed = true; Monitor.Pulse(_lock); }
        }

        class ReadStream : Stream
        {
            #region <Not implemented methods>
            public override void Flush() {}
            public override long Seek(long offset, SeekOrigin origin) { throw new NotImplementedException(); }
            public override void SetLength(long value) {throw new NotImplementedException();}
            public override void Write(byte[] buffer, int offset, int count) { throw new NotImplementedException(); }
            public override long Length { get { throw new NotImplementedException(); } }

            public override bool CanRead { get { return true; } }
            public override bool CanSeek { get { return false; } }
            public override bool CanWrite { get { return false; } }
            #endregion

            public override long Position { get { return _position; } set { throw new NotImplementedException(); } }

            readonly PipeStream _buffer;
            long _position = 0;
            public ReadStream(PipeStream buffer) { _buffer = buffer; }

            public override int Read(byte[] buffer, int offset, int count)
            {
                var ret = _buffer.PopBuffer(buffer, offset, count);
                _position += ret;
                return ret;
            }
        }

        class WriteStream : Stream
        {
            #region <Not implemented methods>
            public override void Flush() { }
            public override long Seek(long offset, SeekOrigin origin) { throw new NotImplementedException(); }
            public override void SetLength(long value) { throw new NotImplementedException(); }
            public override int Read(byte[] buffer, int offset, int count) { throw new NotImplementedException(); }
            public override long Length { get { throw new NotImplementedException(); } }

            public override bool CanRead { get { return false; } }
            public override bool CanSeek { get { return false; } }
            public override bool CanWrite { get { return true; } }
            #endregion

            public override long Position { get { return _position; } set { throw new NotImplementedException(); } }

            readonly PipeStream _buffer;
            long _position=0;
            public WriteStream(PipeStream buffer) { _buffer = buffer; }

            protected override void Dispose(bool disposing)
            {
                _buffer.NotifyClose();
                base.Dispose(disposing);
            }

            public override void Write(byte[] buffer, int offset, int count)
            {
                _buffer.PushBuffer(buffer,offset,count);
                _position += count;
            }
        }

        public static void CreatePipe(out Stream write, out Stream read)
        {
            var buffer = new PipeStream();
            write = new WriteStream(buffer);
            read = new ReadStream(buffer);
        }
    }
}
