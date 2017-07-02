using System;

namespace DotMQ
{
    public enum ControlPacketTypes : byte
    {
        Reserved,
        CONNECT,
        CONNACK,
        PUBLISH,
        PUBACK,
        PUBREC,
        PUBREL,
        PUBCOMP,
        SUBSCRIBE,
        SUBACK,
        UNSUBSCRIBE,
        UNSUBACK,
        PINGREQ,
        PINGRESP,
        DISCONNECT,
        Reserved2
    }
}
