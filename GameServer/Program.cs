using GameServer;
using GameServer.Net.IO;
using System;
using System.Net;
using System.Net.Sockets;

namespace GameSever
{
    class Program
    {
        static List<Client> _users;
        static TcpListener _listener;
        
        static void Main(string[] args)
        {
            _users = new List<Client>();
            _listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 6969);
            _listener.Start();

            while (true)
            {
                var client = new Client(_listener.AcceptTcpClient());
                _users.Add(client);

                BroadcastConnection();
            }
       
        }
        static void BroadcastConnection()
        {
            foreach (var user in _users)
            {
                foreach (var item in _users)
                {
                    var broadcastPacket = new PacketBuilder();
                    broadcastPacket.WriteOpCode(1);
                    broadcastPacket.WriteMessage(item.Username);
                    broadcastPacket.WriteMessage(item.UID.ToString());
                    user.ClientSocket.Client.Send(broadcastPacket.GetPacketBytes());
                }
            }
        }
        public static void BroadcastMessage(string message)
        {
            foreach (var user in _users)
            {
                var msgPacket = new PacketBuilder();
                msgPacket.WriteOpCode(5);
                msgPacket.WriteMessage(message);
                user.ClientSocket.Client.Send(msgPacket.GetPacketBytes());
            }
        }

        public static void BroadcastDisconnect(string uid)
        {
            var disconectedUser = _users.Where(x => x.UID.ToString() == uid).FirstOrDefault(); 
            _users.Remove(disconectedUser);
           
            foreach (var user in _users)
            {
                var broadcastPacket = new PacketBuilder();
                broadcastPacket.WriteOpCode(10);
                broadcastPacket.WriteMessage(uid);
                user.ClientSocket.Client.Send(broadcastPacket.GetPacketBytes());
            }

            BroadcastMessage($"[{disconectedUser.Username}] atsijungė!");
        }
        public static void BroadcastGameStart(string message)
        {
            foreach (var user in _users)
            {
                var msgPacket = new PacketBuilder();
                msgPacket.WriteOpCode(15);
                msgPacket.WriteMessage(message);
                user.ClientSocket.Client.Send(msgPacket.GetPacketBytes());
            }
            BroadcastMessage($"Žaidimas prasidėjo!");
        }
        public static void BroadcastTileAttack(string tileName, string uid)
        {
            foreach (var user in _users)
            {
                if (user.UID.ToString() != uid)
                {
                    var attackPacket = new PacketBuilder();
                    attackPacket.WriteOpCode(20);
                    attackPacket.WriteMessage(tileName);
                    user.ClientSocket.Client.Send(attackPacket.GetPacketBytes());
                }  
            }
            BroadcastMessage($"Langellis {tileName} atakuotas!");
        }
    }
}