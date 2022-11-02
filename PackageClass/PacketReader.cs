using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacketClass
{
    public class PacketReader
    {
        public virtual string ReadMessage()
        {
            Console.WriteLine("Called Reader target method");
            return "";
        }
        public virtual int ReadByte()
        {
            Console.WriteLine("Called Reader target method 'ReadByte' ");
            return 0;
        }
    }
}
