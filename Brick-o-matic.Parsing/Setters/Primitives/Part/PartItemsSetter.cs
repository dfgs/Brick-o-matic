
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
	public class PartItemsSetter : Setter<Part, IEnumerable<IPrimitive>>, IPartSetter
	{
		
		public PartItemsSetter(IEnumerable<IPrimitive> Value) : base(Value)
		{
		}

		public override Part Set(Part Component)
		{
			foreach (IPrimitive child in Value)
			{
				Component.Add(child);
			}
			return Component;
		}
	}
}
