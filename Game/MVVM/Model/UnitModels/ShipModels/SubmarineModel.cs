using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient.MVVM.Model.UnitModels.ShipModels
{
    public class SubmarineModel : Ship
    {
        private int specialAbility;
        private bool isSubmerged;
        private string name = "Submarine";

        public int SpecialAbility
        {
            get { return specialAbility; }
        }

        public bool IsSubmerged
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

        public sealed override string Display()
        {
            string submerged;
            if (IsSubmerged)
            {
                submerged = "Ship is submerged";
            } else
            {
                submerged = "Ship is not submerged";
            }
            string msg = "Name ----- " + name + ". " + submerged;
            return msg;
        }

        public sealed override string DisplaySunk()
        {
            string submerged;
            if (IsSubmerged)
            {
                submerged = "submerged";
            }
            else
            {
                submerged = "submerged";
            }
            string msg = name + " has been sunk while it was " + submerged;
            return msg;
        }
    }
}
