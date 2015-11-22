using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks {
    class ByteBuffer {
        private byte[] buffer;

        public int limit {
            get; private set;
        }

        public int position {
            get; private set;
        }

        public static ByteBuffer allocate(int capacity) {
            ByteBuffer b = new ByteBuffer();
            b.buffer = new byte[capacity];
            b.limit = 0;
            return b;
        }

        public void flip() {
            limit = position;
            position = 0;
        }

        public void clear() {
            limit = buffer.Length;
            position = 0;
        }

        public void put(byte b) {
            if (position >= limit)
                throw new BufferException();

            buffer[position] = b;
            position++;
        }

        public byte get() {
            if (position >= limit)
                throw new BufferException();

            return buffer[position++];
        }

        public byte get(int i) {
            if (i >= limit)
                throw new BufferException();

            return buffer[i];
        }

        public byte[] getArray() {
            return buffer;
        }

    }
}
