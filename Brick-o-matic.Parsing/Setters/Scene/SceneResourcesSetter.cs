
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
				if (resource.Object is ImportedResources importedResources)
				{
					if (importedResources.Scene == null) return Component;
					foreach ((string Name, ISceneObject Object) item in importedResources.Scene.GetResources())
					{
						Component.AddResource($"{resource.Name}.{item.Name}", item.Object);
					}
				}
				else
				{
					Component.AddResource(resource.Name, resource.Object);
				}
				
			}
			return Component;
		}
	}
}
