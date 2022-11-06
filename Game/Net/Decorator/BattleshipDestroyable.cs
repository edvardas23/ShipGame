using GameClient.MVVM.Model.UnitModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GameClient.Net.Decorator
{
    public class BattleshipDestroyable : Decorator
    {
        protected readonly List<string> rockets = new List<string>();

        public BattleshipDestroyable(Ship ship) : base(ship) { }
        public void AddRocket(string rocketName)
        {
            rockets.Add(rocketName);
        }
        public void ShootRocket(string name)
        {
            if (rockets.Contains(name))
            {
                rockets.Remove(name);
            }
            else
            {
                MessageBox.Show("Rocket doesn't exist");
            }
        }
        public override string Display()
        {
            base.Display();
            string msg = "Rockets:\n";
            foreach (string rocket in rockets)
            {
                msg = msg + rocket + "\n";
            }
            return msg;
        }
        public void SetButtonBackground(Button button) 
        {
            button.Background = Brushes.Moccasin;
        }
    }
}