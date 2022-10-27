﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient.MVVM.Model.UnitModels.ShipModels
{
    public class BattleshipModel : Ship
    {
        private int specialAbility;

        public int SpecialAbility
        {
            get { return specialAbility; }
        }

        public BattleshipModel(Ship ship) : base(ship)
        {
            this.specialAbility = SpecialAbility;
        }
        public void UseSpecialAbility()
        {

        }
    }
}
