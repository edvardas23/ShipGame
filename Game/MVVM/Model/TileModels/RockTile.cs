using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient.MVVM.Model.TileModels
{
    internal class RockTile : Tile
    {

        public RockTile(Tile tile) : base(tile.X, tile.Y)
        {
            tile.destroyable = true;
            this.placeable = false;
        }
    }
}
