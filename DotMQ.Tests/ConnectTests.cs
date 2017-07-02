using NUnit.Framework;
using DotMQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotMQ.Tests
{
    [TestFixture]
    public class ConnectTests : BaseTests
    {

        [TestCase]
        public void Parse_ValidMessage_returnValidObject()
        {
            //Arrange
            const ushort expectedPacketId = 5;
            byte[] header = CreateMessage(ControlPacketTypes.PUBLISH, 10, null, (byte)Flag.Qos1, expectedPacketId);
        }
    }
}
