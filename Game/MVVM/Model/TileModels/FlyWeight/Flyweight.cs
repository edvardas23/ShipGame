using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameClient.MVVM.Model.TileModels;

namespace GameClient.MVVM.Model.UnitModels.FlyWeight
{
	public class Flyweight
	{
		private Tile _sharedState;

		public Flyweight(Tile tile)
		{
			this._sharedState = tile;
		}

		public Tile Operation(Tile uniqueState, int x, int y)
		{
			uniqueState.X = x;
			uniqueState.Y = y;
			return uniqueState;
		}
	}
}
