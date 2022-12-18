using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient.MVVM.Composite
{
	public abstract class Component
	{
		public string Name { get; set; }
		public Component() { }
		public Component(string name) 
		{
			this.Name = name;
		}
		public abstract string Display();
	}
}
