using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient.MVVM.Model.UnitModels.ShipModels
{
    class PatrolBoatModel : Ship
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

        public PatrolBoatModel()
        {
        }

        public void UseSpecialAbility()
        {

        }
    }
}
