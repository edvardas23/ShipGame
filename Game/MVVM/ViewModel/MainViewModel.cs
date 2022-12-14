using Game;
using GameClient.MVVM.Core;
using GameClient.MVVM.Model;
using GameClient.MVVM.Model.TileModels;
using GameClient.MVVM.Model.ShotModels;
using GameClient.Net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using GameClient.MVVM.Model.UnitModels;
using GameClient.MVVM.Model.UnitModels.ShipModels;
using GameClient.MVVM.Model.FacadeModels;
using static GameClient.MVVM.Model.FacadeModels.Facade;
using GameClient.MVVM.Model.PrototypeModels;
using GameClient.MVVM.Builder;
using GameClient.Net.Decorator;
using System.Runtime.CompilerServices;
using GameClient.MVVM.Bridge;
using GameClient.MVVM.Model.UnitModels.FlyWeight;
using GameClient.MVVM.Mediator;
using GameClient.MVVM.Visitor;
using GameClient.MVVM.Chain;
using GameClient.MVVM.CompositePattern;

namespace GameClient.MVVM.ViewModel
{
    public class MainViewModel
    {
        private static int type;
        private static bool WantsToAddShip;
        private static Brush Color;
        private static bool placeTile;
        public Ship currentShip { get; set; }
        public ObservableCollection<UserModel> Users { get; set; }
        public ObservableCollection<string> Messages { get; set; }
        public RelayCommand ConnectToSeverCommand { get; set; }
        public RelayCommand SendMessageCommand { get; set; }
        public RelayCommand StartNewGameCommand { get; set; }
        public RelayCommand ReadyForGameCommand { get; set; }
        public static RelayCommand AttackTileCommand { get; set; }
        public RelayCommand UndoGameStartCommand { get; set; }
        public Server _server;
        public Player firstPlayer { get; set; }
        public Player secondPlayer { get; set; }
        public static string TileName { get; set; }
        public string Message { get; set; }
        public string Username { get; set; }
        public ShootStrategy shoot { get; set; }

        public List<Tile> TilesList = new List<Tile>();

        public List<Ship> ShipList = new List<Ship>();

