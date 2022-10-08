using GameClient.MVVM.Model.TileModels;
using GameClient.MVVM.Model.UnitModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient.MVVM.Model
{
    class Player
    {
        public string Username { get; set; }
        public string UID { get; set; }

        public IAlly MyMap { get; set; }
        public IEnemy EnemyMap { get; set; }

        List<Tile> tiles { get; set; }
        public Player()
        {
            MyMap = new Map(10, tiles);
            EnemyMap = null;
        }
        public void SetEnemy(Player enemy)
        {
            EnemyMap = (IEnemy)enemy.EnemyMap;
        }
        public bool Fire(Tile tile)
        {
            if (EnemyMap == null) return false;

            return EnemyMap.TakeShot(tile);
        }
        public bool AddShip(Ship ship)
        {
            return MyMap.AddShip(ship);
        }
    }
}
