
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
	public class PrimitiveRefResourcesSetter : Setter<PrimitiveRef, IEnumerable<Resource>>, IPrimitiveRefSetter
	{
		
		public PrimitiveRefResourcesSetter(IEnumerable<Resource> Value) : base(Value)
		{
		}

		public override PrimitiveRef Set(PrimitiveRef Component)
		{
			foreach (Resource resource in Value)
			{
				Component.AddResource(resource.Name, resource.Object);
			}
			return Component;
		}
	}
}
