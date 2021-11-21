using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace SerialProtocolLogger
{

    /** Geotracer binary packet representation. */
    class GeotracerPacket
    {
        // public const byte PACKET_START = 0x41;
        public const byte PACKET_END = 0x0A;

        public const string AWRITE = "AWRITE";
        public const string AFILEDATA = "AFILEDATA_";
        public const string CU = "CU";
        public const string AVERSION = "AVERSION__";
#if (false)
        public enum TYPE {
            /// <summary>
            /// Rover says hello to the GPS.
            /// "AROVER 5"
            /// </summary>
            ROVER,
            /// <summary>
            /// Rover asks??
            /// 3F 3F 0A
            /// </summary>
            HELLO,
            /// <summary>
            /// "AOPEN 3.0/GEOGPS.CFG rb"
            /// </summary>
            OPEN,
            /// <summary>
            /// 41 46 49 4C 45 48 41 4E 44 4C 20 00 00 03 E7 CC 0A
            /// A  F   I    L   E  H  A  N   D  L                    999  cc
            /// </summary>
            FILEHANDLE,
            READ,
            WRITE,
            CLOSE,
            INFO,
            GETDISK,
            FREE,
            OBSSTART,
            DDSTART,
            /// <summary>
            /// 41 45 52 52 4F 52 43 4F 44 45 20 00 34 0A
            /// "AERRORCODE "
            /// </summary>
            ERRORCODE
        }
#endif

        public string Type;
        public byte[] Payload;

        public GeotracerPacket(string type, byte[] payload)
        {
            Type = type;
            Payload = payload;
        }

        private int NSkipAndText(StringBuilder sb)
        {
            sb.Append(Type);
            int nskip = 0;
            if (Type == AFILEDATA)
            {
                // int datalength = 256 * buffer_[startpos + 11] + buffer_[startpos + 12];
                // total length = "AFILEDATA_" + space + length  + data + checksum + LF.
                // int total_length = 10 + 1 + 2 + datalength:2 + 2;
                int datalength = 256 * Payload[0] + Payload[1];
                nskip = 4;
                sb.Append(String.Format(" L={0}", datalength));
            }
            else if (Type == AWRITE)
            {
                // int datalength = 256 * buffer_[startpos + 12] + buffer_[startpos + 13];
                // total length = "AWRITE" + space + handle:4 + length:2  + data + checksum + LF.
                // int total_length = 6 + 1 + 4 + 1 + 2 + datalength + 2;
                int handle = 16777216 * Payload[0] + 65536 * Payload[1] + 256 * Payload[2] + Payload[3];
                int datalength = 256 * Payload[5] + Payload[6];
                nskip = 7;
                sb.Append(String.Format(" L={0} H=0x{1:X}", datalength, handle));
            }
            return nskip;
        }

        public override string ToString()
        {
            if (Payload == null || Payload.Length == 0)
            {
                return Type;
            }
            StringBuilder sb = new StringBuilder();
            int nskip = NSkipAndText(sb); // number of payload bytes to skip;
            foreach (byte b in Payload)
            {
                if (nskip <= 0)
                {
                    sb.Append(String.Format(" {0:X2}", b));
                }
                else
                {
                    --nskip;
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// Convert packet to binary.
        /// </summary>
        /// <returns>Binary representation of the packet.</returns>
        public byte[] ToBinary()
        {
            return null;
        }

        public void ToFile(string filename)
        {
            using (StreamWriter fout = new System.IO.StreamWriter(filename))
            {
                if (Payload == null || Payload.Length == 0)
                {
                    fout.WriteLine(Type);
                }
                else
                {
                    StringBuilder sb = new StringBuilder();
                    int nskip = NSkipAndText(sb); // number of payload bytes to skip;
                    fout.WriteLine(sb.ToString());
                    int col = 0;
                    int ntotal = 0;
                    foreach (byte b in Payload)
                    {
                        if (nskip <= 0)
                        {
                            if (col == 0)
                            {
                                fout.Write(String.Format("0x{0:x2}", ntotal));
                            }
                            fout.Write(String.Format(" {0:X2}", b));
                            ++ntotal;
                            ++col;
                            if (col >= 8)
                            {
                                fout.WriteLine();
                                col = 0;
                            }
                        }
                        else
                        {
                            --nskip;
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// Assemble binary Geotracer packets from the stream.
    /// </summary>
    class GeotracerAssembler
    {
        /// <summary>
        /// Input data buffer.
        /// </summary>
        private List<byte> buffer_ = new List<byte>();

        /// <summary>
        /// Output queue.
        /// </summary>
        private Queue<GeotracerPacket> queue_ = new Queue<GeotracerPacket>();

#if (false)
        private static char[] ASCII = {
        };
#endif

        /// <summary>
        /// Maximum packet size (i.e. line length), bytes.
        /// </summary>
        public const int MAX_PACKET_SIZE = 4096;

        private const string CHARS_FIRST = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string CHARS_OTHERS = "_0123456789";

        /// <summary>
        /// Feed some data in hope to complete some packets.
        /// </summary>
        /// <param name="data">Data bytes.</param>
        /// <param name="length">Data length.</param>
        public void Feed(byte[] data, int length)
        {
            int i;
            for (int dataIndex = 0; dataIndex < length; ++dataIndex)
            {
                buffer_.Add(data[dataIndex]);
            }
            int n = buffer_.Count;
            string sbuffer = Encoding.ASCII.GetString(buffer_.ToArray(), 0, n);

            int so_far = 0;
            int consumed_count = 0;
            // Minimum command length:
            // addr + '=' + '\r' = 3 bytes.
            while (so_far+3<n)
            {
                int skipped_start = so_far;
                // Find first character...
                while (so_far < n && CHARS_FIRST.IndexOf(sbuffer[so_far]) < 0)
                {
                    ++so_far;
                }
                if (so_far >= n)
                {
                    break;
                }
                int startpos = so_far;

                // Find command end position.
                int cmd_endpos = startpos + 1;  // first character after command.
                while (cmd_endpos < n && (CHARS_FIRST.IndexOf(sbuffer[cmd_endpos]) >= 0 || CHARS_OTHERS.IndexOf(sbuffer[cmd_endpos])>=0))
                {
                    ++cmd_endpos;
                }
                if (cmd_endpos >= n)
                {
                    break;
                }

                // Command is finished :)
                string cmd = sbuffer.Substring(startpos, cmd_endpos - startpos);

                // Find end of command and payload.
                int endpos = cmd_endpos + 1;
                if (cmd == GeotracerPacket.AFILEDATA)
                {
                    if (startpos + 13 >= n)
                    {
                        break;
                    }
                    int datalength = 256 * buffer_[startpos + 11] + buffer_[startpos + 12];
                    // total length = "AFILEDATA_" + space + length  + data + checksum + LF.
                    int total_length = buffer_[startpos + 13] == 0x0D
                        ? 10 + 1 + 2 + 1
                        : 10 + 1 + 2 + datalength + 2;
                    endpos = startpos + total_length;
                }
                else if (cmd == GeotracerPacket.AWRITE)
                {
                    //      0  1  2  3  4  5  6  7  8  9 10 11 12 13 14
                    //L8:	41 57 52 49 54 45 20 00 00 03 E7 20 03 65 34 00 00 00 00 00 00 00 00 00 
                    //      A  W  R  I  T  E            999      869 
                    if (startpos + 14 >= n)
                    {
                        break;
                    }
                    int datalength = 256 * buffer_[startpos + 12] + buffer_[startpos + 13];
                    // total length = "AWRITE" + space + handle:4 + space + length:2  + data + checksum + LF.
                    int total_length = 6 + 1 + 4 + 1 + 2 + datalength + 2;
                    endpos = startpos + total_length;
                }
                else if (cmd == GeotracerPacket.CU)
                {
                    int nsats = buffer_[startpos + 2];
                    int total_length = 31 + nsats * 6 + 1;
                    endpos = startpos + total_length;
                    --cmd_endpos;
                }
                else
                {
                    while (endpos < n && buffer_[endpos] != GeotracerPacket.PACKET_END)
                    {
                        ++endpos;
                    }
                    ++endpos;
                }
                if (endpos > n)
                {
                    break;
                }

                try
                {
                    if (skipped_start < startpos)
                    {
                        int nskipped = startpos - skipped_start;
                        byte[] skipped = new byte[nskipped];
                        for (i=0; i<nskipped; ++i) {
                            skipped[i] = buffer_[skipped_start + i];
                        }
                        queue_.Enqueue(new GeotracerPacket("SKIPPED", skipped));
                    }
                    int npayload = endpos - cmd_endpos - 1;
                    byte[] payload = null;
                    if (npayload > 0)
                    {
                        payload = new byte[npayload];
                        for (i = 0; i < npayload; ++i)
                        {
                            payload[i] = buffer_[cmd_endpos + i + 1];
                        }
                    }
                    GeotracerPacket packet = new GeotracerPacket(cmd, payload);
                    queue_.Enqueue(packet);
                }
                catch (Exception ex)
                {
                    // Pass.
                }
                so_far = endpos;
                consumed_count = endpos;
            }

            // Ditch the front.
            if (consumed_count > 0)
            {
                buffer_.RemoveRange(0, consumed_count);
            }
        }

        /// <summary>
        /// Feed some data in hope to complete some packets.
        /// </summary>
        /// <param name="data">Data bytes.</param>
        public void Feed(byte[] data)
        {
            Feed(data, data.Length);
        }

        /// <summary>
        /// Pop packet off the queue.
        /// </summary>
        /// <returns>New GeotracerPacket, or null if there is none.</returns>
        public GeotracerPacket Pop()
        {
            if (queue_.Count > 0)
            {
                return queue_.Dequeue();
            }
            return null;
        }
    }
}

