
using Brick_o_matic.Math;
using Brick_o_matic.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.Parsing.Setters
{
	public class ICSGPositionSetter : Setter<ICSG, Position>, ICSGSetter
	{
		
		public ICSGPositionSetter(Position Value) : base(Value)
		{
		}

		public override ICSG Set(ICSG Component)
		{
			Component.Position = Value;
			return Component;
		}
	}
}
