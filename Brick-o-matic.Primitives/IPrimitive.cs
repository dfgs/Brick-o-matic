using Brick_o_matic.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.Primitives
{
	public interface IPrimitive: ISceneObject
	{
		Position Position
		{
			get;
			set;
		}

		IBox GetBoundingBox(IResourceProvider ResourceProvider);
		IEnumerable<Brick> Build(IResourceProvider ResourceProvider);

		//void Validate(IResourceProvider ResourceProvider,ILocker Locker);
	}
}
