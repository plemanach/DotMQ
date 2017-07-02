using System;
using System.IO;

namespace DotMQ.Extensions
{
    public static class BinaryWriterExtensions
    {
        static bool _LittleEndian;

        static bool IsBigEndian => !_LittleEndian;

        static BinaryWriterExtensions()
        {
            _LittleEndian = BitConverter.IsLittleEndian;
        }

        public static void WriteUInt16BE(this BinaryWriter binWri, ushort s)
        {
            binWri.Write(IsBigEndian ? s : Endian.SwapUInt16(s));
        }
    }


    public class Endian
    {
        public static UInt16 SwapUInt16(ushort x)
        {
            return (ushort)((ushort)((x & 0xff) << 8) | ((x >> 8) & 0xff));
        }
    }
}
