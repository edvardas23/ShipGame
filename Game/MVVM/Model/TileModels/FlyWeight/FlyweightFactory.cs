using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameClient.MVVM.Model.TileModels;

namespace GameClient.MVVM.Model.UnitModels.FlyWeight
{
	public class FlyweightFactory
	{
		private List<Tuple<Flyweight, string>> flyweights = new List<Tuple<Flyweight, string>>();

		public FlyweightFactory(params Tile[] args)
		{
			foreach (var elem in args)
			{
				flyweights.Add(new Tuple<Flyweight, string>(new Flyweight(elem), this.getTile(elem)));
			}
		}

		public string getTile(Tile tile)
		{
			List<bool> elements = new List<bool>();

			elements.Add(tile.placeable);
			elements.Add(tile.destroyable);

			elements.Sort();

			return string.Join("_", elements);
		}

		public Flyweight GetFlyweight(Tile sharedState)
		{
			string key = this.getTile(sharedState);

			if(flyweights.Where(t => t.Item2 == key).Count() == 0)
			{
				this.flyweights.Add(new Tuple<Flyweight, string>(new Flyweight(sharedState), key));
			}

			return this.flyweights.Where(t => t.Item2 == key).FirstOrDefault().Item1;
		}
	}
}
