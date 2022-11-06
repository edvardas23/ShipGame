using GameClient.MVVM.Model.TileModels;
using GameClient.MVVM.Model.UnitModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace GameClient.MVVM.Bridge
{
    public class RockTileData : TileImplementor //ConcreteImplementor
    {
        private readonly List<Tile> tiles = new List<Tile>();

        private string name;
        private Brush color;
        public RockTileData(string name, Brush color)
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
