using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient.MVVM.Model.TileModels
{
    public class IslandTile : Tile //RefinedAbstraction
    {
        public IslandTile() { }
        public IslandTile(Tile tile) : base(tile.X, tile.Y)
        {
            tile.destroyable = true;
            this.placeable = false;
        }
        public IslandTile(int x, int y) : base(x, y)
        {

        }
        public override void ShowAll()
        {
            // Add separator lines
            Console.WriteLine();
            Console.WriteLine("------------------------");
            base.ShowAll();
            Console.WriteLine("------------------------");
        }
    }
}
