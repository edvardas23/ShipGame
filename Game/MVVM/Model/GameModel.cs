using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient.MVVM.Model
{
    public class GameModel
    {
        public Player firstPlayer { get; set; }
        public Player secondPlayer { get; set; }
        public bool IsGameStarted { get; set; }
        public GameModel()
        { 
            this.IsGameStarted = false;
        }
    }
}
