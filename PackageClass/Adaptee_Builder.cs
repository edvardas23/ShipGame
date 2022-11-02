using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacketClass
{
    public class Adaptee_Builder
    {
        public void WriteOpCode(Byte opcode, MemoryStream ms)
        {
            ms.WriteByte(opcode);
        }
        public void WriteMessage(string msg, MemoryStream ms)
        {
            var msgLenght = msg.Length;
            ms.Write(BitConverter.GetBytes(msgLenght));
            ms.Write(Encoding.ASCII.GetBytes(msg));
        }
        public Byte[] GetPacketBytes(MemoryStream ms)
        {
            return ms.ToArray();
        }
    }
}