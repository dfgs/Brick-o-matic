
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
	public class SceneResourceSetter : Setter<Scene, Resource>, ISceneSetter
	{
		
		public SceneResourceSetter(Resource Value) : base(Value)
		{
		}

		public override Scene Set(Scene Component)
		{
			Component.AddResource(Value.Name,Value.Object);
			return Component;
		}
	}
}
