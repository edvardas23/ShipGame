using GameClient.MVVM.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameClient.MVVM.Model.PrototypeModels;
using GameClient.MVVM.ViewModel;
using GameClient.MVVM.Model.UnitModels;

namespace GameClient.MVVM.Mediator
{

	public interface IMediator
	{
		void Notify(object sender, string ev);

		void Notify(object sender, string ev, List<Ship> ships);
	}

	// Concrete Mediators implement cooperative behavior by coordinating several
	// components.
	class ConcreteMediator : IMediator
	{
		private Component1 _component1;

		private Component2 _component2;

		public ConcreteMediator(Component1 component1, Component2 component2)
		{
			this._component1 = component1;
			this._component1.SetMediator(this);
			this._component2 = component2;
			this._component2.SetMediator(this);
		}

		public void Notify(object sender, string ev)
		{
			if (ev == "A")
			{
				this._component2.DoC();
			}
			if (ev == "D")
			{
				this._component2.DoC();
			}
			if (ev == "B")
			{
				this._component2.DoD();
			}

		}
		public void Notify(object sender, string ev, List<Ship> ships)
		{

		}
	}

	// The Base Component provides the basic functionality of storing a
	// mediator's instance inside component objects.
	class BaseComponent
	{
		protected IMediator _mediator;

		public BaseComponent(IMediator mediator = null)
		{
			this._mediator = mediator;
		}

		public void SetMediator(IMediator mediator)
		{
			this._mediator = mediator;
		}
	}

	// Concrete Components implement various functionality. They don't depend on
	// other components. They also don't depend on any concrete mediator
	// classes.
	class Component1 : BaseComponent
	{
		public Prototype DoA(Prototype prototype)
		{
			Prototype clone = prototype.ShallowCopy(); 
			this._mediator.Notify(this, "A");
			return clone;
			
		}

		public List<Ship> DoB()
		{
			AbstractFactory abstr = new AbstractListFactory();
			List<Ship> ships = abstr.GetShipsList();
			if( ships != null)
			{
				return ships;
			}
			this._mediator.Notify(this, "B");
			return null;
		}
	}

	class Component2 : BaseComponent
	{
		public void DoC()
		{
			// nothing to do atm
			this._mediator.Notify(this, "C");
		}

		public void DoD()
		{
			throw new InvalidOperationException("Failed to get ship list" );
			//this._mediator.Notify(this, "D");
		}
	}
}
