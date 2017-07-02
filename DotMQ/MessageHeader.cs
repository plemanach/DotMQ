using System;
using System.IO;
using DotMQ.Extensions;

namespace DotMQ
{
    public class MessageHeader
    {
        public ControlPacketTypes PacketTypes { get; private set; }

        public byte Flags { get; private set; }

        public int Length { get; private set; }

        public ushort PacketIdentifier { get; private set; }

        private MessageHeader(ControlPacketTypes packetTypes, byte flags, int length)
        {
            PacketTypes = packetTypes;
            Flags = flags;
            Length = length;
        }

        private MessageHeader(
            ControlPacketTypes packetTypes,
            byte flags, 
            int length, 
            ushort packetId)
        {
            PacketTypes = packetTypes;
            Flags = flags;
            Length = length;
            PacketIdentifier = packetId;
        }

        public static MessageHeader Parse(BinaryReader br)
        {
            var fixHeader = br.ReadByte();
            var controlPacketType =  (ControlPacketTypes)(fixHeader >> 4);
            byte flags = (byte)(fixHeader & 127);
            var length = ReadVariableLength(br);
            ushort packetId = 0;
            bool packetIdFound = false;

            if (((flags & (byte)Flag.Qos1) > 0) || ((flags & (byte)Flag.Qos2) > 0))//TODO check condition
            {
                switch (controlPacketType)//TODO check condition
                {
                    case ControlPacketTypes.SUBSCRIBE:
                    case ControlPacketTypes.PUBLISH:
                    case ControlPacketTypes.UNSUBSCRIBE:
                        packetId = br.ReadUInt16BE();
                        packetIdFound = true;
                        break;
                }
            }

            if (!packetIdFound)
            {
                return new MessageHeader(controlPacketType, flags, length);
            }
            else
            {
                return new MessageHeader(controlPacketType, flags, length, packetId);
            }
            
        }

        private static int ReadVariableLength(BinaryReader br)
        {
            int multiplier = 1;
            int value = 0;
            byte encodedByte;
            do
            {
                encodedByte = br.ReadByte();
                value += (encodedByte & 127) * multiplier;
                multiplier *= 128;
                if (multiplier > 128 * 128 * 128)
                    throw new Exception("Malformed Remaining Length");
            } while ((encodedByte & 128) != 0);

            return value;
        }
    }
}
