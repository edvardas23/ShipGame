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

        public void BuildPartRockTile(int x, int y, string identifier, double width, double height, RelayCommand AttackTileCommand, StackPanel newStackPanel, FlyweightFactory factory)
        {
			Tile temp_tile = new Tile(x, y, false, true);
			var flyweight = factory.GetFlyweight(temp_tile);
            Tile tile = flyweight.Operation(temp_tile, x, y);
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
			tiles.Add(rockTile);
			//var visitor = new ConcreteVisitor();
			//rockTile.Accept(visitor);
        }

        public void BuildPartSeaTile(int x, int y, string identifier, double width, double height, RelayCommand AttackTileCommand, StackPanel newStackPanel, Prototype prototype, FlyweightFactory factory)
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
			tiles.Add(seaTile);
			//var visitor = new ConcreteVisitor();
			//seaTile.Accept(visitor);
		}

		public string BuildBattleShipTile(int x, int y, string identifier, double width, double height, RelayCommand AttackTileCommand, StackPanel newStackPanel, FlyweightFactory factory)
		{
			string coordinates = "";
			Tile temp_tile = new Tile(x, y, false, true);
			var flyweight = factory.GetFlyweight(temp_tile);
			Tile tile = flyweight.Operation(temp_tile, x, y);
			coordinates += tile.X.ToString() + ";" + tile.Y.ToString() + " ";
			tile.Background = Brushes.Moccasin;
			tile.Name = identifier + x.ToString() + y.ToString();
			tile.Width = width;
			tile.Height = height;
			if (identifier.Equals("e"))
			{
				tile.Command = AttackTileCommand;
				tile.Click += MainViewModel.Button_Click;
			}
			else
			{

			}
			newStackPanel.Children.Add(tile);
			this.map.Add(tile);
			tiles.Add(tile);

			return coordinates;
		}
		public string BuildCarrierTile(int x, int y, string identifier, double width, double height, RelayCommand AttackTileCommand, StackPanel newStackPanel, FlyweightFactory factory)
		{
			string coordinates = "";
			//for (int i = 0; i < 2; i++)
			//{
				Tile temp_tile = new Tile(x, y, false, true);
				var flyweight = factory.GetFlyweight(temp_tile);
				//if (i == 0)
				//{
					Tile tile = flyweight.Operation(temp_tile, temp_tile.X, temp_tile.Y);
					coordinates += tile.X.ToString() + ";" + tile.Y.ToString() + " ";
					tile.Background = Brushes.LightGreen;
					tile.Name = identifier + x.ToString() + y.ToString();
					tile.Width = width;
					tile.Height = height;
					if (identifier.Equals("e"))
					{
						tile.Command = AttackTileCommand;
						tile.Click += MainViewModel.Button_Click;
					}
					else
					{

					}
					newStackPanel.Children.Add(tile);
					this.map.Add(tile);
					tiles.Add(tile);
				//}
				//if (i == 1)
				//{
					//Tile tile = flyweight.Operation(temp_tile, temp_tile.X + 1, temp_tile.Y);
					//coordinates += tile.X.ToString() + ";" + tile.Y.ToString() + " ";
					/*tile.Background = Brushes.LightGreen;
					tile.Name = identifier + x.ToString() + y.ToString();
					tile.Width = width;
					tile.Height = height;
					if (identifier.Equals("e"))
					{
						tile.Command = AttackTileCommand;
						tile.Click += MainViewModel.Button_Click;
					}
					else
					{

					}
					newStackPanel.Children.Add(tile);
					this.map.Add(tile);
					tiles.Add(tile);
				//}
			//}*/
			return coordinates;
		}
		public string BuildDestroyerTile(int x, int y, string identifier, double width, double height, RelayCommand AttackTileCommand, StackPanel newStackPanel, FlyweightFactory factory)
		{
			string coordinates = "";
			//for (int i = 0; i < 3; i++)
			//{
				//if (i == 0)
				//{
					Tile temp_tile = new Tile(x, y, false, true);
					var flyweight = factory.GetFlyweight(temp_tile);
					Tile tile = flyweight.Operation(temp_tile, temp_tile.X, temp_tile.Y);
					coordinates += tile.X.ToString() + ";" + tile.Y.ToString() + " ";
					tile.Background = Brushes.CadetBlue;
					tile.Name = identifier + x.ToString() + y.ToString();
					tile.Width = width;
					tile.Height = height;
					if (identifier.Equals("e"))
					{
						tile.Command = AttackTileCommand;
						tile.Click += MainViewModel.Button_Click;
					}
					else
					{

					}
					newStackPanel.Children.Add(tile);
					this.map.Add(tile);
					tiles.Add(tile);
				//}
				//if (i == 1)
				//{
					//Tile temp_tile = new Tile(x, y + 1, false, true);
					//var flyweight = factory.GetFlyweight(temp_tile);
					//Tile tile = flyweight.Operation(temp_tile, temp_tile.X, temp_tile.Y);
					//coordinates += tile.X.ToString() + ";" + tile.Y.ToString() + " ";
					/*tile.Background = Brushes.CadetBlue;
					tile.Name = identifier + x.ToString() + y.ToString();
					tile.Width = width;
					tile.Height = height;
					if (identifier.Equals("e"))
					{
						tile.Command = AttackTileCommand;
						tile.Click += MainViewModel.Button_Click;
					}
					else
					{

					}
					newStackPanel.Children.Add(tile);
					this.map.Add(tile);
					tiles.Add(tile);
				//}
				//if (i == 2)
				//{
					//Tile temp_tile = new Tile(x + 1, y, false, true);
					//var flyweight = factory.GetFlyweight(temp_tile);
					//Tile tile = flyweight.Operation(temp_tile, temp_tile.X, temp_tile.Y);
					//coordinates += tile.X.ToString() + ";" + tile.Y.ToString() + " ";
					tile.Background = Brushes.CadetBlue;
					tile.Name = identifier + x.ToString() + y.ToString();
					tile.Width = width;
					tile.Height = height;
					if (identifier.Equals("e"))
					{
						tile.Command = AttackTileCommand;
						tile.Click += MainViewModel.Button_Click;
					}
					else
					{

					}
					newStackPanel.Children.Add(tile);
					this.map.Add(tile);
					tiles.Add(tile);
				//}
			//}*/
			return coordinates;
		}
		public string BuildPatrolBoatTile(int x, int y, string identifier, double width, double height, RelayCommand AttackTileCommand, StackPanel newStackPanel, FlyweightFactory factory)
		{
			string coordinates = "";
			//for (int i = 0; i < 4; i++)
			//{
				Tile temp_tile = new Tile(x, y, false, true);
				var flyweight = factory.GetFlyweight(temp_tile);
			//	if (i == 0)
			//	{
					Tile tile = flyweight.Operation(temp_tile, temp_tile.X, temp_tile.Y);
					coordinates += tile.X.ToString() + ";" + tile.Y.ToString() + " ";
					tile.Background = Brushes.DarkOrange;
					tile.Name = identifier + x.ToString() + y.ToString();
					tile.Width = width;
					tile.Height = height;
					if (identifier.Equals("e"))
					{
						tile.Command = AttackTileCommand;
						tile.Click += MainViewModel.Button_Click;
					}
					else
					{

					}
					newStackPanel.Children.Add(tile);
					this.map.Add(tile);
					tiles.Add(tile);
				//}
				//if (i == 1)
				//{
					//Tile tile = flyweight.Operation(temp_tile, temp_tile.X + 1, temp_tile.Y);
					//coordinates += tile.X.ToString() + ";" + tile.Y.ToString() + " ";
					/*tile.Background = Brushes.DarkOrange;
					tile.Name = identifier + x.ToString() + y.ToString();
					tile.Width = width;
					tile.Height = height;
					if (identifier.Equals("e"))
					{
						tile.Command = AttackTileCommand;
						tile.Click += MainViewModel.Button_Click;
					}
					else
					{

					}
					newStackPanel.Children.Add(tile);
					this.map.Add(tile);
					tiles.Add(tile);
				//}
				//if (i == 2)
				//{
					//Tile tile = flyweight.Operation(temp_tile, temp_tile.X, temp_tile.Y + 1);
					//coordinates += tile.X.ToString() + ";" + tile.Y.ToString() + " ";
					tile.Background = Brushes.DarkOrange;
					tile.Name = identifier + x.ToString() + y.ToString();
					tile.Width = width;
					tile.Height = height;
					if (identifier.Equals("e"))
					{
						tile.Command = AttackTileCommand;
						tile.Click += MainViewModel.Button_Click;
					}
					else
					{

					}
					newStackPanel.Children.Add(tile);
					this.map.Add(tile);
					tiles.Add(tile);
				//}
				//if (i == 3)
				//{
					//Tile tile = flyweight.Operation(temp_tile, temp_tile.X, temp_tile.Y + 2);
					//coordinates += tile.X.ToString() + ";" + tile.Y.ToString() + " ";
					tile.Background = Brushes.DarkOrange;
					tile.Name = identifier + x.ToString() + y.ToString();
					tile.Width = width;
					tile.Height = height;
					if (identifier.Equals("e"))
					{
						tile.Command = AttackTileCommand;
						tile.Click += MainViewModel.Button_Click;
					}
					else
					{

					}
					newStackPanel.Children.Add(tile);
					this.map.Add(tile);
					tiles.Add(tile);
				//}
			//}*/
			return coordinates;
		}
		public string BuildSubmarineTile(int x, int y, string identifier, double width, double height, RelayCommand AttackTileCommand, StackPanel newStackPanel, FlyweightFactory factory)
		{
			string coordinates = "";
			//for (int i = 0; i < 5; i++)
			//{
				Tile temp_tile = new Tile(x, y, false, true);
				var flyweight = factory.GetFlyweight(temp_tile);
				//if (i == 0)
				//{
					Tile tile = flyweight.Operation(temp_tile, temp_tile.X, temp_tile.Y);
					coordinates += tile.X.ToString() + ";" + tile.Y.ToString() + " ";
					tile.Background = Brushes.Aquamarine;
					tile.Name = identifier + x.ToString() + y.ToString();
					tile.Width = width;
					tile.Height = height;
					if (identifier.Equals("e"))
					{
						tile.Command = AttackTileCommand;
						tile.Click += MainViewModel.Button_Click;
					}
					else
					{

					}
					newStackPanel.Children.Add(tile);
					this.map.Add(tile);
					tiles.Add(tile);
				//}
				//if (i == 1)
				//{
					//Tile tile = flyweight.Operation(temp_tile, temp_tile.X + 1, temp_tile.Y);
					//coordinates += tile.X.ToString() + ";" + tile.Y.ToString() + " ";
					/*tile.Background = Brushes.Aquamarine;
					tile.Name = identifier + x.ToString() + y.ToString();
					tile.Width = width;
					tile.Height = height;
					if (identifier.Equals("e"))
					{
						tile.Command = AttackTileCommand;
						tile.Click += MainViewModel.Button_Click;
					}
					else
					{

					}
					newStackPanel.Children.Add(tile);
					this.map.Add(tile);
					tiles.Add(tile);
				//}
				//if (i == 2)
				//{
					//Tile tile = flyweight.Operation(temp_tile, temp_tile.X, temp_tile.Y + 1);
					//coordinates += tile.X.ToString() + ";" + tile.Y.ToString() + " ";
					tile.Background = Brushes.Aquamarine;
					tile.Name = identifier + x.ToString() + y.ToString();
					tile.Width = width;
					tile.Height = height;
					if (identifier.Equals("e"))
					{
						tile.Command = AttackTileCommand;
						tile.Click += MainViewModel.Button_Click;
					}
					else
					{

					}
					newStackPanel.Children.Add(tile);
					this.map.Add(tile);
					tiles.Add(tile);
				//}
				//if (i == 3)
				//{
					//Tile tile = flyweight.Operation(temp_tile, temp_tile.X, temp_tile.Y + 2);
					//coordinates += tile.X.ToString() + ";" + tile.Y.ToString() + " ";
					tile.Background = Brushes.Aquamarine;
					tile.Name = identifier + x.ToString() + y.ToString();
					tile.Width = width;
					tile.Height = height;
					if (identifier.Equals("e"))
					{
						tile.Command = AttackTileCommand;
						tile.Click += MainViewModel.Button_Click;
					}
					else
					{

					}
					newStackPanel.Children.Add(tile);
					this.map.Add(tile);
					tiles.Add(tile);
				//}
				//if (i == 4)
				//{
					//Tile tile = flyweight.Operation(temp_tile, temp_tile.X + 1, temp_tile.Y + 2);
					//coordinates += tile.X.ToString() + ";" + tile.Y.ToString() + " ";
					tile.Background = Brushes.Aquamarine;
					tile.Name = identifier + x.ToString() + y.ToString();
					tile.Width = width;
					tile.Height = height;
					if (identifier.Equals("e"))
					{
						tile.Command = AttackTileCommand;
						tile.Click += MainViewModel.Button_Click;
					}
					else
					{

					}
					newStackPanel.Children.Add(tile);
					this.map.Add(tile);
					tiles.Add(tile);
				//}
			//}*/
			return coordinates;
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