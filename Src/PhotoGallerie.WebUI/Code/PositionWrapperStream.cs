using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace PhotoGalerie
{
    public class PositionWrapperStream : Stream
    {
        private readonly Stream wrapped;

        private int pos = 0;

        public PositionWrapperStream(Stream wrapped)
        {
            this.wrapped = wrapped;
        }

        public override bool CanSeek { get { return false; } }

        public override bool CanWrite { get { return true; } }

        public override long Position
        {
            get { return pos; }
            set { throw new NotSupportedException(); }
        }

        public override bool CanRead
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override long Length
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            pos += count;
            wrapped.Write(buffer, offset, count);
        }

        public override void Flush()
        {
            wrapped.Flush();
        }

        protected override void Dispose(bool disposing)
        {
            wrapped.Dispose();
            base.Dispose(disposing);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }

        // all the other required methods can throw NotSupportedException
    }
}