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

		private Dictionary<string, ISceneObject> resources;


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
			resources = new Dictionary<string, ISceneObject>();
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
			resources.Add(Name, Object);
		}

		public bool TryGetResource<T>(string Name,  out T Object)
			where T : ISceneObject
		{
			ISceneObject sceneObject;
			IResourceProvider resourceProviderLocation;
			string localName;

			if (Name == null) throw new ArgumentNullException(nameof(Name));

			Object= default(T);

			resourceProviderLocation = NameSpaceUtils.GetResourceLocation(this, Name,out localName);
			if (resourceProviderLocation != null) return resourceProviderLocation.TryGetResource(localName, out Object);
			
			if (!resources.TryGetValue(Name, out sceneObject)) return false;
			if (!(sceneObject is T)) return false;

			Object = (T)sceneObject;
			
			return true;
		}

		public IEnumerable<(string Name, ISceneObject Object)> GetResources()
		{
			foreach(KeyValuePair<string,ISceneObject> keyValuePair in resources)
			{
				yield return (keyValuePair.Key, keyValuePair.Value);
			}
		}

		public Box GetBoundingBox()
		{
			int minX = int.MaxValue, minY = int.MaxValue, minZ = int.MaxValue;
			int maxX = int.MinValue, maxY = int.MinValue, maxZ = int.MinValue;
			Box childBox;

			if (ItemsCount == 0) return new Box(new Position(), new Size());

			foreach (IPrimitive item in this.items)
			{
				childBox = item.GetBoundingBox(this);
				minX = System.Math.Min(minX, childBox.Position.X);
				minY = System.Math.Min(minY, childBox.Position.Y);
				minZ = System.Math.Min(minZ, childBox.Position.Z);

				maxX = System.Math.Max(maxX, childBox.Position.X + childBox.Size.X);
				maxY = System.Math.Max(maxY, childBox.Position.Y + childBox.Size.Y);
				maxZ = System.Math.Max(maxZ, childBox.Position.Z + childBox.Size.Z);
			}

			return new Box(new Position(minX, minY, minZ), new Size(maxX - minX, maxY - minY, maxZ - minZ));

		}

		public IEnumerable<Brick> Build()
		{
			foreach (IPrimitive item in this.items)
			{
				foreach(Brick brick in item.Build(this))
				{
					yield return brick;
				}
			}
		}



	}
}
