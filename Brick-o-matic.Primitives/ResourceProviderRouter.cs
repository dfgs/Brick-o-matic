using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.Primitives
{
	public class ResourceProviderRouter : IResourceProvider
	{
		private IResourceProvider primary;
		private IResourceProvider secondary;

		public int ResourcesCount => primary.ResourcesCount+secondary.ResourcesCount;

		public ResourceProviderRouter(IResourceProvider Primary, IResourceProvider Secondary)
		{
			if (Primary == null) throw new ArgumentNullException(nameof(Primary));
			if (Secondary == null) throw new ArgumentNullException(nameof(Secondary));

			this.primary = Primary;this.secondary = Secondary;
		}


		public void AddResource(string Name, ISceneObject Object)
		{
			throw new InvalidOperationException("Cannot add resource") ;
		}

		public IEnumerable<Resource> GetResources()
		{
			return primary.GetResources().Union(secondary.GetResources());
		}

		public bool TryGetResource<T>(string Name, out T Object) where T : ISceneObject
		{
			if (primary.TryGetResource(Name, out Object)) return true;
			return secondary.TryGetResource(Name, out Object);
		}
	}
}
