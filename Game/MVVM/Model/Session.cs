using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient.MVVM.Model
{
    class Session
    {
        private static Session _instance = null;
        public int MapSize { get; set; }
        //public static Map myMap;
        public Session()
        {
            
        }
        public static Session Instance
        {
            get 
            {
                if (_instance == null)
                {
                    _instance = new Session();
                }
                return _instance;
            }
        }
    }
}