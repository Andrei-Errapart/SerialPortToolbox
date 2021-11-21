using System;
using System.Collections.Generic;
using System.Text;

namespace SerialProtocolLogger
{

    /** Geotracer packet representation. */
    class GeotracerLinePacket
    {
        public enum TYPE {
            READ,
            WRITE,
            DATA
        }
        public TYPE Type;
        public int Address;
        public string Payload;

        public enum ADDR
        {
            FIRMWARE_VERSION = 130,
        }
        public GeotracerLinePacket(TYPE type, int address, string payload)
        {
            Type = type;
            Address = address;
            Payload = payload;
        }

        public override string ToString()
        {
            switch (Type)
            {
                case TYPE.READ:
                    return "RN," + Address;
                case TYPE.WRITE:
                    return "WN," + Address + "=" + Payload;
                case TYPE.DATA:
                    return Address.ToString() + "=" + Payload;
            }
            return "";
        }
    }

    /// <summary>
    /// Assemble Geotracer packets.
    /// </summary>
    class GeotracerLineAssembler
    {
        /// <summary>
        /// Input data buffer.
        /// </summary>
        private List<byte> buffer_ = new List<byte>();

        /// <summary>
        /// Output queue.
        /// </summary>
        private Queue<GeotracerLinePacket> queue_ = new Queue<GeotracerLinePacket>();

        /// <summary>
        /// Maximum packet size (i.e. line length), bytes.
        /// </summary>
        public const int MAX_PACKET_SIZE = 1024;

        /// <summary>
        /// Feed some data in hope to complete some packets.
        /// </summary>
        /// <param name="data">Data bytes.</param>
        /// <param name="length">Data length.</param>
        public void Feed(byte[] data, int length)
        {
            int i;

            for (i = 0; i < length; ++i)
            {
                buffer_.Add(data[i]);
            }
            int n = buffer_.Count;
            string sbuffer = Encoding.ASCII.GetString(buffer_.ToArray(), 0, n);

            int so_far = 0;
            int consumed_count = 0;
            // Minimum command length:
            // addr + '=' + '\r' = 3 bytes.
            while (so_far+3<n)
            {
                int startpos = so_far;
                bool is_read = sbuffer[startpos] == 'R' && sbuffer[startpos + 1] == 'N' && sbuffer[startpos+2] == ',';
                bool is_write = sbuffer[startpos] == 'W' && sbuffer[startpos + 1] == 'N' && sbuffer[startpos + 2] == ',';
                bool is_data = Char.IsDigit(sbuffer[startpos]);
                bool is_any = is_read || is_write || is_data;
                int cr_pos = sbuffer.IndexOf('\r', startpos + 1);
                int eq_pos = sbuffer.IndexOf('=', startpos + 1);

                // anyone?
                if (!is_any)
                {
                    ++so_far;
                    continue;
                }

                // Do we have a command without ending?
                if (cr_pos < 0)
                {
                    break;
                }

                // Do we have write or data command without eq-sign?
                if ((is_write || is_data) && eq_pos < 0)
                {
                    so_far = cr_pos + 1;
                    continue;
                }

                try
                {
                    if (is_read)
                    {
                        int ndigits = cr_pos-startpos-3;
                        int addr = int.Parse(sbuffer.Substring(startpos + 3, ndigits));
                        queue_.Enqueue(new GeotracerLinePacket(GeotracerLinePacket.TYPE.READ, addr, ""));
                        consumed_count = cr_pos + 1;
                    }
                    else if (is_write)
                    {
                        int ndigits = eq_pos - startpos - 3;
                        int npayload = cr_pos - eq_pos - 1;
                        int addr = int.Parse(sbuffer.Substring(startpos + 3, ndigits));
                        string payload = sbuffer.Substring(eq_pos+1, npayload);
                        queue_.Enqueue(new GeotracerLinePacket(GeotracerLinePacket.TYPE.WRITE, addr, payload));
                        consumed_count = cr_pos + 1;
                    }
                    else if (is_data)
                    {
                        int ndigits = eq_pos - startpos;
                        int npayload = cr_pos - eq_pos - 1;
                        int addr = int.Parse(sbuffer.Substring(startpos, ndigits));
                        string payload = sbuffer.Substring(eq_pos + 1, npayload);
                        queue_.Enqueue(new GeotracerLinePacket(GeotracerLinePacket.TYPE.DATA, addr, payload));
                        consumed_count = cr_pos + 1;
                    }
                }
                catch (Exception)
                {
                    // Pass.
                }
                so_far = cr_pos + 1;
            }

            // Ditch the front.
            if (consumed_count > 0)
            {
                buffer_.RemoveRange(0, consumed_count);
            }
        }

        /// <summary>
        /// Pop packet off the queue.
        /// </summary>
        /// <returns>New GeotracerPacket, or null if there is none.</returns>
        public GeotracerLinePacket Pop()
        {
            if (queue_.Count > 0)
            {
                return queue_.Dequeue();
            }
            return null;
        }
    }
}
