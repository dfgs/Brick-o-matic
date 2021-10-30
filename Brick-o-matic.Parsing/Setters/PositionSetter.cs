using Brick_o_matic.Math;
using Brick_o_matic.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.Parsing.Setters
{
	public class PositionSetter : Setter<IPrimitive,Vector>
	{
		
		public PositionSetter(Vector Value) : base(Value)
		{
		}

		public override IPrimitive Set(IPrimitive Component)
		{
			Component.Position = Value;
			return Component;
		}
	}
}