        public MainViewModel()
        {
            Users = new ObservableCollection<UserModel>();
            Messages = new ObservableCollection<string>();
            _server = new Server();
            _server.connectedEvent += UserConnected;
            _server.messageReceivedEvent += MessageReceived;
            _server.userDisconnectedEvent += RemoveUser;
            _server.startGameEvent += StartGameEvent;
            _server.attackEnemyTile += AttackEnemyTile;
            _server.undoGameStart += UndoGameStart;
            ConnectToSeverCommand = new RelayCommand(o => _server.ConnectToSever(Username), o => !string.IsNullOrEmpty(Username));
            SendMessageCommand = new RelayCommand(o => _server.SendMessageToServer(Message), o => !string.IsNullOrEmpty(Message));
            StartNewGameCommand = new RelayCommand(o => _server.StartNewGameOnServer(Session.Instance.GameModeType));
            AttackTileCommand = new RelayCommand(o => _server.AttackEnemyTileToServer(TileName));
            UndoGameStartCommand = new RelayCommand(o => _server.UndoGameStartToServer());
        }
        public void UserConnected()
        {
            var user = new UserModel
            {
                Username = _server.PacketReader.ReadMessage(),
                UID = _server.PacketReader.ReadMessage()

            };
            if (!Users.Any(x => x.UID == user.UID))
            {
                Application.Current.Dispatcher.Invoke(() => Users.Add(user));
            }
            CheckUsers();
        }
        private void MessageReceived()
        {
            var msg = _server.PacketReader.ReadMessage();
            Application.Current.Dispatcher.Invoke(() => Messages.Add(msg));
        }
        private void RemoveUser()
        {
            var uid = _server.PacketReader.ReadMessage();
            var user = Users.Where(x => x.UID == uid).FirstOrDefault();
            Application.Current.Dispatcher.Invoke(() => Users.Remove(user));
            CheckUsers();
        }
        private void UndoGameStart()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                MainWindow.AppWindow.UndoGameStartButton.Visibility = Visibility.Hidden;
                if (CheckUsers())
                {
                    MainWindow.AppWindow.StartNewGameButton.Visibility = Visibility.Visible;
                }
                MainWindow.AppWindow.mStackPanel.Children.Clear();
                MainWindow.AppWindow.shipsPanel.Children.Clear();
                MainWindow.AppWindow.eStackPanel.Children.Clear();
                MainWindow.AppWindow.currentDmg.Text = "";
                MainWindow.AppWindow.currentMode.Text = "";
                MainWindow.AppWindow.ShipCoord.Text = "";
                //MainWindow.AppWindow.infoStackPanel.Children.Clear();currentDmg currentMode ShipCoord
            });
        }

        private void AttackEnemyTile()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {

                var messageString = _server.PacketReader.ReadMessage();
                string buttonName = messageString.Replace("e", "m");
                foreach (StackPanel item in MainWindow.AppWindow.mStackPanel.Children)
                {
                    foreach (Tile tile in item.Children)
                    {
                        if (tile.Name == buttonName)
                        {
                            tile.Background = Brushes.Red;
                        }
                    }
                }
            });
        }

        public static void Button_Click(object sender, RoutedEventArgs e)
        {
            Random rnd = new Random();
            if (WantsToAddShip && !placeTile)
            {
                Tile tile = sender as Tile;
                tile.Background = Color;
                TileName = tile.Name;
                WantsToAddShip = false;
            }
            else if(!WantsToAddShip && placeTile)
            {
                if(type == 6)
                {
                    RockTile rockTile = new RockTile();
                    rockTile.Data = new RockTileData("RockTile" + rnd.Next(0, 100).ToString(), Color);
                    Tile tile = rockTile.Add(sender);
                    TileName = tile.Name;
                    rockTile.ShowAll();
                }
                if(type == 7)
                {
                    IslandTile island = new IslandTile();
                    island.Data = new IslandTileData("IslandTile" + rnd.Next(0, 100).ToString(), Color);
                    Tile tile = island.Add(sender);
                    TileName = tile.Name;
                    island.ShowAll();
                }
            }
            else
            {
                Tile tile = sender as Tile;
                tile.Background = Brushes.Gray;
                TileName = tile.Name;
            }
        }
        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            switch (type)
            {
                case 1:
                    currentShip = new BattleshipModel("Battleship");
                    WantsToAddShip = true;
                    placeTile = false;
                    Color = Brushes.Moccasin;
                    break;
                case 2:
                    currentShip = new CarrierModel("Carrier");
                    WantsToAddShip = true;
                    placeTile = false;
                    Color = Brushes.LightGreen;
                    break;
                case 3:
                    currentShip = new DestroyerModel("Destroyer");
                    WantsToAddShip = true;
                    placeTile = false;
                    Color = Brushes.CadetBlue;
                    break;
                case 4:
                    currentShip = new PatrolBoatModel("Patrol boat");
                    WantsToAddShip = true;
                    placeTile = false;
                    Color = Brushes.DarkOrange;
                    break;
                case 5:
                    currentShip = new SubmarineModel("Submarine");
                    WantsToAddShip = true;
                    placeTile = false;
                    Color = Brushes.Aquamarine;
                    break;
                case 6:
                    placeTile = true;
                    WantsToAddShip = false;
                    Color = Brushes.Brown;
                    break;
                case 7:
                    placeTile = true;
                    WantsToAddShip = false;
                    Color = Brushes.DarkGreen;
                    break;
                default:
                    MessageBox.Show("Tokio tipo nėra");
                    break;
            }
        }
        /*private void PlaceShips(List<Ship> ships)
        {
            foreach (var item in ships)
            {
        item.Tiles.Add(Tile)
            }
        }*/

        private void DrawUiForShips()
        {
			Component1 component1 = new Component1();
			Component2 component2 = new Component2();

			//AbstractFactory abstr = component2.DoD();

			//AbstractFactory abstr = new AbstractListFactory();
			// List<Ship> listOfShips = abstr.GetShipsList();

			var battleship = new BattleshipHandler();
			var carrier = new CarrierHandler();
			var destroyer = new DestroyerHandler();
			var patrolboat = new PatrolBoatHandler();
			var submarine = new SubmarineHandler();


			List<Ship> listOfShips = component1.DoB();


			Client clnt = new Client();

			for (int i = 0; i < listOfShips.Count(); i++)
            {
                Button btn = new Button();


                if (listOfShips[i] is BattleshipModel)
                {
                    currentShip = new BattleshipModel("Battleship");
                    BattleshipDestroyable battleshipDestroyable = new BattleshipDestroyable(currentShip, "Battleship");
                    battleshipDestroyable.SetButtonBackground(btn);	
					btn.Content = clnt.ClientCode(battleship);
					btn.Click += SetBattleship;
                    btn.Height = 25;
					battleship.SetNext(carrier);
				}
                else if (listOfShips[i] is CarrierModel)
                {
					currentShip = new CarrierModel("Carrier");
                    CarrierLoadable carrierLoadable = new CarrierLoadable(currentShip, "Carrier");
                    carrierLoadable.SetButtonBackground(btn);
					btn.Content = clnt.ClientCode(carrier);
					btn.Click += SetCarrier;
                    btn.Height = 25;
					carrier.SetNext(destroyer);
				}
                else if (listOfShips[i] is DestroyerModel)
                {
					currentShip = new DestroyerModel("Destroyer");
                    DestroyerDecorated destroyerDecorated = new DestroyerDecorated(currentShip, "Destroyer");
                    destroyerDecorated.SetButtonBackground(btn);
                    btn.Content = clnt.ClientCode(destroyer);
					btn.Click += SetDestroyer;
                    btn.Height = 25;
					destroyer.SetNext(patrolboat);
				}
                else if (listOfShips[i] is PatrolBoatModel)
                {
					currentShip = new PatrolBoatModel("Patrol boat");
                    PatrolBoatArmed patrolBoatArmed = new PatrolBoatArmed(currentShip, "Patrol boat");
                    patrolBoatArmed.SetButtonBackground(btn);
                    btn.Content = clnt.ClientCode(patrolboat);
					btn.Click += SetPatrolBoat;
                    btn.Height = 25;
					patrolboat.SetNext(submarine);
				}
                else
                {
					currentShip = new SubmarineModel("Submarine");
                    SubmarineDecorated submarineDecorated = new SubmarineDecorated(currentShip, "Submarine");
                    submarineDecorated.SetButtonBackground(btn);
                    btn.Content = clnt.ClientCode(submarine);
					btn.Click += SetSubmarine;
                    btn.Height = 25;
					submarine.SetNext(battleship);
				}
                MainWindow.AppWindow.shipsPanel.Children.Add(btn);
            }
        }
        private void AddButtonForPlacingRocks()
        {
            Button btn = new Button();

            btn.Content = "New rock";
            btn.Click += SetRock;
            btn.Height = 25;
            MainWindow.AppWindow.shipsPanel.Children.Add(btn);
        }
        private void AddButtonForPlacingIslands()
        {
            Button btn = new Button();

            btn.Content = "New island";
            btn.Click += SetIsland;
            btn.Height = 25;
            MainWindow.AppWindow.shipsPanel.Children.Add(btn);
        }
        private void SetBattleship(object sender, RoutedEventArgs e)
        {
            type = 1;
            Button2_Click(sender, e);
        }
        private void SetCarrier(object sender, RoutedEventArgs e)
        {
            type = 2;
            Button2_Click(sender, e);
        }
        private void SetDestroyer(object sender, RoutedEventArgs e)
        {
            type = 3;
            Button2_Click(sender, e);
        }
        private void SetPatrolBoat(object sender, RoutedEventArgs e)
        {
            type = 4;
            Button2_Click(sender, e);
        }
        private void SetSubmarine(object sender, RoutedEventArgs e)
        {
            type = 5;
            Button2_Click(sender, e);
        }
        private void SetRock(object sender, RoutedEventArgs e)
        {
            type = 6;
            Button2_Click(sender, e);
        }
        private void SetIsland(object sender, RoutedEventArgs e)
        {
            type = 7;
            Button2_Click(sender, e);
        }
        private void StartGameEvent()
        {
            var messageString = _server.PacketReader.ReadMessage();
            messageString = messageString.Substring(messageString.Length - 1);
            Session.Instance.GameModeType = int.Parse(messageString);
            Facade facade = new Facade(Session.Instance.GameModeType);
            StartGameButtonVisibilty(false);
            UndoButtonVisibilty(true);

			switch (Session.Instance.GameModeType)
            {
                case 1:
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                       
                        ClassicModeSubsystem sub = new ClassicModeSubsystem();
                        MainWindow.AppWindow.currentMode.Text = sub.ModeName(); 


                        Session.Instance.MapSize = 10;
                        GenerateEmptyMap("m", Session.Instance.GameModeType);
                        List<Ship> ships = new List<Ship>();
                        Unit unit = new Unit(new List<Tile>(3), "");
                        Ship ship = new BattleshipModel(unit, "Battleship");
                        Ship ship2 = new CarrierModel(unit, "Carrier");
                        Ship ship3 = new DestroyerModel(unit, "Destroyer");
                        Ship ship4 = new PatrolBoatModel(unit, "Patrol boat");
						Ship ship5 = new SubmarineModel(unit, "Submarine");
						MainWindow.AppWindow.ShipCoord.Text += "Laivai atvaizduoti su Template method sablonu:";
						MainWindow.AppWindow.ShipCoord.Text += " \n" + ship.Display();
						MainWindow.AppWindow.ShipCoord.Text += " \n" + ship2.Display();
						MainWindow.AppWindow.ShipCoord.Text += " \n" + ship3.Display();
						MainWindow.AppWindow.ShipCoord.Text += " \n" + ship4.Display();
						MainWindow.AppWindow.ShipCoord.Text += " \n" + ship5.Display() + " \n";
						ships.Add(ship);
                        ships.Add(ship2);
                        ships.Add(ship3);
                        ships.Add(ship4);
                        DrawUiForShips();
                        AddButtonForPlacingRocks();
                        AddButtonForPlacingIslands();
                        GenerateEmptyMap("e", Session.Instance.GameModeType);

						//Composite
						Composite tree = new Composite("Tree");
						Composite branch1 = new Composite("1 Branch");
						branch1.Add(ship);
						branch1.Add(ship2);
						branch1.Add(ship3);
						Composite branch2 = new Composite("2 Branch");
						branch2.Add(ship4);
						tree.Add(branch1);
						tree.Add(branch2);
						MainWindow.AppWindow.Composite.Text += "Composite sablono realizacija: \n";
						MainWindow.AppWindow.Composite.Text += tree.DisplayResult(1);
					});
                    break;
                case 2:
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        AdvancedModeSubsystem sub = new AdvancedModeSubsystem();

                        MainWindow.AppWindow.currentMode.Text = sub.ModeName();
                        Session.Instance.MapSize = 15;
                        GenerateEmptyMap("m", Session.Instance.GameModeType);
						List<Ship> ships = new List<Ship>();
						Unit unit = new Unit(new List<Tile>(3), "");
						Ship ship = new BattleshipModel(unit, "Battleship");
						Ship ship2 = new CarrierModel(unit, "Carrier");
						Ship ship3 = new DestroyerModel(unit, "Destroyer");
						Ship ship4 = new PatrolBoatModel(unit, "Patrol boat");
						Ship ship5 = new SubmarineModel(unit, "Submarine");
						MainWindow.AppWindow.ShipCoord.Text += "Laivai atvaizduoti su Template method sablonu: \n";
						MainWindow.AppWindow.ShipCoord.Text += " \n" + ship.Display();
						MainWindow.AppWindow.ShipCoord.Text += " \n" + ship2.Display();
						MainWindow.AppWindow.ShipCoord.Text += " \n" + ship3.Display();
						MainWindow.AppWindow.ShipCoord.Text += " \n" + ship4.Display();
						MainWindow.AppWindow.ShipCoord.Text += " \n" + ship5.Display() + " \n";
						ships.Add(ship);
						ships.Add(ship2);
						ships.Add(ship3);
						ships.Add(ship4);
						DrawUiForShips();
                        AddButtonForPlacingRocks();
                        AddButtonForPlacingIslands();
                        GenerateEmptyMap("e", Session.Instance.GameModeType);

						//Composite
						Composite tree = new Composite("Tree");
						Composite branch1 = new Composite("1 Branch");
						branch1.Add(ship);
						branch1.Add(ship2);
						branch1.Add(ship3);
						Composite branch2 = new Composite("2 Branch");
						branch2.Add(ship4);
						tree.Add(branch1);
						tree.Add(branch2);
						MainWindow.AppWindow.Composite.Text += "Composite sablono realizacija: \n";
						MainWindow.AppWindow.Composite.Text += tree.DisplayResult(1);
					});
                    break;
                case 3:
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        TurboModeSubsystem sub = new TurboModeSubsystem();

                        MainWindow.AppWindow.currentMode.Text = sub.ModeName();
                        Session.Instance.MapSize = 20;
                        GenerateEmptyMap("m", Session.Instance.GameModeType);
						List<Ship> ships = new List<Ship>();
						Unit unit = new Unit(new List<Tile>(3), "");
						Ship ship = new BattleshipModel(unit, "Battleship");
						Ship ship2 = new CarrierModel(unit, "Carrier");
						Ship ship3 = new DestroyerModel(unit, "Destroyer");
						Ship ship4 = new PatrolBoatModel(unit, "Patrol boat");
						Ship ship5 = new SubmarineModel(unit, "Submarine");
						MainWindow.AppWindow.ShipCoord.Text += "Laivai atvaizduoti su Template method sablonu: \n";
						MainWindow.AppWindow.ShipCoord.Text += " \n" + ship.Display();
						MainWindow.AppWindow.ShipCoord.Text += " \n" + ship2.Display();
						MainWindow.AppWindow.ShipCoord.Text += " \n" + ship3.Display();
						MainWindow.AppWindow.ShipCoord.Text += " \n" + ship4.Display() + " \n";
						ships.Add(ship);
						ships.Add(ship2);
						ships.Add(ship3);
						ships.Add(ship4);
						DrawUiForShips();
                        AddButtonForPlacingRocks();
                        AddButtonForPlacingIslands();
                        GenerateEmptyMap("e", Session.Instance.GameModeType);

						//Composite
						Composite tree = new Composite("Tree");
						Composite branch1 = new Composite("1 Branch");
						branch1.Add(ship);
						branch1.Add(ship2);
						branch1.Add(ship3);
						Composite branch2 = new Composite("2 Branch");
						branch2.Add(ship4);
						tree.Add(branch1);
						tree.Add(branch2);
						MainWindow.AppWindow.Composite.Text += "Composite sablono realizacija: \n";
						MainWindow.AppWindow.Composite.Text += tree.DisplayResult(1);
					});
                    break;
                default:
                    MessageBox.Show("Neveikia :)");
                    break;
            }
        }
        private void GenerateEmptyMap(string identifier, int gameType)
		{
			Component1 component1 = new Component1();
			Component2 component2 = new Component2();
			new ConcreteMediator(component1, component2);

			Random rnd = new Random();

            if (identifier == "m")
            { 

            }
            Prototype prototype = new Prototype(); 
            for (int i = 0; i < 10; i++)
            {
                int x = rnd.Next(0, 9);
                int y = rnd.Next(0, 9);
                prototype.array[i] = x.ToString() + y.ToString();
            }
			Prototype clone = component1.DoA(prototype);

            MainWindow.AppWindow.Dispatcher.Invoke(() =>
            {
                if (identifier.Equals("m"))
                {
                    /*MainWindow.AppWindow.ShipCoord.Text += "Laivų koordinačiu kopijos padarytos su prototype patternu: \n";
                    for (int i = 0; i < 10; i++)
                    {
                        MainWindow.AppWindow.ShipCoord.Text += " " + clone.array[i];
                    }*/
                }
                Director director = new Director();
                ConcreteBuilder builder = new ConcreteBuilder();
                director.Builder = builder;
				var factory = new FlyweightFactory();
                director.BuildFullFeaturedMap(Session.Instance.MapSize, Session.Instance.MapSize, identifier, AttackTileCommand, clone, factory, Session.Instance.GameModeType);
				//For Visitor pattern implementation only
				var visitor = new ConcreteVisitor();
				List<Tile> tiles = builder.GetTiles();
				bool printedSeaTile = false;
				bool printedRockTile = false;
				bool printedIslandTile = false;
				foreach (var tile in tiles)
				{
					if (tile.Type is SeaTile && printedSeaTile == false)
					{
						SeaTile seatile = (SeaTile)tile;
						seatile.Accept(visitor);
						printedSeaTile = true;
					} else if(tile.Type is RockTile && printedRockTile == false)
					{
						RockTile rockTile = (RockTile)tile;
						rockTile.Accept(visitor);
						printedRockTile = true;
					}
					else if (tile.Type is IslandTile && printedIslandTile == false)
					{
						IslandTile islandTile = (IslandTile)tile;
						islandTile.Accept(visitor);
						printedIslandTile = true;
					}
				}
			});
        }   
        public bool CheckUsers()
        {
            bool flag = false;
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (Users.Count >= 2)
                {
                    try
                    {
                        if (MainWindow.AppWindow.StartNewGameButton != null)
                        {
                            MainWindow.AppWindow.StartNewGameButton.IsEnabled = true;
                            MainWindow.AppWindow.StartNewGameButton.Visibility = Visibility.Visible;
                        }
                    }
                    catch (Exception ex) { }
                    flag = true;
                }
                else
                {
                    try
                    {
                        if (MainWindow.AppWindow.StartNewGameButton != null)
                        {
                            MainWindow.AppWindow.StartNewGameButton.IsEnabled = false;
                            MainWindow.AppWindow.StartNewGameButton.Visibility = Visibility.Hidden;
                        }
                    }
                    catch (Exception ex) { }
                    flag = false;
                }
            });
            return flag;
        }
        private void StartGameButtonVisibilty(bool Flag)
        {
            if (Flag)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    MainWindow.AppWindow.StartNewGameButton.Visibility = Visibility.Visible;
                });
            }
            else
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    MainWindow.AppWindow.StartNewGameButton.Visibility = Visibility.Hidden;
                });
            }
        }
        private void UndoButtonVisibilty(bool Flag)
        {
            if (Flag)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    MainWindow.AppWindow.UndoGameStartButton.Visibility = Visibility.Visible;
                    MainWindow.AppWindow.UndoGameStartButton.IsEnabled = Flag;
                });
            }
        }
    }
}
