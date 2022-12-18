using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient.MVVM.Composite
{
	public class Composite : Component
	{
		protected List<Component> _children = new List<Component>();

		public void Add(Component component)
		{
			this._children.Add(component);
		}

		public void Remove(Component component)
		{
			this._children.Remove(component);
		}
		public override string Display()
		{
			int i = 0;
			string result = "Branch(";

			foreach (Component component in this._children)
			{
				result += component.Display();
				if (i != this._children.Count - 1)
				{
					result += "+";
				}
				i++;
			}

			return result + ")";
		}
	}
}
