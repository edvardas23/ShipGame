using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameClient.MVVM.Model.UnitModels;
using GameClient.MVVM.Model.UnitModels.ShipModels;

namespace GameClient.MVVM.ViewModel
{
    class AbstractListFactory : AbstractFactory
    {

        public override Ship CreateBattleship()
        {
            BattleshipModel model = new BattleshipModel();
            return model;
        }
        public override Ship CreateCarrier()
        {
            CarrierModel model = new CarrierModel();
            return model;
        }
        public override Ship CreateDestroyer()
        {
            DestroyerModel model = new DestroyerModel();
            return model;
        }
        public override Ship CreatePatrol()
        {
            PatrolBoatModel model = new PatrolBoatModel();
            return model;
        }
        public override Ship CreateSubmarine()
        {
            SubmarineModel model = new SubmarineModel();
            return model;
        }
    }
}
