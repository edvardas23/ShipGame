using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace GameClient.MVVM.CompositePattern
{
	public class Composite : Component
	{
		protected List<Component> _children = new List<Component>();
		public Composite(string name) : base(name) { }

		public void Add(Component component)
		{
			this._children.Add(component);
		}

		public void Remove(Component component)
		{
			this._children.Remove(component);
		}
		public override string DisplayResult(int indent)
		{
			string result = "";
			result += new String('-', indent) + " + " + name + " \n";

			foreach (Component component in this._children)
			{
				result += component.DisplayResult(indent + 2) + " \n";
			}

			return result;
		}
	}
}
