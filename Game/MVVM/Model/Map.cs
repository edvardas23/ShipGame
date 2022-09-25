using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient.MVVM.Model
{
    class Map
    {
        public int[,] myMap { get; set; }
        public Map(int mapSize)
        {
            myMap = new int [mapSize,mapSize];
        }
    }
}
