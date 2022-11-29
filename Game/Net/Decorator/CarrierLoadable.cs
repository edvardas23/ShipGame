using GameClient.MVVM.Model.UnitModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GameClient.Net.Decorator
{
    public class CarrierLoadable : Decorator
    {
        protected readonly List<string> planes = new List<string>();
        public CarrierLoadable(Ship ship) : base(ship) { }
        public void AddPlane(string name)
        {
            planes.Add(name);
        }
        public void RemovePlane(string name)
        {
            if (planes.Contains(name))
            {
                planes.Remove(name);
            }
            else
            {
                MessageBox.Show("Plane doesn't exist");
            }
        }
        public sealed override string Display()
        {
            base.Display();
            string msg = "Planes:\n";
            foreach (string plane in planes)
            {
                msg = msg + plane + "\n";
            }
            return msg;
        }
        public void SetButtonBackground(Button button)
        {
            button.Background = Brushes.LightGreen;
        }
    }
}