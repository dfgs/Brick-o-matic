using Brick_o_matic.Math;
using Brick_o_matic.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.Parsing.Setters
{
	public class CSGBSetter : Setter<CSG, IPrimitive>, ICSGSetter
	{
		
		
		public CSGBSetter(IPrimitive Value) : base(Value)
		{
		}

		public override CSG Set(CSG Component)
		{
			Component.B = Value;
			return Component;
		}
	}
}
