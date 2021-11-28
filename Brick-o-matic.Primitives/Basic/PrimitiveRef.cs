using Brick_o_matic.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Brick_o_matic.Primitives
{
	public class PrimitiveRef : Primitive,IPrimitiveRef
	{

		private Dictionary<string, Resource> resources;
		public IEnumerable<Resource> Resources
		{
			get
			{
				foreach (KeyValuePair<string, Resource> keyValuePair in resources)
				{
					yield return keyValuePair.Value;
				}
			}
		}
		
		public int ResourcesCount
		{
			get { return resources.Count; }
		}

		public string Name
		{
			get;
			set;
		}


		public PrimitiveRef()
		{
			resources = new Dictionary<string, Resource>();
		}
		public PrimitiveRef(Position Position):base(Position)
		{
			resources = new Dictionary<string, Resource>();
		}

		public void AddResource(string Name, ISceneObject Object)
		{
			if (Object == null) throw new ArgumentNullException(nameof(Object));
			if (Name == null) throw new ArgumentNullException(nameof(Name));
			if (resources.ContainsKey(Name)) throw new InvalidOperationException($"A resource with name {Name} already exists");
			resources.Add(Name, new Resource(Name,Object));
		}

		public bool TryGetResource<T>(string Name,  out T Object)
			where T : ISceneObject
		{
			Resource resource;

			if (Name == null) throw new ArgumentNullException(nameof(Name));

			Object = default(T);

			if (!resources.TryGetValue(Name, out resource)) return false;
			if (!(resource.Object is T)) return false;

			Object = (T)resource.Object;

			return true;

		}
		
		

		public override IBox GetBoundingBox(IResourceProvider ResourceProvider)
		{
			IPrimitive referencedPrimitive;
			IBox childBox;
			string localName;
			ResourceProviderRouter router;
			IResourceProvider alternativeResourceProvider;

			if (ResourceProvider == null) throw new ArgumentNullException(nameof(ResourceProvider));



			if (!ResourceProvider.TryGetResource(this.Name,  out referencedPrimitive))
			{
				throw new InvalidOperationException($"Reference to primitive {Name} was not found");
			}

			alternativeResourceProvider = NameSpaceUtils.GetResourceLocation(ResourceProvider, Name, out localName);
			if (alternativeResourceProvider == null)
			{
				router = new ResourceProviderRouter(this, ResourceProvider);
			}
			else
			{
				router = new ResourceProviderRouter(new ResourceProviderRouter(this, ResourceProvider), alternativeResourceProvider);
			}


			childBox = referencedPrimitive.GetBoundingBox(router);

			return new Box(Position + childBox.Position, childBox.Size);

		}

		public override IEnumerable<Brick> Build(IResourceProvider ResourceProvider)
		{
			IPrimitive referencedPrimitive;
			string localName;
			ResourceProviderRouter router;
			IResourceProvider alternativeResourceProvider;

			if (ResourceProvider == null) throw new ArgumentNullException(nameof(ResourceProvider));

			if (!ResourceProvider.TryGetResource(this.Name,  out referencedPrimitive))
			{
				throw new InvalidOperationException($"Reference to primitive {Name} was not found");
			}

			alternativeResourceProvider = NameSpaceUtils.GetResourceLocation(ResourceProvider, Name, out localName);
			if (alternativeResourceProvider == null)
			{
				router = new ResourceProviderRouter(this, ResourceProvider);
			}
			else
			{
				router = new ResourceProviderRouter(new ResourceProviderRouter(this, ResourceProvider), alternativeResourceProvider);
			}


			foreach (Brick brick in referencedPrimitive.Build(router))
			{
				yield return new Brick(this.Position + brick.Position, brick.Size,brick.Color);
			}



		}

		public override void Validate(IResourceProvider ResourceProvider, ILocker Locker)
		{
			IPrimitive referencedPrimitive;
			string localName;
			ResourceProviderRouter router;
			IResourceProvider alternativeResourceProvider;

			if (Locker == null) throw new ArgumentNullException(nameof(Locker));
			if (ResourceProvider == null) throw new ArgumentNullException(nameof(ResourceProvider));

			if (!ResourceProvider.TryGetResource(this.Name, out referencedPrimitive))
			{
				throw new InvalidOperationException($"Reference to primitive {Name} was not found");
			}

			alternativeResourceProvider = NameSpaceUtils.GetResourceLocation(ResourceProvider, Name, out localName);
			if (alternativeResourceProvider == null)
			{
				router = new ResourceProviderRouter(this, ResourceProvider);
			}
			else
			{
				router = new ResourceProviderRouter(new ResourceProviderRouter(this, ResourceProvider), alternativeResourceProvider);
			}

			Locker.Lock(Name);
			referencedPrimitive.Validate(router, Locker);
			Locker.Release(Name);

			
		}




	}
}
