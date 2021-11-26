
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
	public class SwitchItemsSetter : Setter<Switch, IEnumerable<IWhen>>, ISwitchSetter
	{
		
		public SwitchItemsSetter(IEnumerable<IWhen> Value) : base(Value)
		{
		}

		public override Switch Set(Switch Component)
		{
			foreach (IWhen child in Value)
			{
				Component.Add(child);
			}
			return Component;
		}
	}
}
