using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.Primitives
{
	public class Scene : IScene
	{
		private Dictionary<string, ISceneObject> resources;

		public Scene()
		{
			resources = new Dictionary<string, ISceneObject>();
		}

		public void AddResource(string Name, ISceneObject Object)
		{
			if (Object == null) throw new ArgumentNullException(nameof(Object));
			if (Name == null) throw new ArgumentNullException(nameof(Name));
			if (resources.ContainsKey(Name)) throw new InvalidOperationException($"A resource with name {Name} already exists");
			resources.Add(Name, Object);
		}

		public ISceneObject GetResource(string Name)
		{
			if (!resources.ContainsKey(Name)) throw new InvalidOperationException($"Resource name {Name} not found");
			return resources[Name];
		}

	}
}
