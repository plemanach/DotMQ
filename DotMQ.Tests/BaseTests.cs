using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotMQ.Tests
{
    public class BaseTests
    {

       protected static byte[] CreateConnectMessage(
           ControlPacketTypes packetType, 
           byte? flags = null, 
           ushort? packetId = null)
       {
            return null;
       }

        protected static byte[] CreateMessage(
            ControlPacketTypes packetType,
            byte length1,
            byte? length2 = null,
            byte? flags = null, ushort? packetId = null)
        {
            byte[] fixHeader = new byte[length2.HasValue ? 3 : 2];
            fixHeader[0] = (byte)((byte)packetType << 4);

            if (flags.HasValue)
            {
                fixHeader[0] = (byte)(fixHeader[0] | flags);
            }


            fixHeader[1] = length1;

            if (length2.HasValue)
            {
                fixHeader[2] = length2.Value;
            }


            if (packetId.HasValue)
            {
                int oldLength = fixHeader.Length;
                Array.Resize(ref fixHeader, fixHeader.Length + 2);
                fixHeader[oldLength] = (byte)(packetId >> 8);
                fixHeader[oldLength + 1] = (byte)(packetId);
            }
            return fixHeader;
        }
    }
}
