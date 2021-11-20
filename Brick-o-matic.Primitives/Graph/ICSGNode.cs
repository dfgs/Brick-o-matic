using Brick_o_matic.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.Primitives
{
	public interface ICSGNode
	{
		

		Box BoundingBox
		{
			get;
		}

		int BrickCount
		{
			get;
		}
		int NodeCount
		{
			get;
		}


		string Name
		{
			get;
		}

		bool SplitTag
		{
			get;
		}


		IEnumerable<ICSGNode> Nodes
		{
			get;
		}
		IEnumerable<Brick> Bricks
		{
			get;
		}

		void Build(IResourceProvider ResourceProvider, IPrimitive Primitive);
		void Build(Box BoundingBox,IEnumerable<Brick> Bricks);

		void Add(Brick Brick);
		void Add(ICSGNode Node);

		IEnumerable<ICSGNode> GetIntersections(Box OtherBox);
		bool Split(Box OtherBox);


		

	}
}
