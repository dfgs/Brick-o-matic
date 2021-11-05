
using Brick_o_matic.Math;
using Brick_o_matic.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.Parsing.Setters
{
	public class IRotateItemSetter : Setter<IRotate, IPrimitive>, IRotateSetter
	{
		
		public IRotateItemSetter(IPrimitive Value) : base(Value)
		{
		}

		public override IRotate Set(IRotate Component)
		{
			Component.Item = Value;
			return Component;
		}
	}
}
