﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient.MVVM.Model.UnitModels.ShipModels
{
    public class PatrolBoatModel : Ship
    {
        private int specialAbility;
        private string name = "Patrol Boat";

        public int SpecialAbility
        {
            get { return specialAbility; }
        }

        public PatrolBoatModel(Ship ship) : base(ship)
        {
            this.specialAbility = SpecialAbility;
		}
		public PatrolBoatModel(Unit ship) : base(ship)
		{
			this.specialAbility = SpecialAbility;
		}

		public PatrolBoatModel()
        {
        }

        public void UseSpecialAbility()
        {

        }
        public sealed override string Display()
        {
            string msg = "Name -----" + name;
            return msg;
        }

        public sealed override string DisplaySunk()
        {
            string msg = name + " has been sunk";
            return msg;
        }
    }
}
