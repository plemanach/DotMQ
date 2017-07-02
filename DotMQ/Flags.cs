using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotMQ
{
    [Flags]
    public enum Flag
    {
        Retain = 0,
        Qos1 = 1 << 1,
        Qos2 = 1 << 2,
        Dup = 1 << 3
    }
}
