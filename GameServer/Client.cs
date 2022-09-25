using GameServer.Net.IO;
using GameSever;
using System.Net.Sockets;

namespace GameServer
{
    class Client
    {
        public string Username { get; set; }
        public Guid UID { get; set; }
        public TcpClient ClientSocket { get; set; }

        PacketReader _packetReader;
        public Client(TcpClient client)
        {
            ClientSocket = client;
            UID = Guid.NewGuid();
            _packetReader = new PacketReader(ClientSocket.GetStream());
            var opcode = _packetReader.ReadByte();
            Username = _packetReader.ReadMessage();

            Console.WriteLine($"[{DateTime.Now}]:Klientas vardu: {Username} prisijungė");

            Task.Run(() => Process());
        }
        void Process()
        {
            while (true)
            {
                try
                {
                    var opcode = _packetReader.ReadByte();
                    switch (opcode)
                    {
                        case 5:
                            var msg = _packetReader.ReadMessage();
                            Console.WriteLine($"[{DateTime.Now}][{Username}]:Gauta žinutė! {msg}");
                            Program.BroadcastMessage($"[{DateTime.Now}]: [{Username}]: {msg}");  
                            break;
                        case 15:
                            var gameStartedMessage = _packetReader.ReadMessage();
                            Console.WriteLine($"[{DateTime.Now}][{Username}]:Sukurtas žaidimas! {gameStartedMessage}");
                            Program.BroadcastGameStart($"[{DateTime.Now}]: [{Username}]: {gameStartedMessage}");
                            break;
                        case 20:
                            var tileAttacked = _packetReader.ReadMessage();
                            Console.WriteLine($"[{DateTime.Now}][{Username}]:Atakavo langelį! {tileAttacked}");
                            Program.BroadcastTileAttack($"{tileAttacked}");
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine($"[{Username}]: Atsijungė!");
                    Program.BroadcastDisconnect(UID.ToString());
                    ClientSocket.Close();
                    break;
                }
            }
        }
    }
}
