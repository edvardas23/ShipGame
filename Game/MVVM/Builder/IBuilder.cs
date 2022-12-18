using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using GameClient.MVVM.Core;
using GameClient.MVVM.Model.PrototypeModels;
using GameClient.MVVM.Model.UnitModels.FlyWeight;
using GameClient.MVVM.Visitor;

namespace GameClient.MVVM.Builder
{
    public interface IBuilder
    {
        void BuildPartSeaTile(int x, int y, string identifier, double width, double height, RelayCommand AttackTileCommand, StackPanel newStackPanel, Prototype prototype, FlyweightFactory factory, ConcreteVisitor visitor);

        void BuildPartRockTile(int x, int y, string identifier, double width, double height, RelayCommand AttackTileCommand, StackPanel newStackPanel, FlyweightFactory factory, ConcreteVisitor visitor);
		string BuildBattleShipTile(int x, int y, string identifier, double width, double height, RelayCommand AttackTileCommand, StackPanel newStackPanel, FlyweightFactory factory);
		string BuildCarrierTile(int x, int y, string identifier, double width, double height, RelayCommand AttackTileCommand, StackPanel newStackPanel, FlyweightFactory factory);
		string BuildDestroyerTile(int x, int y, string identifier, double width, double height, RelayCommand AttackTileCommand, StackPanel newStackPanel, FlyweightFactory factory);
		string BuildPatrolBoatTile(int x, int y, string identifier, double width, double height, RelayCommand AttackTileCommand, StackPanel newStackPanel, FlyweightFactory factory);
		string BuildSubmarineTile(int x, int y, string identifier, double width, double height, RelayCommand AttackTileCommand, StackPanel newStackPanel, FlyweightFactory factory);
	}
}
