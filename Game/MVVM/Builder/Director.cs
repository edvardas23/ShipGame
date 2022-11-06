using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using GameClient.MVVM.Core;
using Game;
using GameClient.MVVM.Model;

namespace GameClient.MVVM.Builder
{
    public class Director
    {
        private IBuilder builder;

        public IBuilder Builder
        {
            set { builder = value; }
        }

        public void BuildMinimalViableMap(int x, int y, string identifier, double width, double height, RelayCommand AttackTileCommand)
        {
            StackPanel stackPanel = (StackPanel)MainWindow.AppWindow.FindName(identifier + "StackPanel");
            double width2 = stackPanel.ActualWidth / Session.Instance.MapSize;
            double height2 = stackPanel.ActualHeight / Session.Instance.MapSize;
            for (int i = 0; i < x; i++)
            {
                StackPanel newStackPanel = new StackPanel();
                newStackPanel.Name = identifier + "StackPanel" + i.ToString();
                newStackPanel.HorizontalAlignment = HorizontalAlignment.Left;
                newStackPanel.Orientation = Orientation.Horizontal;
                stackPanel.Children.Add(newStackPanel);
                for (int j = 0; j < y; j++)
                {
                    this.builder.BuildPartSeaTile(i, j, identifier, width2, height2, AttackTileCommand, newStackPanel);
                }
            }
        }

        public void BuildFullFeaturedMap(int x, int y, string identifier, RelayCommand AttackTileCommand)
        {
            StackPanel stackPanel = (StackPanel)MainWindow.AppWindow.FindName(identifier + "StackPanel");
            double width = stackPanel.ActualWidth / Session.Instance.MapSize;
            double height = stackPanel.ActualHeight / Session.Instance.MapSize;
            for (int i = 0; i < x; i++)
            {
                StackPanel newStackPanel = new StackPanel();
                newStackPanel.Name = identifier + "StackPanel" + i.ToString();
                newStackPanel.HorizontalAlignment = HorizontalAlignment.Left;
                newStackPanel.Orientation = Orientation.Horizontal;
                stackPanel.Children.Add(newStackPanel);
                for (int j = 0; j < y; j++)
                {
                    Random rnd = new Random();
                    int rand = rnd.Next(1, 50);;
                    if (rand >= 48)
                        this.builder.BuildPartRockTile(i, j, identifier, width, height, AttackTileCommand, newStackPanel);
                    else
                        this.builder.BuildPartSeaTile(i, j, identifier, width, height, AttackTileCommand, newStackPanel);
                }
            }
        }
    }
}
