using GameClient.MVVM.Model.UnitModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient.MVVM.Chain
{
	public interface IHandler
	{
		IHandler SetNext(IHandler handler);

		object Handle(object request);
	}

	abstract class AbstractHandler : IHandler
	{
		private IHandler _nextHandler;

		public IHandler SetNext(IHandler handler)
		{
			this._nextHandler = handler;

			return handler;
		}

		public virtual object Handle(object request)
		{
			if (this._nextHandler != null)
			{
				return this._nextHandler.Handle(request);
			}
			else
			{
				return null;
			}
		}
	}
	class BattleshipHandler : AbstractHandler
	{
		public override object Handle(object request)
		{
			if (request.ToString() == "bt")
			{
				return "Battleship";
			}
			else
			{
				return base.Handle(request);
			}
		}
	}

	class CarrierHandler : AbstractHandler
	{
		public override object Handle(object request)
		{
			if (request.ToString() == "cr")
			{
				return "Carrier";
			}
			else
			{
				return base.Handle(request);
			}
		}
	}
	class DestroyerHandler : AbstractHandler
	{
		public override object Handle(object request)
		{
			if (request.ToString() == "dt")
			{
				return "Destroyer";
			}
			else
			{
				return base.Handle(request);
			}
		}
	}
	class SubmarineHandler : AbstractHandler
	{
		public override object Handle(object request)
		{
			if (request.ToString() == "sb")
			{
				return "Submarine";
			}
			else
			{
				return base.Handle(request);
			}
		}
	}
	class PatrolBoatHandler : AbstractHandler
	{
		public override object Handle(object request)
		{
			if (request.ToString() == "pb")
			{
				return "Patrol Boatas";
			}
			else
			{
				return base.Handle(request);
			}
		}
	}
	class Client
	{
		public string ClientCode(AbstractHandler handler)
		{
			foreach (var tipas in new List<String> { "bt", "cr", "dt", "pb", "sb" })
			{
				
				var result = handler.Handle(tipas);

				if (result != null)
				{
					return result.ToString();
				}
			}
			return null;
		}
	}
}
