using Brick_o_matic.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Brick_o_matic.Primitives
{
	public class Scene : IScene
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

		public int ItemsCount
		{
			get { return items.Count; }
		}
		public int ResourcesCount
		{
			get { return resources.Count; }
		}

		private List<IPrimitive> items;
		public IEnumerable<IPrimitive> Items
		{
			get => items;
		}

		public Scene()
		{
			resources = new Dictionary<string, Resource>();
			items = new List<IPrimitive>();
		}

		public void Add(IPrimitive Child)
		{
			if (Child == null) throw new ArgumentNullException(nameof(Child));
			items.Add(Child);
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
			IResourceProvider resourceProviderLocation;
			string localName;

			if (Name == null) throw new ArgumentNullException(nameof(Name));

			Object= default(T);

			resourceProviderLocation = NameSpaceUtils.GetResourceLocation(this, Name,out localName);
			if (resourceProviderLocation != null) return resourceProviderLocation.TryGetResource(localName, out Object);
			
			if (!resources.TryGetValue(Name, out resource)) return false;
			if (!(resource.Object is T)) return false;

			Object = (T)resource.Object;
			
			return true;
		}

		

		public IBox GetBoundingBox(IResourceProvider ResourceProvider)
		{
			IResourceProvider router;
			Box childBox;


			if ((ResourceProvider == null) || (ResourceProvider==this)) router = this;
			else router = new ResourceProviderRouter(ResourceProvider, this);

			childBox = new Box(items.Select(item => item.GetBoundingBox(router)));

			return childBox;


		}

		public IEnumerable<Brick> Build(IResourceProvider ResourceProvider)
		{
			IResourceProvider router;

			if ((ResourceProvider == null) || (ResourceProvider == this)) router = this;
			else router = new ResourceProviderRouter(ResourceProvider, this);

			foreach (IPrimitive item in this.items)
			{
				foreach(Brick brick in item.Build(router))
				{
					yield return brick;
				}
			}
		}

		
	

		public void Validate()
		{
			ILocker locker;

			locker = new Locker();
			foreach (Resource resource in resources.Values)
			{
				resource.Object.Validate(this, locker);
			}
			foreach (IPrimitive primitive in items)
			{
				primitive.Validate(this,locker);
			}
		}

	}
}
