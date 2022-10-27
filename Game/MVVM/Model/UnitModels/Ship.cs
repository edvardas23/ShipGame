using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameClient.MVVM.Model.TileModels;

namespace GameClient.MVVM.Model.UnitModels
{
    public class Ship : Unit
    {
        private int size;
        private int hp;

        public int Size
        {
            get { return size; }
        }

        public int HP
        {
            get { return hp; }
        }

        public Ship(Unit unit) : base(unit.Tiles)
        {
            //size = unit.Tiles.Count;
            hp = unit.Tiles.Count;
        }
        public void TakeDamage()
        {
            if (hp > 0) hp--;
        }
        public bool IsShipSunk()
        {
            return hp == 0;
        }
    }
}
