using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameClient.MVVM.Model.TileModels;

namespace GameClient.MVVM.Visitor
{
	public interface IVisitor
	{
		void VisitSeaTile(SeaTile seatile);
		void VisitRockTile(RockTile rocktile);
		void VisitIslandTile(IslandTile islandtile);
	}
}
