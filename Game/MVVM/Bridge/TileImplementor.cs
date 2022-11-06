using GameClient.MVVM.Model.TileModels;
using GameClient.MVVM.Model.UnitModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient.MVVM.Bridge
{
    public abstract class TileImplementor //Implementor
    {
        public abstract Tile AddTile(object obj);
        public abstract void ShowAllRecords();
    }
}