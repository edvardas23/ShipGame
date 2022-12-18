using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game;
using GameClient.MVVM.Model.TileModels;

namespace GameClient.MVVM.Visitor
{
	public class ConcreteVisitor : IVisitor
	{
		public bool printedSeaTile = false;
		public bool printedRockTile = false;
		public bool printedIslandTile = false;
		public void VisitIslandTile(IslandTile islandtile)
		{
			if(printedIslandTile == false)
			{
				MainWindow.AppWindow.ShipCoord.Text += "IslandTile coordinates are: X - " + islandtile.X + " Y - " + islandtile.Y + "\n";
				printedIslandTile = true;
			}
		}

		public void VisitRockTile(RockTile rocktile)
		{
			if(printedRockTile == false)
			{
				MainWindow.AppWindow.ShipCoord.Text += "RockTile coordinates are: X - " + rocktile.X + " Y - " + rocktile.Y + "\n";
				printedRockTile = true;
			}
		}

		public void VisitSeaTile(SeaTile seatile)
		{
			if(printedSeaTile == false)
			{
				MainWindow.AppWindow.ShipCoord.Text += "SeaTile coordinates are: X - " + seatile.X + " Y - " + seatile.Y + "\n";
				printedSeaTile = true;
			}
		}
	}
}
