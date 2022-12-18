using GameClient.MVVM.Model.UnitModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Xml.Linq;

namespace GameClient.Net.Decorator
{
    public class SubmarineDecorated : Decorator
    {
        protected readonly List<string> armors = new List<string>();
        public SubmarineDecorated(Ship ship, string name) : base(ship, name) { }
        public void AddArmor(string name)
        {
            armors.Add(name);
        }
        public void DropArmor(string name)
        {
            if (armors.Contains(name))
            {
                armors.Remove(name);
            }
            else
            {
                MessageBox.Show("Weapon doesn't exist");
            }
        }
        public sealed override string Display()
        {
            base.Display();
            string msg = "Armors:\n";
            foreach (string armor in armors)
            {
                msg = msg + armor + "\n";
            }
            return msg;
        }
        public void SetButtonBackground(Button button)
        {
            button.Background = Brushes.Aquamarine;
        }

        public override string DisplaySunk()
        {
            base.Display();
            string msg = "Armors:\n";
            foreach (string armor in armors)
            {
                msg = msg + armor + "\n";
            }
            return msg;
        }
    }
}
