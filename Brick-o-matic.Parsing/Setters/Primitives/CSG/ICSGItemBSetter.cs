
using Brick_o_matic.Math;
using Brick_o_matic.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.Parsing.Setters
{
	public class ICSGItemBSetter : Setter<ICSG, IPrimitive>, ICSGSetter
	{
		
		public ICSGItemBSetter(IPrimitive Value) : base(Value)
		{
		}

		public override ICSG Set(ICSG Component)
		{
			Component.ItemB = Value;
			return Component;
		}
	}
}
