﻿using Brick_o_matic.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Brick_o_matic.Primitives
{
	public class PrimitiveRef : Primitive,IPrimitiveRef
	{
		//private IResourceProvider alternativeResourceProvider;

		private Dictionary<string, ISceneObject> resources;
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
			resources = new Dictionary<string, ISceneObject>();
		}
		public PrimitiveRef(Position Position):base(Position)
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

		public bool TryGetResource<T>(string Name,  out T Object)
			where T : ISceneObject
		{
		
			ISceneObject sceneObject;

			if (Name == null) throw new ArgumentNullException(nameof(Name));

			Object = default(T);

			if (!resources.TryGetValue(Name, out sceneObject)) return false;
			if (!(sceneObject is T)) return false;

			Object = (T)sceneObject;

			return true;

		}

		public IEnumerable<(string Name, ISceneObject Object)> GetResources()
		{
			foreach (KeyValuePair<string, ISceneObject> keyValuePair in resources)
			{
				yield return (keyValuePair.Key, keyValuePair.Value);
			}
		}
		
		

		public override Box GetBoundingBox(IResourceProvider ResourceProvider)
		{
			IPrimitive referencedPrimitive;
			Box childBox;
			string localName;
			ResourceProviderRouter router;
			IResourceProvider alternativeResourceProvider;

			if (ResourceProvider == null) throw new ArgumentNullException(nameof(ResourceProvider));


			if (!ResourceProvider.TryGetResource(this.Name,  out referencedPrimitive))
			{
				throw new InvalidOperationException($"Reference to primitive {Name} was not found");
			}

			alternativeResourceProvider = NameSpaceUtils.GetResourceLocation(ResourceProvider, Name, out localName);
			if (alternativeResourceProvider == null) alternativeResourceProvider = ResourceProvider;

			router = new ResourceProviderRouter(new ResourceProviderRouter(this,ResourceProvider), alternativeResourceProvider);
			
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
			if (alternativeResourceProvider == null) alternativeResourceProvider = ResourceProvider;
			
			router = new ResourceProviderRouter(new ResourceProviderRouter(this, ResourceProvider), alternativeResourceProvider);

			foreach (Brick brick in referencedPrimitive.Build(router))
			{
				yield return new Brick(this.Position + brick.Position, brick.Size,brick.Color);
			}


		}


	}
}
