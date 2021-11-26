
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
	public class SwitchPositionSetter : Setter<Switch, Position>, ISwitchSetter
	{
		
		public SwitchPositionSetter(Position Value) : base(Value)
		{
		}

		public override Switch Set(Switch Component)
		{
			Component.Position = Value;
			return Component;
		}
	}
}
