
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
	public class SceneItemsSetter : Setter<Scene, IEnumerable<IPrimitive>>, ISceneSetter
	{
		
		public SceneItemsSetter(IEnumerable<IPrimitive> Value) : base(Value)
		{
		}

		public override Scene Set(Scene Component)
		{
			foreach (IPrimitive child in Value)
			{
				Component.Add(child);
			}
			return Component;
		}
	}
}
