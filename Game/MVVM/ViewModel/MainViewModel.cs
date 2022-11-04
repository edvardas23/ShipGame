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

namespace GameClient.MVVM.ViewModel
{
    public class MainViewModel
    {
        public Ship currentShip { get; set; }
        public ObservableCollection<UserModel> Users { get; set; }
        public ObservableCollection<string> Messages { get; set; }
        public RelayCommand ConnectToSeverCommand { get; set; }
        public RelayCommand SendMessageCommand { get; set; }
        public RelayCommand StartNewGameCommand { get; set; }
        public RelayCommand ReadyForGameCommand { get; set; }
        public RelayCommand AttackTileCommand { get; set; }
        public RelayCommand UndoGameStartCommand { get; set; }
        private Server _server;
        public Player firstPlayer { get; set; }
        public Player secondPlayer { get; set; }
        public string TileName { get; set; }
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
        private void UserConnected()
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Tile tile = sender as Tile;
            tile.Background = Brushes.Gray;
            TileName = tile.Name;
        }
        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            //currentShip = new Ship();
            MessageBox.Show(currentShip.GetType().ToString());
            //currentShip.Size
        }
        //private void PlaceShips(List<Ship> ships)
        //{
        //    foreach (var item in ships)
        //    {
        //item.Tiles.Add(Tile)
        //    }
        //}

        private void DrawUiForShips()
        {
            AbstractFactory abstr = new AbstractListFactory();
            List<Ship> listOfShips = abstr.GetShipsList();

            for (int i = 0; i < listOfShips.Count(); i++)
            {
                Button btn = new Button();
                if (listOfShips[i] is BattleshipModel)
                {
                    btn.Content = "Battleship";
                    currentShip = new BattleshipModel();
                    btn.Click += Button2_Click;
                    btn.Height = 25;
                }
                else if (listOfShips[i] is CarrierModel)
                {
                    btn.Content = "Carrier";
                    currentShip = new CarrierModel();
                    btn.Height = 25;
                }
                else if (listOfShips[i] is DestroyerModel)
                {
                    btn.Content = "Destroyer";
                    currentShip = new DestroyerModel();
                    btn.Height = 25;
                }
                else if (listOfShips[i] is PatrolBoatModel)
                {
                    btn.Content = "Patrol Boat";
                    currentShip = new PatrolBoatModel();
                    btn.Height = 25;
                }
                else
                {
                    btn.Content = "Submarine";
                    currentShip = new SubmarineModel();
                    btn.Height = 25;
                }

                MainWindow.AppWindow.shipsPanel.Children.Add(btn);
            }

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
                        GenerateEmptyMap("m");
                        List<Ship> ships = new List<Ship>();
                        Unit unit = new Unit(new List<Tile>(3));
                        Ship ship = new Ship(unit);
                        ships.Add(ship);
                        ships.Add(ship);
                        DrawUiForShips();
                        //PlaceShips(ships);
                        //Ship ship = new Ship();
                        GenerateEmptyMap("e");
                    });
                    break;
                case 2:
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        AdvancedModeSubsystem sub = new AdvancedModeSubsystem();

                        MainWindow.AppWindow.currentMode.Text = sub.ModeName();
                        Session.Instance.MapSize = 15;
                        GenerateEmptyMap("m");
                        DrawUiForShips();
                        GenerateEmptyMap("e");
                    });
                    break;
                case 3:
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        TurboModeSubsystem sub = new TurboModeSubsystem();

                        MainWindow.AppWindow.currentMode.Text = sub.ModeName();
                        Session.Instance.MapSize = 20;
                        GenerateEmptyMap("m");
                        DrawUiForShips();
                        GenerateEmptyMap("e");
                    });
                    break;
                default:
                    MessageBox.Show("Neveikia :)");
                    break;
            }
        }
        private void GenerateEmptyMap(string identifier)
        {
            Random rnd = new Random();
            Prototype prototype = new Prototype(); 
            for (int i = 0; i < 10; i++)
            {
                int x = rnd.Next(0, 9);
                int y = rnd.Next(0, 9);
                prototype.array[i] = x.ToString() + y.ToString();
            }

            Prototype clone = prototype.ShallowCopy();



            MainWindow.AppWindow.Dispatcher.Invoke(() =>
            {
                MainWindow.AppWindow.ShipCoord.Text += "Laivų koordinačiu kopijos padarytos su prototype patternu: \n";
                for (int i = 0; i < 10; i++)
                {
                    MainWindow.AppWindow.ShipCoord.Text += " " + clone.array[i];
                }
                
                StackPanel stackPanel = (StackPanel)MainWindow.AppWindow.FindName(identifier + "StackPanel");
                double width = stackPanel.ActualWidth / Session.Instance.MapSize;
                double height = stackPanel.ActualHeight / Session.Instance.MapSize;
                for (int i = 0; i < Session.Instance.MapSize; i++)
                {
                    StackPanel newStackPanel = new StackPanel();
                    newStackPanel.Name = identifier + "StackPanel" + i.ToString();
                    newStackPanel.HorizontalAlignment = HorizontalAlignment.Left;
                    newStackPanel.Orientation = Orientation.Horizontal;
                    stackPanel.Children.Add(newStackPanel);

                    for (int j = 0; j < Session.Instance.MapSize; j++)
                    {
                        Random rnd = new Random();
                        int rand = rnd.Next(1, 50); 
                        if (rand >= 48)
                        {
                            Tile tile = new Tile(i, j);
                            RockTile rockTile = new RockTile(tile);
                            rockTile.Background = Brushes.Olive;
                            rockTile.placeable = false;
                            rockTile.Name = identifier + i.ToString() + j.ToString();
                            rockTile.Width = width;
                            rockTile.Height = height;



                            if (identifier.Equals("e"))
                            {
                                rockTile.Command = AttackTileCommand;
                                rockTile.Click += Button_Click;
                            }

                            System.Diagnostics.Debug.WriteLine(rockTile.Parent);
                            newStackPanel.Children.Add(rockTile);
                            TilesList.Add(rockTile);
                        }
                        else
                        {
                            Tile tile = new Tile(i, j);
                            //tile.Content = identifier + i.ToString() + j.ToString();
                            tile.Name = identifier + i.ToString() + j.ToString();
                            tile.Width = width;
                            tile.Height = height;

                            if (identifier.Equals("e"))
                            {
                                tile.Command = AttackTileCommand;
                                tile.Click += Button_Click;
                            }
                            else
                            {
                                for (int k = 0; k < 10; k++)
                                {
                                    if (i.ToString() + j.ToString() == prototype.array[k])
                                    {
                                        tile.Background = Brushes.Green;
                                    }
                                }
                            }

                            System.Diagnostics.Debug.WriteLine(tile.Parent);
                            newStackPanel.Children.Add(tile);
                            TilesList.Add(tile);

                        }
                    }
                }
            });
        }   
        private bool CheckUsers()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (Users.Count >= 2)
                {
                    MainWindow.AppWindow.StartNewGameButton.IsEnabled = true;
                    MainWindow.AppWindow.StartNewGameButton.Visibility = Visibility.Visible;
                    return true;
                }
                else
                {
                    MainWindow.AppWindow.StartNewGameButton.IsEnabled = true;
                    MainWindow.AppWindow.StartNewGameButton.Visibility = Visibility.Visible;
                    return true;
                }
            });
            return false;
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
