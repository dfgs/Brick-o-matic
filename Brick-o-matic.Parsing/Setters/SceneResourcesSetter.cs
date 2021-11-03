
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
	public class SceneResourcesSetter : Setter<Scene, IEnumerable<Resource>>, ISceneSetter
	{
		
		public SceneResourcesSetter(IEnumerable<Resource> Value) : base(Value)
		{
		}

		public override Scene Set(Scene Component)
		{
			foreach (Resource resource in Value)
			{
				Component.AddResource(resource.Name, resource.Object);
			}
			return Component;
		}
	}
}
