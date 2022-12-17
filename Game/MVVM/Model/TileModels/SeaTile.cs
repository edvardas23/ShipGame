using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameClient.MVVM.Visitor;

namespace GameClient.MVVM.Model.TileModels
{
	public class SeaTile : Tile, IVisitorTile
	{
		int x { get; set; }
		int y { get; set; }

		public SeaTile(Tile tile) : base(tile.X, tile.Y)
        {
            tile.destroyable = false;
            tile.placeable = true;
			this.x = tile.X;
			this.y = tile.Y;
		}

        public SeaTile(int x, int y) : base(x, y)
        {
			this.x = x;
			this.y = y;
		}

		public void Accept(IVisitor visitor)
		{
			visitor.VisitSeaTile(this);
		}
	}
}
