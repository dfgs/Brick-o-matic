using Brick_o_matic.Math;
using Brick_o_matic.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.Parsing.Setters
{
	public class PrimitiveRefNameSetter : Setter<PrimitiveRef, string>, IPrimitiveRefSetter
	{
		
		public PrimitiveRefNameSetter(string Value) : base(Value)
		{
		}

		public override PrimitiveRef Set(PrimitiveRef Component)
		{
			Component.Name = Value;
			return Component;
		}
	}
}
