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
    public class DestroyerDecorated : Decorator
    {
        protected readonly List<string> bombs = new List<string>();
        public DestroyerDecorated(Ship ship, string name) : base(ship, name) { }
        public void AddBomb(string name)
        {
            bombs.Add(name);
        }
        public void DropBomb(string name)
        {
            if (bombs.Contains(name))
            {
                bombs.Remove(name);
            }
            else
            {
                MessageBox.Show("Weapon doesn't exist");
            }
        }
        public override string Display()
        {
            base.Display();
            string msg = "Bombs:\n";
            foreach (string bomb in bombs)
            {
                msg = msg + bomb + "\n";
            }
            return msg;
        }
        public void SetButtonBackground(Button button)
        {
            button.Background = Brushes.CadetBlue;
        }

        public override string DisplaySunk()
        {
            throw new NotImplementedException();
        }
    }
}
