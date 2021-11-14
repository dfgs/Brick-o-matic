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

		

		public Box GetBoundingBox(IResourceProvider ResourceProvider)
		{
			IResourceProvider router;

			int minX = int.MaxValue, minY = int.MaxValue, minZ = int.MaxValue;
			int maxX = int.MinValue, maxY = int.MinValue, maxZ = int.MinValue;
			Box childBox;

			if (ItemsCount == 0) return new Box(new Position(), new Size());

			if ((ResourceProvider == null) || (ResourceProvider==this)) router = this;
			else router = new ResourceProviderRouter(ResourceProvider, this);

			foreach (IPrimitive item in this.items)
			{
				childBox = item.GetBoundingBox(router);
				minX = System.Math.Min(minX, childBox.Position.X);
				minY = System.Math.Min(minY, childBox.Position.Y);
				minZ = System.Math.Min(minZ, childBox.Position.Z);

				maxX = System.Math.Max(maxX, childBox.Position.X + childBox.Size.X);
				maxY = System.Math.Max(maxY, childBox.Position.Y + childBox.Size.Y);
				maxZ = System.Math.Max(maxZ, childBox.Position.Z + childBox.Size.Z);
			}

			return new Box(new Position(minX, minY, minZ), new Size(maxX - minX, maxY - minY, maxZ - minZ));

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

		public ICSGNode BuildCSGNode(IResourceProvider ResourceProvider)
		{
			IResourceProvider router;
			CSGNode node;
			int minX = int.MaxValue, minY = int.MaxValue, minZ = int.MaxValue;
			int maxX = int.MinValue, maxY = int.MinValue, maxZ = int.MinValue;
			ICSGNode childNode;


			if ((ResourceProvider == null) || (ResourceProvider == this)) router = this;
			else router = new ResourceProviderRouter(ResourceProvider, this);

			node = new CSGNode(); node.Name = "Scene";

			if (ItemsCount == 0) node.BoundingBox = new Box(new Position(), new Size());
			else
			{
				foreach (IPrimitive item in this.items)
				{
					childNode = item.BuildCSGNode(ResourceProvider);
					node.Add(childNode);

					minX = System.Math.Min(minX, childNode.BoundingBox.Position.X);
					minY = System.Math.Min(minY, childNode.BoundingBox.Position.Y);
					minZ = System.Math.Min(minZ, childNode.BoundingBox.Position.Z);

					maxX = System.Math.Max(maxX, childNode.BoundingBox.Position.X + childNode.BoundingBox.Size.X);
					maxY = System.Math.Max(maxY, childNode.BoundingBox.Position.Y + childNode.BoundingBox.Size.Y);
					maxZ = System.Math.Max(maxZ, childNode.BoundingBox.Position.Z + childNode.BoundingBox.Size.Z);
				}

				node.BoundingBox = new Box(new Position(minX, minY, minZ), new Size(maxX - minX, maxY - minY, maxZ - minZ));
			}


			return node;
		}


	}
}
