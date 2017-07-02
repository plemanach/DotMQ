using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotMQ
{
    [Flags]
    public enum ConnectFlags
    {
        Reserved = 0,
        CleanSession = 1 << 1,
        WillFlag1 = 1 << 2,
        WillQoS1 = 1 << 3,
        WillQoS2 = 1 << 4,
        WillRetain = 1 << 5,
        PasswordFlag = 1 << 6,
        UserNameFlag = 1 << 7,
    }
}
