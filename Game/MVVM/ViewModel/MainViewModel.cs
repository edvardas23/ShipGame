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
using System.Windows.Documents.DocumentStructures;
using System.Windows.Media;
using System.Threading;

namespace GameClient.MVVM.ViewModel
{
    public class MainViewModel
    {
        public ObservableCollection<UserModel> Users { get; set; }
        public ObservableCollection<string> Messages { get; set; }
        public RelayCommand ConnectToSeverCommand { get; set; }
        public RelayCommand SendMessageCommand { get; set; }
        public RelayCommand StartNewGameCommand { get; set; }
        public RelayCommand ReadyForGameCommand { get; set; }
        public RelayCommand AttackTileCommand { get; set; }
        private Server _server;
        public Player firstPlayer { get; set; }
        public Player secondPlayer { get; set; }
        public string TileName { get; set; }
        public string Message { get; set; }
        public string Username { get; set; }
        public ShootStrategy shoot { get; set; }

        public List<Tile> TilesList = new List<Tile>();
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
            ConnectToSeverCommand = new RelayCommand(o => _server.ConnectToSever(Username), o => !string.IsNullOrEmpty(Username));
            SendMessageCommand = new RelayCommand(o => _server.SendMessageToServer(Message), o => !string.IsNullOrEmpty(Message));
            //ReadyForGameCommand = new RelayCommand() TODO
            StartNewGameCommand = new RelayCommand(o => _server.StartNewGameOnServer(Session.Instance.GameModeType));
            AttackTileCommand = new RelayCommand(o => _server.AttackEnemyTileToServer(TileName));
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
        private void StartGameEvent()
        {
            var messageString = _server.PacketReader.ReadMessage();
            messageString = messageString.Substring(messageString.Length - 1);
            Session.Instance.GameModeType = int.Parse(messageString);

            switch (Session.Instance.GameModeType)
            {
                case 1:
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        MainWindow.AppWindow.currentDmg.Text = "Klasikinis";
                        Session.Instance.MapSize = 10;
                        GenerateEmptyMap("m");
                        GenerateEmptyMap("e");
                    });
                    break;
                case 2:
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        MainWindow.AppWindow.currentDmg.Text = "Papildytas";
                        Session.Instance.MapSize = 15;
                        GenerateEmptyMap("m");
                        GenerateEmptyMap("e");
                    });
                    break;
                case 3:
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        MainWindow.AppWindow.currentDmg.Text = "Turbo";
                        Session.Instance.MapSize = 20;
                        GenerateEmptyMap("m");
                        GenerateEmptyMap("e");
                    });
                    break;
                default:
                    MessageBox.Show("Neveikia :)");
                    break;
            }
            
            //gameSession = Session.Instance;
            //gameSession.MapSize = 10;
            //GenerateEmptyMap();
            //GenerateEnemyMap();
            /*gameModel = new GameModel();
            gameModel.IsGameStarted = true;
            AssignPlayers(gameModel);
            Map myMap = new Map(gameSession.MapSize, TilesList);
            firstPlayer.MyMap = myMap;
            secondPlayer.MyMap = myMap;
            firstPlayer.SetEnemy(secondPlayer);
            secondPlayer.SetEnemy(firstPlayer);
            GenerateEnemyMap(gameSession);*/
        }
        private void GenerateEmptyMap(string identifier)
        {
            MainWindow.AppWindow.Dispatcher.Invoke(() =>
            {
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
                        Tile tile = new Tile(i, j);
                        //tile.Content = identifier + i.ToString() + j.ToString();
                        tile.Name = identifier + i.ToString() + j.ToString();
                        tile.Width = width;
                        tile.Height = height;
                        tile.Click += Button_Click;
                        if (identifier.Equals("e"))
                            tile.Command = AttackTileCommand;
                        System.Diagnostics.Debug.WriteLine(tile.Parent);
                        newStackPanel.Children.Add(tile);
                        TilesList.Add(tile);
                    }
                }
            });
        }
        public void AssignPlayers(GameModel gameModel)
        {
            foreach (var item in Users)
            {
                if (firstPlayer == null)
                {
                    firstPlayer = new Player();
                    firstPlayer.Username = item.Username;
                    firstPlayer.UID = item.UID;
                  
                    gameModel.firstPlayer = firstPlayer;
                    continue;
                }
                else if (secondPlayer == null)
                {
                    secondPlayer = new Player();
                    secondPlayer.Username = item.Username;
                    secondPlayer.UID = item.UID;
              
                    gameModel.secondPlayer = secondPlayer;
                }
            }
        }
        private void CheckUsers()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {   
                if (Users.Count >= 2)
                {
                    MainWindow.AppWindow.StartNewGameButton.IsEnabled = true;
                }
                else
                {
                    MainWindow.AppWindow.StartNewGameButton.IsEnabled = true;
                }
            });
        }
    }
}
