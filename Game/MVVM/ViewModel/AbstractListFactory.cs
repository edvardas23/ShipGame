using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameClient.MVVM.Model.UnitModels;
using GameClient.MVVM.Model.UnitModels.ShipModels;

namespace GameClient.MVVM.ViewModel
{
    public class AbstractListFactory : AbstractFactory
    {

        public override Ship CreateBattleship()
        {
            BattleshipModel model = new BattleshipModel("Battleship");
            return model;
        }
        public override Ship CreateCarrier()
        {
            CarrierModel model = new CarrierModel("Carrier");
            return model;
        }
        public override Ship CreateDestroyer()
        {
            DestroyerModel model = new DestroyerModel("Destroyer");
            return model;
        }
        public override Ship CreatePatrol()
        {
            PatrolBoatModel model = new PatrolBoatModel("Patrol boat");
            return model;
        }
        public override Ship CreateSubmarine()
        {
            SubmarineModel model = new SubmarineModel("Submarine");
            return model;
        }
    }
}
