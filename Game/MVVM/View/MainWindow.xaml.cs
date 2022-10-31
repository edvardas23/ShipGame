using GameClient.MVVM.Model;
using GameClient.MVVM.Model.ShotModels;
using GameClient.MVVM.Model.TileModels;
using GameClient.MVVM.ViewModel;
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
            ChangeGameModeButtonsVisibility(true);
        }
        private void ChangeGameModeButtonsVisibility(bool visible)
        {
            if (visible)
            {
                classicModeButton.Visibility = Visibility.Visible;
                advancedModeButton.Visibility = Visibility.Visible;
                turboModeButton.Visibility = Visibility.Visible;
            }
            else
            {
                classicModeButton.Visibility = Visibility.Hidden;
                advancedModeButton.Visibility = Visibility.Hidden;
                turboModeButton.Visibility = Visibility.Hidden;
                StartNewGameButton.Visibility = Visibility.Hidden;
                eStackPanel.Visibility = Visibility.Hidden;
            }
        }
        private void classicModeButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeGameModeButtonsVisibility(false);
            Session.Instance.GameModeType = 1;
        }

        private void advancedModeButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeGameModeButtonsVisibility(false);
            Session.Instance.GameModeType = 2;
        }

        private void turboModeButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeGameModeButtonsVisibility(false);
            Session.Instance.GameModeType = 3;
        }

    }
}
