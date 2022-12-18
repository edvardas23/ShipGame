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
using GameClient.MVVM.Model.UnitModels.FlyWeight;
using GameClient.MVVM.Visitor;

namespace GameClient.MVVM.Builder
{

    internal class ConcreteBuilder : IBuilder
    {
        private Map map = new Map();
		List<Tile> tiles = new List<Tile>();

		public List<Tile> GetTiles()
		{
			return tiles;
		}

        public ConcreteBuilder()
        {
            this.Reset();
        }

        public void BuildPartRockTile(int x, int y, string identifier, double width, double height, RelayCommand AttackTileCommand, StackPanel newStackPanel, FlyweightFactory factory, ConcreteVisitor visitor)
        {
			Tile temp_tile = new Tile(x, y, false, true);
			var flyweight = factory.GetFlyweight(temp_tile);
            Tile tile = flyweight.Operation(temp_tile, x, y);
            RockTile rockTile = new RockTile(tile);
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
				rockTile.Accept(visitor);
				rockTile.Background = Brushes.Olive;
			}
            newStackPanel.Children.Add(rockTile);
            this.map.Add(rockTile);
			tiles.Add(rockTile);
        }

        public void BuildPartSeaTile(int x, int y, string identifier, double width, double height, RelayCommand AttackTileCommand, StackPanel newStackPanel, Prototype prototype, FlyweightFactory factory, ConcreteVisitor visitor)
        {
			Tile temp_tile = new Tile(x, y, true, false);
			var flyweight = factory.GetFlyweight(temp_tile);
			Tile tile = flyweight.Operation(temp_tile, x, y);
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
				seaTile.Accept(visitor);
				for (int k = 0; k < 10; k++)
                {
                    if (y.ToString() + x.ToString() == prototype.array[k])
                    {
                        ///seaTile.Background = Brushes.Green;
                    }
                }
            }
            newStackPanel.Children.Add(seaTile);
            this.map.Add(seaTile);
			tiles.Add(seaTile);
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
