using Brick_o_matic.Math;
using Brick_o_matic.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.Parsing.Setters
{
	public class CSGASetter : Setter<CSG, IPrimitive>, ICSGSetter
	{
		
		
		public CSGASetter(IPrimitive Value) : base(Value)
		{
		}

		public override CSG Set(CSG Component)
		{
			Component.A = Value;
			return Component;
		}
	}
}
