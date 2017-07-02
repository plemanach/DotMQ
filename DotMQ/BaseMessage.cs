using System.IO;
using DotMQ.Extensions;
using System.Text;

namespace DotMQ
{
    public class BaseMessage
    {
        public static void WriteString(string data, BinaryWriter bw)
        {
            bw.WriteUInt16BE((ushort)data.Length);
            bw.Write(UTF8Encoding.Default.GetBytes(data));
        }
    }
}
