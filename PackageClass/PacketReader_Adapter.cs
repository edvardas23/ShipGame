using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PacketClass
{
    public class PacketReader_Adapter : PacketReader
    {
        private Adaptee_Reader adaptee;
        private NetworkStream stream;
        
        public PacketReader_Adapter(NetworkStream stream)
        {
            adaptee = new Adaptee_Reader(stream);
            this.stream = stream;
        }
        public override string ReadMessage()
        {
            string msg = adaptee.ReadMessage();
            return msg;
        }
        public override int ReadByte()
        {
            return adaptee.ReadByte();
        }
    }
}
