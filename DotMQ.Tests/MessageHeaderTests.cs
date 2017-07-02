using NUnit.Framework;
using System;
using System.IO;
namespace DotMQ.Tests
{
    
    [TestFixture]
    public class MessageHeaderTests : BaseTests
    {
        [TestCase]
        public void Parse_ValidHeader_ReturnObject()
        {
            //Arrange
            byte[] header = CreateMessage(ControlPacketTypes.CONNECT, 54);
            MessageHeader actualHeader;

            var stream = new MemoryStream(header);
            using (BinaryReader br = new BinaryReader(stream))
            {
                actualHeader = MessageHeader.Parse(br);
            }

            //Assert
            Assert.IsNotNull(actualHeader);
            Assert.IsTrue(actualHeader.PacketTypes == ControlPacketTypes.CONNECT);
        }

        

        [TestCase]
        public void Parse_ValidHeader_With_SimpleLength_ReturnObject()
        {
            //Arrange
            const int expectedLength = 56;
            byte[] header = CreateMessage(ControlPacketTypes.CONNECT, expectedLength);
            MessageHeader actualHeader;

            var stream = new MemoryStream(header);
            //Act
            using (BinaryReader br = new BinaryReader(stream))
            {
                actualHeader = MessageHeader.Parse(br);
            }
            //Assert
            Assert.IsNotNull(actualHeader);
            Assert.IsTrue(actualHeader.Length == expectedLength);
        }

        [TestCase]
        public void Parse_ValidHeader_With_ComplexVariableLength_ReturnObject()
        {
            //Arrange
            const int expectedLength = 128;
            byte[] fixHeader = CreateMessage(ControlPacketTypes.CONNECT, 128, 1);

            MessageHeader actualHeader;

            var stream = new MemoryStream(fixHeader);
            //Act
            using (BinaryReader br = new BinaryReader(stream))
            {
                actualHeader = MessageHeader.Parse(br);
            }
            //Assert
            Assert.IsNotNull(actualHeader);
            Assert.IsTrue(actualHeader.Length == expectedLength);
        }

        [TestCase]
        public void Parse_ValidHeader_With_PacketIdentifier_ReturnObject()
        {
            //Arrange
            const ushort expectedPacketId = 5;
            byte[] fixHeader = CreateMessage(ControlPacketTypes.PUBLISH, 10, null, (byte)Flag.Qos1, expectedPacketId);

            MessageHeader actualHeader;

            var stream = new MemoryStream(fixHeader);
            //Act
            using (BinaryReader br = new BinaryReader(stream))
            {
                actualHeader = MessageHeader.Parse(br);
            }
            //Assert
            Assert.IsNotNull(actualHeader);
            Assert.AreEqual(expectedPacketId, actualHeader.PacketIdentifier);
        }
    }
}
