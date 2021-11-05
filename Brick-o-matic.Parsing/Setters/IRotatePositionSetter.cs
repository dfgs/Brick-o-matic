
using Brick_o_matic.Math;
using Brick_o_matic.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.Parsing.Setters
{
	public class IRotatePositionSetter : Setter<IRotate, Position>, IRotateSetter
	{
		
		public IRotatePositionSetter(Position Value) : base(Value)
		{
		}

		public override IRotate Set(IRotate Component)
		{
			Component.Position = Value;
			return Component;
		}
	}
}
