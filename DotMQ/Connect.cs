
using System.IO;
using DotMQ.Extensions;
using System.Text;

namespace DotMQ
{
    public class Connect : IMessage
    {
        public MessageHeader Header { get; private set; }
        public ConnectVariableHeader VariableHeader { get; private set; }
        public string ClientId { get; private set; }
        public string WillTopic { get; private set; }
        public string WillMessage { get; private set; }
        public string UserName { get; private set; }
        public string Password { get; private set; }

        public Connect(MessageHeader header)
        {
            Header = header;
        }

        public void Parse(BinaryReader br)
        {
            VariableHeader = new ConnectVariableHeader();
            VariableHeader.Parse(br);
            var clientIdLength = br.ReadUInt16BE();
            byte[] clientIdByte = br.ReadBytes(clientIdLength);
            ClientId = UTF8Encoding.Default.GetString(clientIdByte);

            if ((VariableHeader.ConnectFlags & ConnectFlags.WillFlag1) == ConnectFlags.WillFlag1)
            {
                var willTopicLength = br.ReadUInt16BE();
                byte[] willTopicByte = br.ReadBytes(willTopicLength);
                WillTopic = UTF8Encoding.Default.GetString(willTopicByte);

                var willMessageLength = br.ReadUInt16BE();
                byte[] willMessageByte = br.ReadBytes(willMessageLength);
                WillMessage = UTF8Encoding.Default.GetString(willTopicByte);
            }

            if ((VariableHeader.ConnectFlags & ConnectFlags.UserNameFlag) == ConnectFlags.UserNameFlag)
            {
                var userNameLength = br.ReadUInt16BE();
                byte[] userNameByte = br.ReadBytes(userNameLength);
                UserName = UTF8Encoding.Default.GetString(userNameByte);
            }

            if ((VariableHeader.ConnectFlags & ConnectFlags.PasswordFlag) == ConnectFlags.PasswordFlag)
            {
                var passwordLength = br.ReadUInt16BE();
                byte[]passwordByte = br.ReadBytes(passwordLength);
                Password = UTF8Encoding.Default.GetString(passwordByte);
            }
        }
    }
}



