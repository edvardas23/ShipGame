using GameServer;
using System.Net;
using System.Net.Sockets;
using PacketClass;

namespace GameSever
{
    public class Server
    {
        static List<Client> _users;
        static TcpListener _listener;
        
        static void Main(string[] args)
        {
            Console.WriteLine("Serveris startavo");
            _users = new List<Client>();
            _listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 6969);
            try { 
                _listener.Start();
            }
            catch { }
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
                    PacketBuilder broadcastPacket = new PacketBuilder_Adapter();
                    broadcastPacket.WriteOpCode(1);
                    broadcastPacket.WriteMessage(item.Username);
                    broadcastPacket.WriteMessage(item.UID.ToString());
                    user.ClientSocket.Client.Send(broadcastPacket.GetPacketBytes());
                }
            }
            Console.WriteLine("TEST");
        }
        public static void BroadcastMessage(string message)
        {
            foreach (var user in _users)
            {
                PacketBuilder msgPacket = new PacketBuilder_Adapter();
                msgPacket.WriteOpCode(5);
                msgPacket.WriteMessage(message);
                user.ClientSocket.Client.Send(msgPacket.GetPacketBytes());
            }
        }
        public static void BroadcastUndoGameStart()
        {
            foreach (var user in _users)
            {
                PacketBuilder msgPacket = new PacketBuilder_Adapter();
                msgPacket.WriteOpCode(25);
                user.ClientSocket.Client.Send(msgPacket.GetPacketBytes());
            }
        }

        public static void BroadcastDisconnect(string uid)
        {
            var disconectedUser = _users.Where(x => x.UID.ToString() == uid).FirstOrDefault(); 
            _users.Remove(disconectedUser);
           
            foreach (var user in _users)
            {
                var broadcastPacket = new PacketBuilder_Adapter();
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
                PacketBuilder msgPacket = new PacketBuilder_Adapter();
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
                if (user.UID.ToString() != uid && user.Turn == false )
                {
                    PacketBuilder attackPacket = new PacketBuilder_Adapter();
                    attackPacket.WriteOpCode(20);
                    attackPacket.WriteMessage(tileName);
                    user.ClientSocket.Client.Send(attackPacket.GetPacketBytes());
                    BroadcastMessage($"Langellis {tileName} atakuotas!");
                    user.Turn = true;
                    break;
                }
                else if (user.UID.ToString() != uid && user.Turn == true)
                {
                    BroadcastMessage($"ne tavo eile atakuoti");
                }

            }          
        }
    }
}