using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient.MVVM.Visitor
{
	public interface IVisitorTile
	{
		void Accept(IVisitor visitor);
	}
}
