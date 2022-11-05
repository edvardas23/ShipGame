using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameClient.MVVM.Model.TileModels;

namespace GameClient.MVVM.Builder
{
    public class Map
    {
        private List<Tile> tiles = new List<Tile>();

        public void Add(Tile part)
        {
            this.tiles.Add(part);
        }

    }
}
