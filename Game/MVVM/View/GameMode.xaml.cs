using GameClient.MVVM.Model;
using GameClient.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GameClient.MVVM.View
{
    /// <summary>
    /// Interaction logic for GameMode.xaml
    /// </summary>
    public partial class GameMode : Window
    {
        public static GameMode ModeWindow;
        public GameMode()
        {
            ModeWindow = this;
            InitializeComponent();
        }

        private void classicModeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void advancedModeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ModeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
