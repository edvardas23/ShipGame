using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using GameClient.MVVM.Core;
using GameClient.MVVM.Model.PrototypeModels;
using GameClient.MVVM.Model.UnitModels.FlyWeight;

namespace GameClient.MVVM.Builder
{
    public interface IBuilder
    {
        void BuildPartSeaTile(int x, int y, string identifier, double width, double height, RelayCommand AttackTileCommand, StackPanel newStackPanel, Prototype prototype, FlyweightFactory factory);

        void BuildPartRockTile(int x, int y, string identifier, double width, double height, RelayCommand AttackTileCommand, StackPanel newStackPanel, FlyweightFactory factory);
    }
}
