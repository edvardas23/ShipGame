using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PacketClass
{
    public class Adaptee_Reader : BinaryReader
    {
        private NetworkStream _ns;
        public Adaptee_Reader(NetworkStream ns) : base(ns)
        {
            _ns = ns;
        }
        public string ReadMessage()
        {
            byte[] msgBuffer;
            var lenght = ReadInt32();
            msgBuffer = new byte[lenght];
            _ns.Read(msgBuffer, 0, lenght);

            var msg = Encoding.ASCII.GetString(msgBuffer);
            return msg;
        }
        public int ReadByte()
        {
            return _ns.ReadByte();
        }
        public void WriteConsole(string Username)
        {
            Console.WriteLine($"[{DateTime.Now}]:Klientas vardu: {Username} prisijungė");
        }
    }
}
