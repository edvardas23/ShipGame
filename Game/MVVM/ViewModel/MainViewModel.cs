using Game;
using GameClient.MVVM.Core;
using GameClient.MVVM.Model;
using GameClient.Net;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GameClient.MVVM.ViewModel
{
    class MainViewModel
    {
        public ObservableCollection<UserModel> Users { get; set; }
        public ObservableCollection<string> Messages { get; set; }
        public RelayCommand ConnectToSeverCommand { get; set; }
        public Session gameSession { get; set; }
        public RelayCommand SendMessageCommand { get; set; }
        public RelayCommand StartNewGameCommand { get; set; }
        public RelayCommand AttackTileCommand { get; set; }
        private Server _server;
        public Player firstPlayer { get; set; }
        public Player secondPlayer { get; set; }
        public GameModel gameModel { get; set; }
        public string TileName { get; set; }

        public string Message { get; set; }
        public string Username { get; set; }
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
            StartNewGameCommand = new RelayCommand(o => _server.StartNewGameOnServer(), o => Users.Count == 2);
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
        }
        private void AttackEnemyTile()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                var messageString = _server.PacketReader.ReadMessage();
                string buttonName = messageString.Replace("e", "m");
                foreach (StackPanel item in MainWindow.AppWindow.myStackPanel.Children)
                {
                    foreach (Button button in item.Children)
                    {
                        if(button.Name == buttonName)
                             button.Background = Brushes.Red;
                    }
                }
            });  
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TileName = (sender as Button).Name.ToString();
        }
        private void StartGameEvent()
        {
            gameSession = Session.Instance;
            gameSession.MapSize = 10;
            GenerateEmptyMap(gameSession);
            gameModel = new GameModel();
            gameModel.IsGameStarted = true;
            AssignPlayers(gameModel);
            Map myMap = new Map(gameSession.MapSize);
            firstPlayer.MyMap = myMap;
            secondPlayer.MyMap = myMap;
            firstPlayer.EnemyMap = secondPlayer.MyMap;
            secondPlayer.EnemyMap = firstPlayer.MyMap;
            GenerateEnemyMap(gameSession);

        }
        private void GenerateEmptyMap(Session session)
        {
            MainWindow.AppWindow.Dispatcher.Invoke(() =>
            {
                double width = MainWindow.AppWindow.myStackPanel.ActualWidth / gameSession.MapSize;
                double height = MainWindow.AppWindow.myStackPanel.ActualHeight / gameSession.MapSize;
                for (int i = 0; i < gameSession.MapSize; i++)
                {
                    StackPanel stackPanel = new StackPanel();
                    stackPanel.Name = "stackPanel" + i.ToString();
                    stackPanel.HorizontalAlignment = HorizontalAlignment.Left;
                    stackPanel.Orientation = Orientation.Horizontal;
                    MainWindow.AppWindow.myStackPanel.Children.Add(stackPanel);

                    for (int j = 0; j < gameSession.MapSize; j++)
                    {
                        Button newBtn = new Button();
                        newBtn.Content = "m" + i.ToString() + j.ToString();
                        newBtn.Name = "m" + i.ToString() + j.ToString();
                        newBtn.Width = width;
                        newBtn.Height = height;
                        System.Diagnostics.Debug.WriteLine(newBtn.Parent);
                        stackPanel.Children.Add(newBtn);
                    }
                }
            });
        }
        private void GenerateEnemyMap(Session session)
        {
            MainWindow.AppWindow.Dispatcher.Invoke(() =>
            {
                double width = MainWindow.AppWindow.enemyStackPanel.ActualWidth / gameSession.MapSize;
                double height = MainWindow.AppWindow.enemyStackPanel.ActualHeight / gameSession.MapSize;
                for (int i = 0; i < gameSession.MapSize; i++)
                {
                    StackPanel stackPanel = new StackPanel();
                    stackPanel.Name = "eStackPanel" + i.ToString();
                    stackPanel.HorizontalAlignment = HorizontalAlignment.Left;
                    stackPanel.Orientation = Orientation.Horizontal;
                    MainWindow.AppWindow.enemyStackPanel.Children.Add(stackPanel);

                    for (int j = 0; j < gameSession.MapSize; j++)
                    {
                        Button newBtn = new Button();
                        newBtn.Content = "e" + i.ToString() + j.ToString();
                        newBtn.Name = "e" + i.ToString() + j.ToString();
                        newBtn.Click  += Button_Click;
                        newBtn.Command = AttackTileCommand;
                        newBtn.Width = width;
                        newBtn.Height = height;
                        stackPanel.Children.Add(newBtn);
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
    }
}
