using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacketClass
{
    public class PacketBuilder
    {
        public virtual void WriteOpCode(Byte opcode)
        {
            Console.WriteLine("Called Target WriteOpCode method");
        }
        public virtual void WriteMessage(string msg)
        {
            Console.WriteLine("Called Target WriteMessage method");
        }
        public virtual Byte[] GetPacketBytes()
        {
            Console.WriteLine("Called Target GetPacketBytes method");
            return null;
        }
    }
}
