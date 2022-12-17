using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameClient.MVVM.Visitor;

namespace GameClient.MVVM.Model.TileModels
{
    public class RockTile : Tile, IVisitorTile //RefinedAbstraction
    {
		int x { get; set; }
		int y { get; set; }
        public RockTile() { }
        public RockTile(Tile tile) : base(tile.X, tile.Y, tile.placeable, tile.destroyable)
        {
            tile.destroyable = tile.destroyable;
            this.placeable = tile.placeable;
			this.x = tile.X;
			this.y = tile.Y;
		}
        public RockTile(int x, int y) : base(x, y)
        {
			this.x = x;
			this.y = y;
        }
        public override void ShowAll()
        {
            // Add separator lines
            Console.WriteLine();
            Console.WriteLine("------------------------");
            base.ShowAll();
            Console.WriteLine("------------------------");
        }

		public void Accept(IVisitor visitor)
		{
			visitor.VisitRockTile(this);
		}
	}
}
