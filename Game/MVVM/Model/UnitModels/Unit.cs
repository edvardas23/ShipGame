using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameClient.MVVM.Model.TileModels;

namespace GameClient.MVVM.Model.UnitModels
{
    public class Unit
    {

        private List<Tile> tiles;

        public List<Tile> Tiles
        {
            get { return tiles; }
        }

        public Unit(List<Tile> tiles)
        {
            this.tiles = tiles;
        }
        public Unit() { }
    }
}
