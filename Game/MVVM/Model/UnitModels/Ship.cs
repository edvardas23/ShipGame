using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameClient.MVVM.Model.TileModels;
using GameClient.MVVM.Model.UnitModels.ShipModels;

namespace GameClient.MVVM.Model.UnitModels
{
    public abstract class Ship : Unit
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

        public Ship(Unit unit, string name) : base(unit.Tiles, name)
        {
            size = unit.Tiles.Count;
            hp = unit.Tiles.Count;
        }
        public Ship(string name) : base(name) { }
        public void TakeDamage()
        {
            if (hp > 0) hp--;
        }
        public bool IsShipSunk()
        {
            return hp == 0;
        }
        public virtual string Display()
        {
            string msg;
            if (this.GetType() == typeof(SubmarineModel))
            {
                msg = this.Display();
            } else if (this.IsShipSunk())
            {
                msg = this.DisplaySunk();
            } else
            {
                msg = "HP: " + hp as string;
            }
            return msg;
        }
		public abstract string DisplaySunk();
    }
}
