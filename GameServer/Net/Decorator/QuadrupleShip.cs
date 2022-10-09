using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Net.Decorator
{
    internal class QuadrupleShip : IShape
    {
        public void Draw()
        {
            Console.WriteLine("Shape: QuadrupleShip");
        }
    }
}
