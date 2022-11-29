using GameClient.MVVM.Model.UnitModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient.Net.Decorator
{
    public abstract class Decorator : Ship
    {
        protected Ship ship;
        public Decorator(Ship ship)
        {
            this.ship = ship;
        }
        public void SetShip(Ship ship)
        {
            this.ship = ship;
        }
        public override string Display()
        {
            if (ship != null)
            {
                return ship.Display();
            }
            else
            {
                return "";
            }
        }

        public override string DisplaySunk()
        {
            if (ship != null)
            { 
                return ship.DisplaySunk();
            } else
            {
                return "";
            }
        }
    }
}
