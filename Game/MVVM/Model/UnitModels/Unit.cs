using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using GameClient.MVVM.Bridge;
using GameClient.MVVM.CompositePattern;
using GameClient.MVVM.Model.TileModels;

namespace GameClient.MVVM.Model.UnitModels
{
    public class Unit : Component
    {
        private List<Tile> tiles;
        public List<Tile> Tiles
        {
            get { return tiles; }
		}
		public Unit(string name) : base(name) { }
        public Unit(List<Tile> tiles, string name) : base(name)
		{
            this.tiles = tiles;
        }

		public override string DisplayResult(int indent)
		{
			return "Unit";
		}
	}
}
