using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using GameClient.MVVM.Core;
using GameClient.MVVM.Model.TileModels;
using GameClient.MVVM.ViewModel;

using System.Windows.Controls;
using System.Windows.Media;
using GameClient.MVVM.Model.UnitModels;
using GameClient.MVVM.Model.UnitModels.ShipModels;
using GameClient.MVVM.Model.FacadeModels;
using static GameClient.MVVM.Model.FacadeModels.Facade;
using GameClient.MVVM.Model.PrototypeModels;
using System.Windows;

namespace GameClient.MVVM.Builder
{

    internal class ConcreteBuilder : IBuilder
    {
        private Map map = new Map();

        public ConcreteBuilder()
        {
            this.Reset();
        }

        public void BuildPartRockTile(int x, int y, string identifier, double width, double height, RelayCommand AttackTileCommand, StackPanel newStackPanel)
        {
            Tile tile = new Tile(x, y);
            RockTile rockTile = new RockTile(tile);
            rockTile.Background = Brushes.Olive;
            rockTile.Name = identifier + x.ToString() + y.ToString();
            rockTile.Width = width;
            rockTile.Height = height;
            if (identifier.Equals("e"))
            {
                rockTile.Command = AttackTileCommand;
                rockTile.Click += MainViewModel.Button_Click;
            }
            else
            {

            }
            newStackPanel.Children.Add(rockTile);
            this.map.Add(rockTile);
        }

        public void BuildPartSeaTile(int x, int y, string identifier, double width, double height, RelayCommand AttackTileCommand, StackPanel newStackPanel, Prototype prototype)
        {
            Tile tile = new Tile(x, y);
            SeaTile seaTile = new SeaTile(tile);
            seaTile.Name = identifier + x.ToString() + y.ToString();
            seaTile.Width = width;
            seaTile.Height = height; 
            if (identifier.Equals("e"))
            {
                seaTile.Command = AttackTileCommand;
                seaTile.Click += MainViewModel.Button_Click;
            }
            else
            {
                for (int k = 0; k < 10; k++)
                {
                    if (y.ToString() + x.ToString() == prototype.array[k])
                    {
                        seaTile.Background = Brushes.Green;
                    }
                }
            }
            newStackPanel.Children.Add(seaTile);
            this.map.Add(seaTile);
        }

        public void Reset()
        {
            this.map = new Map();
        }

        public Map GetMap()
        {
            Map map = this.map;
            this.Reset();
            return map;
        }

    }
}
