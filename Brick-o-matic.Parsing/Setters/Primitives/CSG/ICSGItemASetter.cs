
using Brick_o_matic.Math;
using Brick_o_matic.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.Parsing.Setters
{
	public class ICSGItemASetter : Setter<ICSG, IPrimitive>, ICSGSetter
	{
		
		public ICSGItemASetter(IPrimitive Value) : base(Value)
		{
		}

		public override ICSG Set(ICSG Component)
		{
			Component.ItemA = Value;
			return Component;
		}
	}
}
