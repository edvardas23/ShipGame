using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient.MVVM.Model.UnitModels.ShipModels
{
    class CarrierModel : Ship
    {
        private int specialAbility;
        private string name = "Carrier";

        public int SpecialAbility
        {
            get { return specialAbility; }
        }

        public CarrierModel(Ship ship) : base(ship)
        {
            this.specialAbility = SpecialAbility;
        }

        public CarrierModel()
        {
        }

        public void UseSpecialAbility()
        {

        }
    }
}
