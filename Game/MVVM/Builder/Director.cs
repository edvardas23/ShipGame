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
using GameClient.MVVM.Model.PrototypeModels;
using GameClient.MVVM.Model.UnitModels.FlyWeight;

namespace GameClient.MVVM.Builder
{
    public class Director
    {
        private IBuilder builder;

        public IBuilder Builder
        {
            set { builder = value; }
        }

        public void BuildMinimalViableMap(int x, int y, string identifier, double width, double height, RelayCommand AttackTileCommand, Prototype prototype, FlyweightFactory factory)
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
                    this.builder.BuildPartSeaTile(i, j, identifier, width2, height2, AttackTileCommand, newStackPanel, prototype, factory);
                }
            }
        }

        public void BuildFullFeaturedMap(int x, int y, string identifier, RelayCommand AttackTileCommand, Prototype prototype, FlyweightFactory factory, int gameType)
        {
			List<string> coordinates = new List<string>();
			string cood1 = "", cood2 = "", cood3 = "", cood4 = "", cood5 = "";
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
                    int rand = rnd.Next(1, 50);
					if (rand >= 48)
					{
						if (identifier.Equals("m"))
						{
							cood1 += this.builder.BuildBattleShipTile(i, j, identifier, width, height, AttackTileCommand, newStackPanel, factory);
							cood2 += this.builder.BuildCarrierTile(i, j, identifier, width, height, AttackTileCommand, newStackPanel, factory);
							cood3 += this.builder.BuildDestroyerTile(i, j, identifier, width, height, AttackTileCommand, newStackPanel, factory);
							cood4 += this.builder.BuildPatrolBoatTile(i, j, identifier, width, height, AttackTileCommand, newStackPanel, factory);

							if (gameType == 2 || gameType == 3)
							{
								cood5 += this.builder.BuildSubmarineTile(i, j, identifier, width, height, AttackTileCommand, newStackPanel, factory);
							}
						}
						else
							this.builder.BuildPartSeaTile(i, j, identifier, width, height, AttackTileCommand, newStackPanel, prototype, factory);
					}
					else
						this.builder.BuildPartSeaTile(i, j, identifier, width, height, AttackTileCommand, newStackPanel, prototype, factory);
                }
            }
			MessageBox.Show("cood1: " + cood1 + "\n" + "cood2: " + cood2 + "\n" + cood3 + "cood3: " + "\n" + "cood4: " + cood4 + "\n" + "cood5: " + cood5 + "\n");
		}
	}
}
