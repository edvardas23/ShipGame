using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameClient.MVVM.Visitor;

namespace GameClient.MVVM.Model.TileModels
{
	public class IslandTile : Tile, IVisitorTile //RefinedAbstraction
	{
		int x { get; set; }
		int y { get; set; }
		public IslandTile() { }
        public IslandTile(Tile tile) : base(tile.X, tile.Y)
        {
            tile.destroyable = true;
            this.placeable = false;
			this.x = tile.X;
			this.y = tile.Y;
		}
        public IslandTile(int x, int y) : base(x, y)
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
			visitor.VisitIslandTile(this);
		}
	}
}
