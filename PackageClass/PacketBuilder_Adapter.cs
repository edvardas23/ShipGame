using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacketClass
{
    public class PacketBuilder_Adapter : PacketBuilder
    {
        MemoryStream _ms;
        Adaptee_Builder builder;

        public PacketBuilder_Adapter()
        {
            _ms = new MemoryStream();
            builder = new Adaptee_Builder();
        }
        public override void WriteOpCode(Byte opcode)
        {
            builder.WriteOpCode(opcode, _ms);
        }
        public override void WriteMessage(string msg)
        {
            builder.WriteMessage(msg, _ms);
        }
        public override Byte[] GetPacketBytes()
        {
            return _ms.ToArray();
        }
    }
}
