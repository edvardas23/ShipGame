using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows;
using GameClient.MVVM.Model.UnitModels.ShipModels;
using GameClient.Net.Decorator;
using PacketClass;

namespace GameClient.Net
{
    class Server
    {
        TcpClient _client;
        public PacketReader PacketReader;

        public event Action connectedEvent;
        public event Action messageReceivedEvent;
        public event Action userDisconnectedEvent;
        public event Action startGameEvent; // Pradedamas žaidimas
        public event Action attackEnemyTile;
        public Server()
        {
            _client = new TcpClient();
        }
        public void ConnectToSever(string username)
        {
            if (!_client.Connected)
            {
                _client.Connect("127.0.0.1", 6969);
                PacketReader = new PacketReader_Adapter(_client.GetStream());
                if (!string.IsNullOrEmpty(username))
                {
                    var connectPacket = new PacketBuilder_Adapter();
                    connectPacket.WriteOpCode(0);
                    connectPacket.WriteMessage(username);
                    _client.Client.Send(connectPacket.GetPacketBytes());
                }
                ReadPackets();
            }
        }
        private void ReadPackets()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    //--------------------
                    var opcode = 0;
                    var message = "";
                    try
                    {
                        opcode = PacketReader.ReadByte();
                    }
                    catch { }
                    //-------------------- Jei buginsis ištrinti
                    switch (opcode)
                    {
                        case 1:
                            connectedEvent?.Invoke();
                            break;
                        case 5:
                            messageReceivedEvent?.Invoke();
                            break;
                        case 10:
                            userDisconnectedEvent?.Invoke();
                            break;
                        case 15:
                            startGameEvent?.Invoke();
                            break;
                        case 20:
                            attackEnemyTile?.Invoke();
                            break;
                        default:
                            Console.WriteLine("ah yes...");
                            break;
                    }
                }
            });
        }
        public void SendMessageToServer(string message)
        {
            var messagePacket = new PacketBuilder_Adapter();
            messagePacket.WriteOpCode(5);
            messagePacket.WriteMessage(message);
            _client.Client.Send(messagePacket.GetPacketBytes());

        }
        public void StartNewGameOnServer(int GameModeType)
        {
            BattleshipModel battleship = new BattleshipModel();
            MessageBox.Show(battleship.Display(), battleship.GetName());

            CarrierModel carrier = new CarrierModel();
            MessageBox.Show(carrier.Display(), "Carrier");

            PatrolBoatModel patrolBoat = new PatrolBoatModel();
            MessageBox.Show(patrolBoat.Display(), "Patrol boat");

            MessageBox.Show("Decorator functionality");

            /*BattleshipDestroyable battleshipDestroyable = new BattleshipDestroyable(battleship);
            battleshipDestroyable.AddRocket("Solid rocket");
            battleshipDestroyable.AddRocket("Water rocket");
            battleshipDestroyable.AddRocket("Thermal rocket");
            MessageBox.Show(battleshipDestroyable.Display());
            battleshipDestroyable.ShootRocket("Solid rocket");
            MessageBox.Show(battleshipDestroyable.Display());*/

            CarrierLoadable carrierLoadable = new CarrierLoadable(carrier);
            carrierLoadable.AddPlane("Helicopter");
            carrierLoadable.AddPlane("Aircraft");
            carrierLoadable.AddPlane("Bomber");
            MessageBox.Show(carrierLoadable.Display());
            carrierLoadable.RemovePlane("Bomber");
            MessageBox.Show(carrierLoadable.Display());

            var newGamePacket = new PacketBuilder_Adapter();
            newGamePacket.WriteOpCode(15);
            newGamePacket.WriteMessage("Naujas žaidimas" + GameModeType.ToString());
            _client.Client.Send(newGamePacket.GetPacketBytes());
        }
        
        public void AttackEnemyTileToServer(string buttonName)
        {
            var attackPacket = new PacketBuilder_Adapter();
            attackPacket.WriteOpCode(20);
            attackPacket.WriteMessage(buttonName);
            _client.Client.Send(attackPacket.GetPacketBytes());
        }

    }
}
