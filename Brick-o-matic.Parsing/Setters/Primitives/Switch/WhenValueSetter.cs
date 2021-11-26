
using Brick_o_matic.Math;
using Brick_o_matic.Parsing.Setters;
using Brick_o_matic.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.Parsing.Setters
{
	public class WhenValueSetter : Setter<When, string>, IWhenSetter
	{
		
		public WhenValueSetter(string Value) : base(Value)
		{
		}

		public override When Set(When Component)
		{
			Component.Value = Value;
			return Component;
		}
	}
}
