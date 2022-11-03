using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient.MVVM.Model.PrototypeModels
{
    class Prototype
    {
        public string[] array = new string[10];


        public Prototype ShallowCopy()
        {
            return (Prototype)this.MemberwiseClone();
        }
    }
}
