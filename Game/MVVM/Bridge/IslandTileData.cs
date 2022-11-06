using GameClient.MVVM.Model.TileModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace GameClient.MVVM.Bridge
{
    public class IslandTileData : TileImplementor //ConcreteImplementor
    {
        private readonly List<Tile> tiles = new List<Tile>();

        private string name;
        private Brush color;
        public IslandTileData(string name, Brush color)
        {
            this.name = name;
            this.color = color;
        }
        public override Tile AddTile(object obj)
        {
            Tile tile = obj as Tile;
            tile.Name = name;
            tile.Background = color;
            tiles.Add(tile);
            return tile;
        }
        public override void ShowAllRecords()
        {
            string msg = "Existing tiles:\n";
            foreach (Tile tile in tiles)
            {
                msg = msg + tile.Name + "\n";
            }
            MessageBox.Show(msg);
        }
    }
}
