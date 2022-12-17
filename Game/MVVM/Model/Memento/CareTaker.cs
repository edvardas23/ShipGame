using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GameClient.MVVM.Model
{
	public class CareTaker
	{
		Memento _memento;
		public void Save(UIElementCollection elements)
		{
			_memento = new Memento
			{
				ElementStates = new bool[elements.Count]
			};

			for (int i = 0; i < elements.Count; i++)
			{
				_memento.ElementStates[i] = elements[i].IsEnabled;
			}
		}

		public void Restore(UIElementCollection elements)
		{
			for (int i = 0; i < elements.Count; i++)
			{
				elements[i].IsEnabled = _memento.ElementStates[i];
			}
		}
	}
}

