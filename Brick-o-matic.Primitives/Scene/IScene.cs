using Brick_o_matic.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.Primitives
{
	public interface IScene
	{
		IEnumerable<IPrimitive> Items
		{
			get;
		}

		int ItemsCount
		{
			get;
		}
		int ResourcesCount
		{
			get;
		}
		Box GetBoundingBox();

		IEnumerable<Brick> Build();

		void Add(IPrimitive Child);

		void AddResource(string Name,ISceneObject Object);
		ISceneObject GetResource(string Name);

		IEnumerable<(string Name,ISceneObject Object)> GetResources();
	}
}
