using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GameClient.MVVM.Model.UnitModels.ShipModels
{
    public class BattleshipModel : Ship
    {
        private int specialAbility;
        private readonly string name = "Battleship";

        public int SpecialAbility
        {
            get { return specialAbility; }
        }

        public BattleshipModel(Ship ship, string name) : base(name)
		{
            this.specialAbility = SpecialAbility;
        }
		public BattleshipModel(Unit ship, string name) : base(name)
		{
			this.specialAbility = SpecialAbility;
		}

		public BattleshipModel(BattleshipModel other, string name) : base(other, name)
        {
            other.specialAbility = SpecialAbility;
        }

        public BattleshipModel(string name) : base(name)
        {
        }

        public void UseSpecialAbility()
        {

        }

        public string GetName()
        {
            return name;
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
		public override string DisplayResult(int indent)
		{
			return (new String('-', indent) + this.GetType().Name);
		}
	}
}
