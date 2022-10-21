using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient.MVVM.Model.ShotModels
{
    class ShootStrategy
    {
        private IStrategy _strategy;

        public ShootStrategy() { }
        public ShootStrategy(IStrategy strategy)
        {
            this._strategy = strategy;
        }
        public void SetStrategy(IStrategy strategy)
        {
            this._strategy = strategy;
        }
        public int SetDamage()
        {
            int dmg = this._strategy.Damage();
            return dmg;
        }
    }
    public interface IStrategy
    {
        int Damage();
    }

    public class ClassicShot : IStrategy
    {
        public int Damage()
        {
            return 1;
        }
    }
    public class FugueShoot : IStrategy
    {
        public int Damage()
        {
            return 4;
        }
    }
}
