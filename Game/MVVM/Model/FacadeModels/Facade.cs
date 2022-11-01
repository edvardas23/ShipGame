using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient.MVVM.Model.FacadeModels
{
    public class Facade
    {
/*        protected ClassicModeSubsystem _classicModeSubsystem;

        protected AdvancedModeSubsystem _advancedModeSubsystem;

        protected TurboModeSubsystem _turboModeSubsystem;*/

        public Facade(int type)
        {
            if(type == 1)
            {
                ClassicModeSubsystem mode = new ClassicModeSubsystem();
                mode.ModeName();
            }
            if (type == 2)
            {
                ClassicModeSubsystem mode = new ClassicModeSubsystem();
                mode.ModeName();
            }
            if (type == 3)
            {
                ClassicModeSubsystem mode = new ClassicModeSubsystem();
                mode.ModeName();
            }
        }
        
   
        public class ClassicModeSubsystem
        {

            public string ModeName()
            {
                return "Klasikinis žaidimo režimas";
            }
        }
        public class AdvancedModeSubsystem
        {

            public string ModeName()
            {
                return "Papildytas žaidimo režimas";
            }
        }
        public class TurboModeSubsystem
        {

            public string ModeName()
            {
                return "Turbo žaidimo režimas";
            }
        }
    }
}
