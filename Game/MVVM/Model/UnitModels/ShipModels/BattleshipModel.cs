using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient.MVVM.Model.UnitModels.ShipModels
{
    class BattleshipModel : Ship
    {
        private int specialAbility;
        private readonly string name = "Battleship";

        public int SpecialAbility
        {
            get { return specialAbility; }
        }

        public BattleshipModel(Ship ship) : base(ship)
        {
            this.specialAbility = SpecialAbility;
        }

        public BattleshipModel(BattleshipModel other) : base(other)
        {
            other.specialAbility = SpecialAbility;
        }

        public BattleshipModel()
        {
        }

        public void UseSpecialAbility()
        {

        }

        public string GetName()
        {
            return name;
        }
    }
}
