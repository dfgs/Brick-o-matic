using Brick_o_matic.Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brick_o_matic.Primitives.UnitTest
{
	
	[TestClass]
	public class BoxGeometryGeometryUnitTest
	{
		private static BoxGeometry FindBoxGeometry(IEnumerable<BoxGeometry> Items, Position Position, Size Size)
		{
			return Items.First(item =>
				(item.NegativeX.Position == Position.X) && (item.PositiveX.Position == Position.X + Size.X)
				&& (item.NegativeY.Position == Position.Y) && (item.PositiveY.Position == Position.Y + Size.Y)
				&& (item.NegativeZ.Position == Position.Z) && (item.PositiveZ.Position == Position.Z + Size.Z)
				);
		}
		private static bool FindBoxGeometry(IEnumerable<BoxGeometry> Items, Position Position, Size Size, Color NegativeX, Color PositiveX, Color NegativeY, Color PositiveY, Color NegativeZ, Color PositiveZ)
		{
			try
			{
				BoxGeometry g = Items.First(item =>
					  (item.NegativeX.Position == Position.X) && (item.PositiveX.Position == Position.X + Size.X)
					  && (item.NegativeY.Position == Position.Y) && (item.PositiveY.Position == Position.Y + Size.Y)
					  && (item.NegativeZ.Position == Position.Z) && (item.PositiveZ.Position == Position.Z + Size.Z)
					  && (item.NegativeX.Color == NegativeX) && (item.PositiveX.Color == PositiveX)
					  && (item.NegativeY.Color == NegativeY) && (item.PositiveY.Color == PositiveY)
					  && (item.NegativeZ.Color == NegativeZ) && (item.PositiveZ.Color == PositiveZ)
					);
				return true;
			}
			catch
			{
				return false;
			}
		}

		[TestMethod]
		public void ShouldNotIntersectIfOtherBoxIsFlat()
		{
			BoxGeometry box1, box2;

			box1 = new BoxGeometry(new Position(0, 0, 0), new Size(1, 1, 1),new Color());
			box2 = new BoxGeometry(new Position(0, 0, 0), new Size(0, 1, 1), new Color());
			Assert.IsFalse(box1.IntersectWith(box2));
			Assert.IsFalse(box2.IntersectWith(box1));
		}

		[TestMethod]
		public void ShouldNotIntersect()
		{
			BoxGeometry box1, box2;

			box1 = new BoxGeometry(new Position(0, 0, 0), new Size(1, 1, 1), new Color());
			box2 = new BoxGeometry(new Position(1, 0, 0), new Size(1, 1, 1), new Color());
			Assert.IsFalse(box1.IntersectWith(box2));
			Assert.IsFalse(box2.IntersectWith(box1));
		}
		[TestMethod]
		public void ShouldIntersect()
		{
			BoxGeometry box1, box2;

			box1 = new BoxGeometry(new Position(0, 0, 0), new Size(5, 1, 1), new Color());
			box2 = new BoxGeometry(new Position(4, 0, 0), new Size(5, 1, 1), new Color());
			Assert.IsTrue(box1.IntersectWith(box2));
			Assert.IsTrue(box2.IntersectWith(box1));

			box1 = new BoxGeometry(new Position(0, 0, 0), new Size(1, 5, 1), new Color());
			box2 = new BoxGeometry(new Position(0, 4, 0), new Size(1, 5, 1), new Color());
			Assert.IsTrue(box1.IntersectWith(box2));
			Assert.IsTrue(box2.IntersectWith(box1));

			box1 = new BoxGeometry(new Position(0, 0, 0), new Size(1, 1, 5), new Color());
			box2 = new BoxGeometry(new Position(0, 0, 4), new Size(1, 1, 5), new Color());
			Assert.IsTrue(box1.IntersectWith(box2));
			Assert.IsTrue(box2.IntersectWith(box1));

			box1 = new BoxGeometry(new Position(0, 0, 0), new Size(5, 5, 5), new Color());
			box2 = new BoxGeometry(new Position(4, 4, 4), new Size(5, 5, 5), new Color());
			Assert.IsTrue(box1.IntersectWith(box2));
			Assert.IsTrue(box2.IntersectWith(box1));


			box1 = new BoxGeometry(new Position(0, 0, 0), new Size(5, 5, 5), new Color());
			box2 = new BoxGeometry(new Position(2, 2, 2), new Size(1, 1, 1), new Color());
			Assert.IsTrue(box1.IntersectWith(box2));
			Assert.IsTrue(box2.IntersectWith(box1));

		}

		#region split with plane X

		[TestMethod]
		public void ShouldNotSplitWithNegativeXPlane()
		{
			BoxGeometry box;
			BoxGeometry partA,partB;
			Color blue, red;
			bool result;

			blue = new Color(0, 0, 255);
			red = new Color(255, 0, 0);
			box = new BoxGeometry(new Position(0, 0, 0), new Size(3, 1, 1), red);
			
			result = box.SplitWithPlane(new Plane(0, PlaneDirections.NegativeX, blue),out partA,out partB);
			Assert.IsFalse(result);
			result = box.SplitWithPlane(new Plane(3, PlaneDirections.NegativeX, blue), out partA, out partB);
			Assert.IsFalse(result);
		}

		[TestMethod]
		public void ShouldSplitWithNegativeXPlane()
		{
			BoxGeometry box;
			BoxGeometry partA, partB;
			Color blue, red;
			bool result;

			blue = new Color(0, 0, 255);
			red = new Color(255, 0, 0);
			box = new BoxGeometry(new Position(0, 0, 0), new Size(3, 1, 1), red);

			result = box.SplitWithPlane(new Plane(2, PlaneDirections.NegativeX, blue), out partA, out partB);
			Assert.IsTrue(result);
	
			
			Assert.AreEqual(red, partA.NegativeX.Color);
			Assert.AreEqual(blue, partA.PositiveX.Color);
			Assert.AreEqual(red, partA.NegativeY.Color);
			Assert.AreEqual(red, partA.PositiveY.Color);
			Assert.AreEqual(red, partA.NegativeZ.Color);
			Assert.AreEqual(red, partA.PositiveZ.Color);

		
			Assert.AreEqual(red, partB.NegativeX.Color);
			Assert.AreEqual(red, partB.PositiveX.Color);
			Assert.AreEqual(red, partB.NegativeY.Color);
			Assert.AreEqual(red, partB.PositiveY.Color);
			Assert.AreEqual(red, partB.NegativeZ.Color);
			Assert.AreEqual(red, partB.PositiveZ.Color);
		}

		[TestMethod]
		public void ShouldNotSplitWithPositiveXPlane()
		{
			BoxGeometry box;
			BoxGeometry partA, partB;
			Color blue, red;
			bool result;

			blue = new Color(0, 0, 255);
			red = new Color(255, 0, 0);
			box = new BoxGeometry(new Position(0, 0, 0), new Size(3, 1, 1), red);

			result = box.SplitWithPlane(new Plane(0, PlaneDirections.PositiveX, blue), out partA, out partB);
			Assert.IsFalse(result);
			result = box.SplitWithPlane(new Plane(3, PlaneDirections.PositiveX, blue), out partA, out partB);
			Assert.IsFalse(result);
		}

		[TestMethod]
		public void ShouldSplitWithPositiveXPlane()
		{
			BoxGeometry box;
			BoxGeometry partA, partB;
			Color blue, red;
			bool result;

			blue = new Color(0, 0, 255);
			red = new Color(255, 0, 0);
			box = new BoxGeometry(new Position(0, 0, 0), new Size(3, 1, 1), red);

			result = box.SplitWithPlane(new Plane(2, PlaneDirections.PositiveX, blue), out partA, out partB);
			Assert.IsTrue(result);

			
			Assert.AreEqual(red, partA.NegativeX.Color);
			Assert.AreEqual(red, partA.PositiveX.Color);
			Assert.AreEqual(red, partA.NegativeY.Color);
			Assert.AreEqual(red, partA.PositiveY.Color);
			Assert.AreEqual(red, partA.NegativeZ.Color);
			Assert.AreEqual(red, partA.PositiveZ.Color);

			
			Assert.AreEqual(blue, partB.NegativeX.Color);
			Assert.AreEqual(red, partB.PositiveX.Color);
			Assert.AreEqual(red, partB.NegativeY.Color);
			Assert.AreEqual(red, partB.PositiveY.Color);
			Assert.AreEqual(red, partB.NegativeZ.Color);
			Assert.AreEqual(red, partB.PositiveZ.Color);
		}
		#endregion

		#region split with plane Y

		[TestMethod]
		public void ShouldNotSplitWithNegativeYPlane()
		{
			BoxGeometry box;
			BoxGeometry partA, partB;
			Color blue, red;
			bool result;

			blue = new Color(0, 0, 255);
			red = new Color(255, 0, 0);
			box = new BoxGeometry(new Position(0, 0, 0), new Size(1, 3, 1), red);

			result = box.SplitWithPlane(new Plane(0, PlaneDirections.NegativeY, blue), out partA, out partB);
			Assert.IsFalse(result);
			result = box.SplitWithPlane(new Plane(3, PlaneDirections.NegativeY, blue), out partA, out partB);
			Assert.IsFalse(result);
		}

		[TestMethod]
		public void ShouldSplitWithNegativeYPlane()
		{
			BoxGeometry box;
			BoxGeometry partA, partB;
			Color blue, red;
			bool result;

			blue = new Color(0, 0, 255);
			red = new Color(255, 0, 0);
			box = new BoxGeometry(new Position(0, 0, 0), new Size(1, 3, 1), red);

			result = box.SplitWithPlane(new Plane(2, PlaneDirections.NegativeY, blue), out partA, out partB);
			Assert.IsTrue(result);

			Assert.AreEqual(red, partA.NegativeX.Color);
			Assert.AreEqual(red, partA.PositiveX.Color);
			Assert.AreEqual(red, partA.NegativeY.Color);
			Assert.AreEqual(blue, partA.PositiveY.Color);
			Assert.AreEqual(red, partA.NegativeZ.Color);
			Assert.AreEqual(red, partA.PositiveZ.Color);

			Assert.AreEqual(red, partB.NegativeX.Color);
			Assert.AreEqual(red, partB.PositiveX.Color);
			Assert.AreEqual(red, partB.NegativeY.Color);
			Assert.AreEqual(red, partB.PositiveY.Color);
			Assert.AreEqual(red, partB.NegativeZ.Color);
			Assert.AreEqual(red, partB.PositiveZ.Color);
		}

		[TestMethod]
		public void ShouldNotSplitWithPositiveYPlane()
		{
			BoxGeometry box;
			BoxGeometry partA, partB;
			Color blue, red;
			bool result;

			blue = new Color(0, 0, 255);
			red = new Color(255, 0, 0);
			box = new BoxGeometry(new Position(0, 0, 0), new Size(1, 3, 1), red);

			result = box.SplitWithPlane(new Plane(0, PlaneDirections.PositiveY, blue), out partA, out partB);
			Assert.IsFalse(result);
			result = box.SplitWithPlane(new Plane(3, PlaneDirections.PositiveY, blue), out partA, out partB);
			Assert.IsFalse(result);
		}

		[TestMethod]
		public void ShouldSplitWithPositiveYPlane()
		{
			BoxGeometry box;
			BoxGeometry partA, partB;
			Color blue, red;
			bool result;

			blue = new Color(0, 0, 255);
			red = new Color(255, 0, 0);
			box = new BoxGeometry(new Position(0, 0, 0), new Size(1, 3, 1), red);

			result = box.SplitWithPlane(new Plane(2, PlaneDirections.PositiveY, blue), out partA, out partB);
			Assert.IsTrue(result);

			Assert.AreEqual(red, partA.NegativeX.Color);
			Assert.AreEqual(red, partA.PositiveX.Color);
			Assert.AreEqual(red, partA.NegativeY.Color);
			Assert.AreEqual(red, partA.PositiveY.Color);
			Assert.AreEqual(red, partA.NegativeZ.Color);
			Assert.AreEqual(red, partA.PositiveZ.Color);

			Assert.AreEqual(red, partB.NegativeX.Color);
			Assert.AreEqual(red, partB.PositiveX.Color);
			Assert.AreEqual(blue, partB.NegativeY.Color);
			Assert.AreEqual(red, partB.PositiveY.Color);
			Assert.AreEqual(red, partB.NegativeZ.Color);
			Assert.AreEqual(red, partB.PositiveZ.Color);
		}
		#endregion

		#region split with plane Z

		[TestMethod]
		public void ShouldNotSplitWithNegativeZPlane()
		{
			BoxGeometry box;
			BoxGeometry partA, partB;
			Color blue, red;
			bool result;

			blue = new Color(0, 0, 255);
			red = new Color(255, 0, 0);
			box = new BoxGeometry(new Position(0, 0, 0), new Size(1, 1, 3), red);

			result = box.SplitWithPlane(new Plane(0, PlaneDirections.NegativeZ, blue), out partA, out partB);
			Assert.IsFalse(result);
			result = box.SplitWithPlane(new Plane(3, PlaneDirections.NegativeZ, blue), out partA, out partB);
			Assert.IsFalse(result);
		}

		[TestMethod]
		public void ShouldSplitWithNegativeZPlane()
		{
			BoxGeometry box;
			BoxGeometry partA, partB;
			Color blue, red;
			bool result;

			blue = new Color(0, 0, 255);
			red = new Color(255, 0, 0);
			box = new BoxGeometry(new Position(0, 0, 0), new Size(1, 1, 3), red);

			result = box.SplitWithPlane(new Plane(2, PlaneDirections.NegativeZ, blue), out partA, out partB);
			Assert.IsTrue(result);

			Assert.AreEqual(red, partA.NegativeX.Color);
			Assert.AreEqual(red, partA.PositiveX.Color);
			Assert.AreEqual(red, partA.NegativeY.Color);
			Assert.AreEqual(red, partA.PositiveY.Color);
			Assert.AreEqual(red, partA.NegativeZ.Color);
			Assert.AreEqual(blue, partA.PositiveZ.Color);

			Assert.AreEqual(red, partB.NegativeX.Color);
			Assert.AreEqual(red, partB.PositiveX.Color);
			Assert.AreEqual(red, partB.NegativeY.Color);
			Assert.AreEqual(red, partB.PositiveY.Color);
			Assert.AreEqual(red, partB.NegativeZ.Color);
			Assert.AreEqual(red, partB.PositiveZ.Color);
		}

		[TestMethod]
		public void ShouldNotSplitWithPositiveZPlane()
		{
			BoxGeometry box;
			BoxGeometry partA, partB;
			Color blue, red;
			bool result;

			blue = new Color(0, 0, 255);
			red = new Color(255, 0, 0);
			box = new BoxGeometry(new Position(0, 0, 0), new Size(1, 1, 3), red);

			result = box.SplitWithPlane(new Plane(0, PlaneDirections.PositiveZ, blue), out partA, out partB);
			Assert.IsFalse(result);
			result = box.SplitWithPlane(new Plane(3, PlaneDirections.PositiveZ, blue), out partA, out partB);
			Assert.IsFalse(result);
		}

		[TestMethod]
		public void ShouldSplitWithPositiveZPlane()
		{
			BoxGeometry box;
			BoxGeometry partA, partB;
			Color blue, red;
			bool result;

			blue = new Color(0, 0, 255);
			red = new Color(255, 0, 0);
			box = new BoxGeometry(new Position(0, 0, 0), new Size(1, 1, 3), red);

			result = box.SplitWithPlane(new Plane(2, PlaneDirections.PositiveZ, blue), out partA, out partB);
			Assert.IsTrue(result);

			
			Assert.AreEqual(red, partA.NegativeX.Color);
			Assert.AreEqual(red, partA.PositiveX.Color);
			Assert.AreEqual(red, partA.NegativeY.Color);
			Assert.AreEqual(red, partA.PositiveY.Color);
			Assert.AreEqual(red, partA.NegativeZ.Color);
			Assert.AreEqual(red, partA.PositiveZ.Color);

			
			Assert.AreEqual(red, partB.NegativeX.Color);
			Assert.AreEqual(red, partB.PositiveX.Color);
			Assert.AreEqual(red, partB.NegativeY.Color);
			Assert.AreEqual(red, partB.PositiveY.Color);
			Assert.AreEqual(blue, partB.NegativeZ.Color);
			Assert.AreEqual(red, partB.PositiveZ.Color);
		}
		#endregion
		[TestMethod]
		public void ShouldReturnIsFlat()
		{
			BoxGeometry box;
			Color blue, red;

			blue = new Color(0, 0, 255);
			red = new Color(255, 0, 0);
			box = new BoxGeometry(new Position(0, 0, 0), new Size(0, 1, 1), red);
			Assert.IsTrue(box.IsFlat);
			box = new BoxGeometry(new Position(0, 0, 0), new Size(1, 0, 1), red);
			Assert.IsTrue(box.IsFlat);
			box = new BoxGeometry(new Position(0, 0, 0), new Size(1, 1, 0), red);
			Assert.IsTrue(box.IsFlat);

			box = new BoxGeometry(new Plane(0, PlaneDirections.NegativeX, blue), new Plane(-1, PlaneDirections.NegativeX, blue),
				new Plane(0, PlaneDirections.NegativeY, blue), new Plane(1, PlaneDirections.NegativeY, blue),
				new Plane(0, PlaneDirections.NegativeZ, blue), new Plane(1, PlaneDirections.NegativeZ, blue)
				);
			Assert.IsTrue(box.IsFlat);
			box = new BoxGeometry(new Plane(0, PlaneDirections.NegativeX, blue), new Plane(1, PlaneDirections.NegativeX, blue),
				new Plane(0, PlaneDirections.NegativeY, blue), new Plane(-1, PlaneDirections.NegativeY, blue),
				new Plane(0, PlaneDirections.NegativeZ, blue), new Plane(1, PlaneDirections.NegativeZ, blue)
				);
			Assert.IsTrue(box.IsFlat);
			box = new BoxGeometry(new Plane(0, PlaneDirections.NegativeX, blue), new Plane(1, PlaneDirections.NegativeX, blue),
				new Plane(0, PlaneDirections.NegativeY, blue), new Plane(1, PlaneDirections.NegativeY, blue),
				new Plane(0, PlaneDirections.NegativeZ, blue), new Plane(-1, PlaneDirections.NegativeZ, blue)
				);
			Assert.IsTrue(box.IsFlat);


			box = new BoxGeometry(new Position(0, 0, 0), new Size(1, 1, 1), red);
			Assert.IsFalse(box.IsFlat);
		}

		[TestMethod]
		public void ShouldNotSplitWhenBoxGeometriesAreFlat()
		{
			BoxGeometry box1, box2;
			BoxGeometry[] splits;
			Color blue, red;

			blue = new Color(0, 0, 255);
			red = new Color(255, 0, 0);
			box1 = new BoxGeometry(new Position(0, 0, 0), new Size(0, 1, 1),red);
			box2 = new BoxGeometry(new Position(3, 3, 3), new Size(2, 0, 2), blue);

			splits = box1.SplitWith(box2).ToArray();
			Assert.AreEqual(2, splits.Length);
			Assert.AreEqual(box1, splits[0]);
			Assert.AreEqual(box2, splits[1]);

			splits = box2.SplitWith(box1).ToArray();
			Assert.AreEqual(2, splits.Length);
			Assert.AreEqual(box2, splits[0]);
			Assert.AreEqual(box1, splits[1]);
		}

		[TestMethod]
		public void ShouldNotSplitWhenOneBoxGeometriIsFlat()
		{
			BoxGeometry box1, box2;
			BoxGeometry[] splits;
			Color blue, red;

			blue = new Color(0, 0, 255);
			red = new Color(255, 0, 0);
			box1 = new BoxGeometry(new Position(0, 0, 0), new Size(1, 1, 1), red);
			box2 = new BoxGeometry(new Position(3, 3, 3), new Size(2, 0, 2), blue);

			splits = box1.SplitWith(box2).ToArray();
			Assert.AreEqual(2, splits.Length);
			Assert.AreEqual(box1, splits[0]);
			Assert.AreEqual(box2, splits[1]);

			splits = box2.SplitWith(box1).ToArray();
			Assert.AreEqual(2, splits.Length);
			Assert.AreEqual(box2, splits[0]);
			Assert.AreEqual(box1, splits[1]);
		}

		
		[TestMethod]
		public void ShouldNotSplitWhenBoxGeometriesDontIntersect()
		{
			BoxGeometry box1, box2;
			BoxGeometry[] splits;
			Color blue, red;

			blue = new Color(0, 0, 255);
			red = new Color(255, 0, 0);
			box1 = new BoxGeometry(new Position(0, 0, 0), new Size(1, 1, 1), red);
			box2 = new BoxGeometry(new Position(3, 3, 3), new Size(2, 2, 2), blue);

			splits = box1.SplitWith(box2).ToArray();
			Assert.AreEqual(2, splits.Length);
			Assert.AreEqual(box1, splits[0]);
			Assert.AreEqual(box2, splits[1]);

			splits = box2.SplitWith(box1).ToArray();
			Assert.AreEqual(2, splits.Length);
			Assert.AreEqual(box2, splits[0]);
			Assert.AreEqual(box1, splits[1]);
		}

		
		[TestMethod]
		public void ShouldSplitWhenBoxGeometriesIntersect_AlignedBarShapeX()
		{
			BoxGeometry box1, box2;
			BoxGeometry[] splits;
			Color blue, red;

			blue = new Color(0, 0, 255);
			red = new Color(255, 0, 0);
			box1 = new BoxGeometry(new Position(0, 0, 0), new Size(2, 1, 1), red);
			box2 = new BoxGeometry(new Position(1, 0, 0), new Size(2, 1, 1), blue);

			splits = box1.SplitWith(box2).ToArray();
			Assert.AreEqual(4, splits.Length);

			// parts of box 1
			Assert.IsTrue(FindBoxGeometry(splits, new Position(0, 0, 0), new Size(1, 1, 1) , red,blue,red,red,red,red ));

			// intersections 
			Assert.IsTrue( FindBoxGeometry(splits, new Position(1, 0, 0), new Size(1, 1, 1), red, red, red, red, red, red));
			Assert.IsTrue( FindBoxGeometry(splits, new Position(1, 0, 0), new Size(1, 1, 1), blue, blue, blue, blue, blue, blue));

			// parts of box 2
			Assert.IsTrue(FindBoxGeometry(splits, new Position(2, 0, 0), new Size(1, 1, 1), red, blue, blue, blue, blue, blue));
		}
		[TestMethod]
		public void ShouldSplitWhenBoxGeometriesIntersect_AlignedBarShapeY()
		{
			BoxGeometry box1, box2;
			BoxGeometry[] splits;
			Color blue, red;

			blue = new Color(0, 0, 255);
			red = new Color(255, 0, 0);
			box1 = new BoxGeometry(new Position(0, 0, 0), new Size(1, 2, 1), red);
			box2 = new BoxGeometry(new Position(0, 1, 0), new Size(1, 2, 1), blue);

			splits = box1.SplitWith(box2).ToArray();
			Assert.AreEqual(4, splits.Length);

			// parts of box 1
			Assert.IsTrue(FindBoxGeometry(splits, new Position(0, 0, 0), new Size(1, 1, 1), red, red, red, blue, red, red));

			// intersections 
			Assert.IsTrue(FindBoxGeometry(splits, new Position(0, 1, 0), new Size(1, 1, 1), red, red, red, red, red, red));
			Assert.IsTrue(FindBoxGeometry(splits, new Position(0, 1, 0), new Size(1, 1, 1), blue, blue, blue, blue, blue, blue));

			// parts of box 2
			Assert.IsTrue(FindBoxGeometry(splits, new Position(0, 2, 0), new Size(1, 1, 1), blue, blue, red, blue, blue, blue));
		}

		[TestMethod]
		public void ShouldSplitWhenBoxGeometriesIntersect_AlignedBarShapeZ()
		{
			BoxGeometry box1, box2;
			BoxGeometry[] splits;
			Color blue, red;

			blue = new Color(0, 0, 255);
			red = new Color(255, 0, 0);
			box1 = new BoxGeometry(new Position(0, 0, 0), new Size(1, 1, 2), red);
			box2 = new BoxGeometry(new Position(0, 0, 1), new Size(1, 1, 2), blue);

			splits = box1.SplitWith(box2).ToArray();
			Assert.AreEqual(4, splits.Length);

			// parts of box 1
			Assert.IsTrue(FindBoxGeometry(splits, new Position(0, 0, 0), new Size(1, 1, 1), red, red, red, red, red, blue));

			// intersections 
			Assert.IsTrue(FindBoxGeometry(splits, new Position(0, 0, 1), new Size(1, 1, 1), red, red, red, red, red, red));
			Assert.IsTrue(FindBoxGeometry(splits, new Position(0, 0, 1), new Size(1, 1, 1), blue, blue, blue, blue, blue, blue));

			// parts of box 2
			Assert.IsTrue(FindBoxGeometry(splits, new Position(0, 0, 2), new Size(1, 1, 1), blue, blue, blue, blue, red, blue));
		}

		[TestMethod]
		public void ShouldSplitWhenBoxGeometriesIntersect_StairShape()
		{
			BoxGeometry box1, box2;
			BoxGeometry[] splits;
			Color blue, red;

			blue = new Color(0, 0, 255);
			red = new Color(255, 0, 0);
			box1 = new BoxGeometry(new Position(0, 0, 0), new Size(2, 2, 1), red);
			box2 = new BoxGeometry(new Position(1, 1, 0), new Size(2, 2, 1), blue);

			splits = box1.SplitWith(box2).ToArray();
			Assert.AreEqual(6, splits.Length);

			// parts of box 1
			Assert.IsTrue(FindBoxGeometry(splits, new Position(0, 0, 0), new Size(1, 2, 1), red, blue, red, red, red, red));

			// intersections 
			Assert.IsTrue(FindBoxGeometry(splits, new Position(1, 1, 0), new Size(1, 1, 1), red, red, red, red, red, red));
			Assert.IsTrue(FindBoxGeometry(splits, new Position(1, 1, 0), new Size(1, 1, 1), blue, blue, blue, blue, blue, blue));

			// parts of box 2
			Assert.IsTrue(FindBoxGeometry(splits, new Position(2, 1, 0), new Size(1, 2, 1), red, blue, blue, blue, blue, blue));

		}
		/*
		[TestMethod]
		public void ShouldSplitWhenBoxGeometriesIntersect_SameBoxGeometries()
		{
			BoxGeometry box1, box2;
			BoxGeometry split;
			BoxGeometry[] splits;
			Color blue, red;

			blue = new Color(0, 0, 255);
			red = new Color(255, 0, 0);
			box1 = new BoxGeometry(new Position(0, 0, 0), new Size(2, 2, 2), red);
			box2 = new BoxGeometry(new Position(0, 0, 0), new Size(2, 2, 2), blue);

			result = box1.SplitWith(box2).ToArray();
			Assert.IsFalse(result);
			split=FindBoxGeometry(splits, new Position(0, 0, 0), new Size(2, 2, 2));
			

			result = box2.SplitWith(box1).ToArray();
			Assert.IsFalse(result);
			split=FindBoxGeometry(splits, new Position(0, 0, 0), new Size(2, 2, 2));
			
		}

		[TestMethod]
		public void ShouldSplitWhenBoxGeometriesIntersect_HorizontalShape()
		{
			BoxGeometry box1, box2;
			BoxGeometry split;
			BoxGeometry[] splits;
			Color blue, red;

			blue = new Color(0, 0, 255);
			red = new Color(255, 0, 0);
			box1 = new BoxGeometry(new Position(0, 0, 0), new Size(2, 2, 2), red);
			box2 = new BoxGeometry(new Position(1, 0, 0), new Size(2, 2, 2), blue);

			result = box1.SplitWith(box2).ToArray();
			Assert.AreEqual(3, splits.Length);

			split = FindBoxGeometry(splits, new Position(0, 0, 0), new Size(1, 2, 2));
			
			split = FindBoxGeometry(splits, new Position(1, 0, 0), new Size(1, 2, 2));
			
			split = FindBoxGeometry(splits, new Position(2, 0, 0), new Size(1, 2, 2));
			

			result = box2.SplitWith(box1).ToArray();
			Assert.AreEqual(3, splits.Length);

			split = FindBoxGeometry(splits, new Position(0, 0, 0), new Size(1, 2, 2));
			
			split = FindBoxGeometry(splits, new Position(1, 0, 0), new Size(1, 2, 2));
			
			split = FindBoxGeometry(splits, new Position(2, 0, 0), new Size(1, 2, 2));
			
		}


		[TestMethod]
		public void ShouldSplitWhenBoxGeometriesIntersect_BoxGeometry2IntoBoxGeometry1()
		{
			BoxGeometry box1, box2;
			BoxGeometry[] splits;
			Color blue, red;

			blue = new Color(0, 0, 255);
			red = new Color(255, 0, 0);
			box1 = new BoxGeometry(new Position(0, 0, 0), new Size(3, 3, 3), red);
			box2 = new BoxGeometry(new Position(1, 1, 1), new Size(1, 1, 1), blue);

			result = box1.SplitWith(box2).ToArray();
			Assert.AreEqual(27, splits.Length);
			for (int x = 0; x < 3; x++)
			{
				for (int y = 0; y < 3; y++)
				{
					for (int z = 0; z < 3; z++)
					{
						Assert.IsNotNull(FindBoxGeometry(splits, new Position(x, y, z), new Size(1, 1, 1)));
					}
				}
			}

			result = box2.SplitWith(box1).ToArray();
			Assert.AreEqual(27, splits.Length);
			for (int x = 0; x < 3; x++)
			{
				for (int y = 0; y < 3; y++)
				{
					for (int z = 0; z < 3; z++)
					{
						Assert.IsNotNull(FindBoxGeometry(splits, new Position(x, y, z), new Size(1, 1, 1)));
					}
				}
			}

		}


		[TestMethod]
		public void ShouldSplitWhenBoxGeometriesIntersect_TShape()
		{
			BoxGeometry box1, box2;
			BoxGeometry split;
			BoxGeometry[] splits;
			Color blue, red;

			blue = new Color(0, 0, 255);
			red = new Color(255, 0, 0);
			box1 = new BoxGeometry(new Position(0, 0, 0), new Size(3, 2, 3), red);
			box2 = new BoxGeometry(new Position(1, 1, 1), new Size(1, 2, 1), blue);

			result = box1.SplitWith(box2).ToArray();
			Assert.AreEqual(19, splits.Length);

			// parts of box 1
			split = FindBoxGeometry(splits, new Position(0, 0, 0), new Size(1, 1, 1));
			
			split = FindBoxGeometry(splits, new Position(0, 0, 1), new Size(1, 1, 1));
			
			split = FindBoxGeometry(splits, new Position(0, 0, 2), new Size(1, 1, 1));
			

			split = FindBoxGeometry(splits, new Position(1, 0, 0), new Size(1, 1, 1));
			
			split = FindBoxGeometry(splits, new Position(1, 0, 1), new Size(1, 1, 1));
			
			split = FindBoxGeometry(splits, new Position(1, 0, 2), new Size(1, 1, 1));
			

			split = FindBoxGeometry(splits, new Position(2, 0, 0), new Size(1, 1, 1));
			
			split = FindBoxGeometry(splits, new Position(2, 0, 1), new Size(1, 1, 1));
			
			split = FindBoxGeometry(splits, new Position(2, 0, 2), new Size(1, 1, 1));
			

			split = FindBoxGeometry(splits, new Position(0, 1, 0), new Size(1, 1, 1));
			
			split = FindBoxGeometry(splits, new Position(0, 1, 1), new Size(1, 1, 1));
			
			split = FindBoxGeometry(splits, new Position(0, 1, 2), new Size(1, 1, 1));
			

			split = FindBoxGeometry(splits, new Position(1, 1, 0), new Size(1, 1, 1));
			
			split = FindBoxGeometry(splits, new Position(1, 1, 1), new Size(1, 1, 1)); // intersection with box2
			
			split = FindBoxGeometry(splits, new Position(1, 1, 2), new Size(1, 1, 1));
			

			split = FindBoxGeometry(splits, new Position(2, 1, 0), new Size(1, 1, 1));
			
			split = FindBoxGeometry(splits, new Position(2, 1, 1), new Size(1, 1, 1));
			
			split = FindBoxGeometry(splits, new Position(2, 1, 2), new Size(1, 1, 1));
			

			// parts of box 2
			split = FindBoxGeometry(splits, new Position(1, 1, 1), new Size(1, 1, 1));    // intersection with box1
			
			split = FindBoxGeometry(splits, new Position(1, 2, 1), new Size(1, 1, 1));
			
		}

		//*/
	}
}
