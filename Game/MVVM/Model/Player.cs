using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient.MVVM.Model
{
    class Player
    {
        public string Username { get; set; }
        public string UID { get; set; }
        public Map MyMap { get; set; }
        public Map EnemyMap { get; set; } 
        public Player()
        {

        }
    }
}
