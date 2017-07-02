using System.IO;
using System.Text;
using DotMQ.Extensions;

namespace DotMQ
{
    public class ConnectVariableHeader : BaseMessage, IMessage
    {
        public string ProtocolName { get; private set; }

        public byte ProtocolLevel { get; private set; }

        public ConnectFlags ConnectFlags { get; private set; }

        public ushort KeepAlive { get; private set; }

        public void Parse(BinaryReader br)
        {
            var protocolLength = br.ReadUInt16BE();
            byte[] protocolByte = br.ReadBytes(protocolLength);
            ProtocolName = UTF8Encoding.Default.GetString(protocolByte);
            if (ProtocolName != "MQTT")
            {
                throw new DotMQExceptionUnknownProtocol();
            }
            ProtocolLevel = br.ReadByte();//TODO check level
            ConnectFlags = (ConnectFlags)br.ReadByte();
            KeepAlive = br.ReadUInt16BE();
        }

        public void Write(BinaryWriter bw)
        {
            //Write Protocol Name
            WriteString(ProtocolName, bw);

            //Write Protocol level
            bw.Write(ProtocolLevel);

            //Write ConnectFlags
            bw.Write((byte)ConnectFlags);

            bw.WriteUInt16BE(KeepAlive);
        }
    }
    
}
