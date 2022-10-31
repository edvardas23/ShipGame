﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameClient.MVVM.Model.UnitModels;
using GameClient.MVVM.Model;
using GameClient.MVVM.Model.TileModels;
using GameClient.MVVM.Model.UnitModels.ShipModels;

namespace GameClient.MVVM.ViewModel
{
    abstract class AbstractFactory
    {
        public List<Ship> Ships = new List<Ship>();

        public abstract Ship CreateBattleship();
        public abstract Ship CreateCarrier();
        public abstract Ship CreateDestroyer();
        public abstract Ship CreatePatrol();
        public abstract Ship CreateSubmarine();

        public List<Ship> GetShipsList(string gameMode)
        {
            switch(gameMode)
            {
                case "Klasikinis":
                    Ships.Add(CreateBattleship());
                    Ships.Add(CreateCarrier());
                    Ships.Add(CreateDestroyer());
                    Ships.Add(CreatePatrol());
                    break;
                case "Papildytas":
                    Ships.Add(CreateBattleship());
                    Ships.Add(CreateCarrier());
                    Ships.Add(CreateDestroyer());
                    Ships.Add(CreatePatrol());
                    Ships.Add(CreateSubmarine());
                    break;
                case "Turbo":
                    Ships.Add(CreateBattleship());
                    Ships.Add(CreateCarrier());
                    Ships.Add(CreateDestroyer());
                    Ships.Add(CreatePatrol());
                    Ships.Add(CreateSubmarine());
                    break;
            }
            return Ships;
        }

    }
}
