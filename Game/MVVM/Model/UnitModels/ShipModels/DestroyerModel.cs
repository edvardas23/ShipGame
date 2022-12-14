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

        public DestroyerModel(Ship ship, string name) : base(name)
		{
            this.specialAbility = SpecialAbility;
		}
		public DestroyerModel(Unit ship, string name) : base(name)
		{
			this.specialAbility = SpecialAbility;
		}

		public DestroyerModel(string name) : base(name)
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
		public override string DisplayResult(int indent)
		{
			return (new String('-', indent) + this.GetType().Name);
		}
	}
}
