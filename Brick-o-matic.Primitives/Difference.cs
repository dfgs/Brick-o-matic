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
			List<BoxGeometry> items;

			if ((A == null) || (B == null)) return new Model();

			modelA = A.Build();
			modelB = B.Build();

			items = new List<BoxGeometry>();
			foreach(BoxGeometry boxGeometry in modelA.Items)
			{
				if (!boxGeometry.IntersectWith(modelB.BoundingBox))
				{
					items.Add(boxGeometry);
					continue;
				}
				foreach(Box item in boxGeometry.SplitWith(modelB.BoundingBox))
				{
					if (item.IntersectWith(modelB.BoundingBox)) continue;
					items.Add(new BoxGeometry(item.Position,item.Size,boxGeometry.Color));
				}

			}

			return new Model(items.ToArray());
		}


	}
}
