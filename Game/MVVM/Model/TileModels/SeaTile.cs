using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient.MVVM.Model.TileModels
{
    internal class SeaTile : Tile
    {
        
        public SeaTile(Tile tile) : base(tile.X, tile.Y)
        {
            tile.destroyable = false;
            tile.placeable = true;
        }

        public SeaTile(int x, int y) : base(x, y)
        {

        }
    }
}
