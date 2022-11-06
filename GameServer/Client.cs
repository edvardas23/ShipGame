using GameSever;
using System.Net.Sockets;
using PacketClass;

namespace GameServer
{
    class Client
    {
        public string Username { get; set; }
        public Guid UID { get; set; }
        public TcpClient ClientSocket { get; set; }
        public bool Turn { get; set; }

        PacketReader _packetReader;
        public Client(TcpClient client)
        {
            ClientSocket = client;
            UID = Guid.NewGuid();

            _packetReader = new PacketReader_Adapter(ClientSocket.GetStream());
            var opcode = _packetReader.ReadByte();
            Username = _packetReader.ReadMessage();
            Turn = false;
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
                            Observer.BroadcastMessage($"[{DateTime.Now}]: [{Username}]: {msg}");  
                            break;
                        case 15:  
                            var gameStartedMessage = _packetReader.ReadMessage(); 
                            Turn = true;
                            Console.WriteLine($"[{DateTime.Now}][{Username}]:Sukurtas žaidimas! {gameStartedMessage}");
                            Observer.BroadcastGameStart($"[{DateTime.Now}]: [{Username}]: {gameStartedMessage}");
                            break;
                        case 20:
                            var tileAttacked = _packetReader.ReadMessage();
                            Observer.BroadcastTileAttack($"{tileAttacked}", UID.ToString());
                            Turn = false;
                            Console.WriteLine($"[{DateTime.Now}][{Username}]:Atakavo langelį! {tileAttacked}");   
                            break;
                        case 25:
                            Observer.BroadcastUndoGameStart();
                            Console.WriteLine($"[{DateTime.Now}]:Žaidimo pradžia atšaukta!");
                            break;
                        default:
                            Console.WriteLine("Nenustatyta komanda");
                            break;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine($"[{Username}]: Atsijungė!");
                    Observer.BroadcastDisconnect(UID.ToString());
                    ClientSocket.Close();
                    break;
                }
            }
        }
    }
}
