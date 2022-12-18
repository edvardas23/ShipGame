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
    public class PatrolBoatArmed : Decorator
    {
        protected readonly List<string> weapons = new List<string>();
        public PatrolBoatArmed(Ship ship, string name) : base(ship, name) { }
        public void AddWeapon(string name)
        {
            weapons.Add(name);
        }
        public void DropWeapon(string name)
        {
            if (weapons.Contains(name))
            {
                weapons.Remove(name);
            }
            else
            {
                MessageBox.Show("Weapon doesn't exist");
            }
        }
        public sealed override string Display()
        {
            base.Display();
            string msg = "Weapons:\n";
            foreach (string weapon in weapons)
            {
                msg = msg + weapon + "\n";
            }
            return msg;
        }
        public void SetButtonBackground(Button button)
        {
            button.Background = Brushes.DarkOrange;
        }
    }
}