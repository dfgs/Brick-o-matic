using Brick_o_matic.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.Primitives
{
	public class Difference : CSG
	{
		public Difference()
		{
		}

		public override Model Build()
		{
			Model modelA, modelB;
			List<BoxGeometry> candidates;
			List<BoxGeometry> results;

			if ((A == null) || (B == null)) return new Model();

			modelA = A.Build();
			modelB = B.Build();

			results = new List<BoxGeometry>();
			results.AddRange(modelA.Items);

			foreach(BoxGeometry boxGeometryB in modelB.Items)
			{
				candidates = results;results = new List<BoxGeometry>();
				foreach (BoxGeometry candidate in candidates )
				{
					if (!candidate.IntersectWith(boxGeometryB)) continue;
					foreach (BoxGeometry split in candidate.SplitWith(boxGeometryB))
					{
						if (split.IntersectWith(boxGeometryB)) 
							continue;
						results.Add(split);
					}
				}

			}

			return new Model(results.ToArray());
		}


	}
}
