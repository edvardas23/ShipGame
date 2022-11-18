using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient.MVVM.Model.UnitModels.ShipModels
{
    public class DestroyerModel : Ship
    {
        private int specialAbility;
        private string name = "Destroyer";

        public int SpecialAbility
        {
            get { return specialAbility; }
        }

        public DestroyerModel(Ship ship) : base(ship)
        {
            this.specialAbility = SpecialAbility;
        }

        public DestroyerModel() : base()
        {
        }

        public void UseSpecialAbility()
        {

        }
    }
}
