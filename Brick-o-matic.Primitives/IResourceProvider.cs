using Brick_o_matic.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.Primitives
{
	public interface IResourceProvider
	{
		int ResourcesCount
		{
			get;
		}
		IEnumerable<Resource> Resources
		{
			get;
		}
		bool TryGetResource<T>(string Name,out T Object)
			where T:ISceneObject;

		void AddResource(string Name, ISceneObject Object);

		void LockResource(string Name);
		void ReleaseResource(string Name);
	}
}
