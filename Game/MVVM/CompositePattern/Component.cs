using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient.MVVM.CompositePattern
{
	public abstract class Component
	{
		public string name { get; set; }
		public Component(string name)
		{
			this.name = name;
		}
		public abstract string DisplayResult(int indent);
	}
}
