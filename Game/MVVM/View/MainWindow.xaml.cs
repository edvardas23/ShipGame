using GameClient.MVVM.Model.ShotModels;
using GameClient.MVVM.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        ShootStrategy shoot = new ShootStrategy();
        public static MainWindow AppWindow;
        public MainWindow()
        {
            AppWindow = this;
            InitializeComponent();
        }

        private void Damage_1(object sender, RoutedEventArgs e)
        {
            shoot.SetStrategy(new ClassicShot());
            int dmg = shoot.SetDamage();
            AppWindow.currentDmg.Text = "Dabartine žala - " + dmg.ToString(); 
        }
        private void Damage_4(object sender, RoutedEventArgs e)
        {
            shoot.SetStrategy(new FugueShoot());
            int dmg = shoot.SetDamage();
            AppWindow.currentDmg.Text = "Dabartine žala - " + dmg.ToString();
        }

        private void StartNewGameButton_Click(object sender, RoutedEventArgs e)
        {
            Thread newWindowThread = new Thread(new ThreadStart(() =>
            {
                GameMode gameModeWindow = new GameMode();
                gameModeWindow.Show();
                System.Windows.Threading.Dispatcher.Run();
            }));
            newWindowThread.SetApartmentState(ApartmentState.STA);
            newWindowThread.IsBackground = true;
            newWindowThread.Start();
        }
    }
}
