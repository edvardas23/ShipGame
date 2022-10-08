using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient.MVVM.Model.ShotModels
{
    class ShootStrategy
    {
        public interface IStrategy
        {
            object Damage(object data);
        }

        class ClassicShot : IStrategy
        {
            public object Damage(object data)
            {
                return 1;
            }
        }
        class FugueShoot : IStrategy
        {
            public object Damage(object data)
            {
                return 4;
            }
        }

    }
}
