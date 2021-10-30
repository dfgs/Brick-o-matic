using Brick_o_matic.Math;
using Brick_o_matic.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.Parsing.Setters
{
	public class BrickSizeSetter : Setter<Brick,Vector>, IBrickSetter
	{
		
		public BrickSizeSetter(Vector Value) : base(Value)
		{
		}

		public override Brick Set(Brick Component)
		{
			Component.Size = Value;
			return Component;
		}
	}
}
