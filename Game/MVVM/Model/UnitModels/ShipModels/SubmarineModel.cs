using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient.MVVM.Model.UnitModels.ShipModels
{
    class SubmarineModel : Ship
    {
        private int specialAbility;
        private int isSubmerged;
        private string name = "Submarine";

        public int SpecialAbility
        {
            get { return specialAbility; }
        }

        public int IsSubmerged
        {
            get { return isSubmerged; }
        }

        public SubmarineModel(Ship ship) : base(ship)
        {
            this.specialAbility = SpecialAbility;
            this.isSubmerged = IsSubmerged;
        }

        public SubmarineModel()
        {
        }

        public void UseSpecialAbility()
        {

        }
    }
}
