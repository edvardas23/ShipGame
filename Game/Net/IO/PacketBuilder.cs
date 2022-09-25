using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient.Net.IO
{
    class PacketBuilder
    {
        MemoryStream _ms;
        public PacketBuilder()
        {
            _ms = new MemoryStream();
        }

        public void WriteOpCode(Byte opcode)
        {
            _ms.WriteByte(opcode);
        }
        public void WriteMessage(string msg)
        {
            var msgLenght = msg.Length;
            _ms.Write(BitConverter.GetBytes(msgLenght));
            _ms.Write(Encoding.ASCII.GetBytes(msg));
        }
        public Byte[] GetPacketBytes()
        { 
            return _ms.ToArray();
        }
    }
}
