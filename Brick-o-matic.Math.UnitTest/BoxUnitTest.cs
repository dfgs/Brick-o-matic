using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Brick_o_matic.Math.UnitTest
{
	[TestClass]
	public class BoxUnitTest
	{
		[TestMethod]
		public void ShouldInstanciate()
		{
			Assert.IsNotNull( new Box(0, 0, 0, 1, 1, 1));
			Assert.IsNotNull( new Box(-1, -1, -1, 1, 1, 1));
			Assert.IsNotNull(new Box(0, 0, 0, 1));
			Assert.IsNotNull(new Box(0, 0, 0));
		}

		[TestMethod]
		public void ShouldNotInstanciateIfSizeInvalid()
		{
			Assert.ThrowsException<ArgumentException>(() => new Box(0, 0, 0, 0));
			Assert.ThrowsException<ArgumentException>(() => new Box(0, 0, 0, -1));
		}
		[TestMethod]
		public void ShouldNotInstanciateIfSizeXInvalid()
		{
			Assert.ThrowsException<ArgumentException>(() => new Box(0, 0, 0, 0, 1, 1));
			Assert.ThrowsException<ArgumentException>(() => new Box(0, 0, 0, -1, 1, 1));
		}

		[TestMethod]
		public void ShouldNotInstanciateIfSizeYInvalid()
		{
			Assert.ThrowsException<ArgumentException>(() => new Box(0, 0, 0, 1, 0, 1));
			Assert.ThrowsException<ArgumentException>(() => new Box(0, 0, 0, 1, -1, 1));
		}


		[TestMethod]
		public void ShouldNotInstanciateIfSizeZInvalid()
		{
			Assert.ThrowsException<ArgumentException>(() => new Box(0, 0, 0, 1, 1, 0));
			Assert.ThrowsException<ArgumentException>(() => new Box(0, 0, 0, 1, 1, -1));
		}

		[TestMethod]
		public void ShouldNotSetInvalidSizeX()
		{
			Box box;

			box = new Box(0, 0, 0, 1, 1, 1);
			Assert.IsNotNull(box);

			Assert.ThrowsException<ArgumentException>(() => { box.SizeX = 0; });
			Assert.ThrowsException<ArgumentException>(() => { box.SizeX = -1; });
		}

		[TestMethod]
		public void ShouldNotSetInvalidSizeY()
		{
			Box box;

			box = new Box(0, 0, 0, 1, 1, 1);
			Assert.IsNotNull(box);

			Assert.ThrowsException<ArgumentException>(() => { box.SizeY = 0; });
			Assert.ThrowsException<ArgumentException>(() => { box.SizeY = -1; });
		}

		[TestMethod]
		public void ShouldNotSetInvalidSizeZ()
		{
			Box box;

			box = new Box(0, 0, 0, 1, 1, 1);
			Assert.IsNotNull(box);

			Assert.ThrowsException<ArgumentException>(() => { box.SizeZ = 0; });
			Assert.ThrowsException<ArgumentException>(() => { box.SizeZ = -1; });
		}

		[TestMethod]
		public void ShouldReturnValidX2()
		{
			Box box;

			box = new Box(0, 0, 0, 1, 1, 1);
			Assert.IsNotNull(box);
			Assert.AreEqual(0, box.X2);

			box = new Box(0, 0, 0, 2, 1, 1);
			Assert.IsNotNull(box);
			Assert.AreEqual(1, box.X2);

			box = new Box(0, 0, 0, 3, 1, 1);
			Assert.IsNotNull(box);
			Assert.AreEqual(2, box.X2);
		}

		[TestMethod]
		public void ShouldReturnValidY2()
		{
			Box box;

			box = new Box(0, 0, 0, 1, 1, 1);
			Assert.IsNotNull(box);
			Assert.AreEqual(0, box.Y2);

			box = new Box(0, 0, 0, 1, 2, 1);
			Assert.IsNotNull(box);
			Assert.AreEqual(1, box.Y2);

			box = new Box(0, 0, 0, 1, 3, 1);
			Assert.IsNotNull(box);
			Assert.AreEqual(2, box.Y2);
		}

		[TestMethod]
		public void ShouldReturnValidZ2()
		{
			Box box;

			box = new Box(0, 0, 0, 1, 1, 1);
			Assert.IsNotNull(box);
			Assert.AreEqual(0, box.Z2);

			box = new Box(0, 0, 0, 1, 1, 2);
			Assert.IsNotNull(box);
			Assert.AreEqual(1, box.Z2);

			box = new Box(0, 0, 0, 1, 1, 3);
			Assert.IsNotNull(box);
			Assert.AreEqual(2, box.Z2);
		}


		[TestMethod]
		public void ShouldNotIntersectIfOtherBoxIsNull()
		{
			Box box1;

			box1 = new Box(0, 0, 0, 1, 1, 1);
			
			Assert.ThrowsException<ArgumentNullException>(()=>box1.IntersectWith(null));
		}
		[TestMethod]
		public void ShouldNotIntersect()
		{
			Box box1,box2;

			box1 = new Box(0, 0, 0, 1, 1, 1);
			box2 = new Box(1, 0, 0, 1, 1, 1);
			Assert.IsFalse(box1.IntersectWith(box2));
			Assert.IsFalse(box2.IntersectWith(box1));
		}
		[TestMethod]
		public void ShouldIntersect()
		{
			Box box1, box2;

			box1 = new Box(0, 0, 0, 5, 1, 1);
			box2 = new Box(4, 0, 0, 5, 1, 1);
			Assert.IsTrue(box1.IntersectWith(box2));
			Assert.IsTrue(box2.IntersectWith(box1));

			box1 = new Box(0, 0, 0, 1, 5, 1);
			box2 = new Box(0, 4, 0, 1, 5, 1);
			Assert.IsTrue(box1.IntersectWith(box2));
			Assert.IsTrue(box2.IntersectWith(box1));

			box1 = new Box(0, 0, 0, 1, 1, 5);
			box2 = new Box(0, 0, 4, 1, 1, 5);
			Assert.IsTrue(box1.IntersectWith(box2));
			Assert.IsTrue(box2.IntersectWith(box1));
			
			box1 = new Box(0, 0, 0, 5, 5, 5);
			box2 = new Box(4, 4, 4, 5, 5, 5);
			Assert.IsTrue(box1.IntersectWith(box2));
			Assert.IsTrue(box2.IntersectWith(box1));


			box1 = new Box(0, 0, 0, 5, 5, 5);
			box2 = new Box(2, 2, 2, 1, 1, 1);
			Assert.IsTrue(box1.IntersectWith(box2));
			Assert.IsTrue(box2.IntersectWith(box1));

		}
		[TestMethod]
		public void ShouldNotIntersectWithCoordinate()
		{
			Box box1;

			box1 = new Box(0, 0, 0, 1, 1, 1);
			Assert.IsFalse(box1.IsInside(new Vector(-1, 0, 0)));
			Assert.IsFalse(box1.IsInside(new Vector(1, 0, 0)));
			Assert.IsFalse(box1.IsInside(new Vector(0, -1, 0)));
			Assert.IsFalse(box1.IsInside(new Vector(0, 1, 0)));
			Assert.IsFalse(box1.IsInside(new Vector(0, 0, -1)));
			Assert.IsFalse(box1.IsInside(new Vector(0, 0, 1)));
		}

		[TestMethod]
		public void ShouldIntersectWithCoordinate()
		{
			Box box1;

			box1 = new Box(0, 0, 0, 5, 5, 5);
			Assert.IsTrue(box1.IsInside(new Vector(0, 0, 0)));
			Assert.IsTrue(box1.IsInside(new Vector(4, 4, 4)));
			Assert.IsTrue(box1.IsInside(new Vector(1, 2, 3)));
			Assert.IsTrue(box1.IsInside(new Vector(3, 2, 1)));
		}






		[TestMethod]
		public void ShouldNotSplitIfOtherBoxIsNull()
		{
			Box box1;

			box1 = new Box(0, 0, 0, 1, 1, 1);

			Assert.ThrowsException<ArgumentNullException>(() => box1.SplitWith(null).ToArray());
		}
		
		private static Box FindBox(IEnumerable<Box> Items,int X,int Y,int Z,int SizeX,int SizeY,int SizeZ)
		{
			return Items.FirstOrDefault(item => (item.X1 == X) && (item.Y1 == Y) && (item.Z1 == Z) && (item.SizeX == SizeX) && (item.SizeY == SizeY) && (item.SizeZ == SizeZ));
		}

		[TestMethod]
		public void ShouldSplitWhenBoxesDoesntIntersect()
		{
			Box box1, box2;
			Box[] splits;

			box1 = new Box(0, 0, 0, 1, 1, 1);
			box2 = new Box(3, 3, 3, 2, 2, 2);

			splits = box1.SplitWith(box2).ToArray();
			Assert.AreEqual(2, splits.Length);
			Assert.IsNotNull(FindBox(splits, 0, 0, 0, 1, 1, 1));
			Assert.IsNotNull(FindBox(splits, 3, 3, 3, 2, 2, 2));

			splits = box2.SplitWith(box1).ToArray();
			Assert.AreEqual(2, splits.Length);
			Assert.IsNotNull(FindBox(splits, 0, 0, 0, 1, 1, 1));
			Assert.IsNotNull(FindBox(splits, 3, 3, 3, 2, 2, 2));
		}

		[TestMethod]
		public void ShouldSplitWhenBoxesIntersect_StairShape()
		{
			Box box1, box2;
			Box[] splits;

			box1 = new Box(0, 0, 0, 2, 2, 2);
			box2 = new Box(1, 1, 1, 2, 2, 2);

			splits = box1.SplitWith(box2).ToArray();
			Assert.AreEqual(15, splits.Length);
			
			// parts of box 1
			Assert.IsNotNull(FindBox(splits, 0, 0, 0, 1, 1, 1));
			Assert.IsNotNull(FindBox(splits, 1, 0, 0, 1, 1, 1));
			Assert.IsNotNull(FindBox(splits, 0, 1, 0, 1, 1, 1));
			Assert.IsNotNull(FindBox(splits, 1, 1, 0, 1, 1, 1));
			
			Assert.IsNotNull(FindBox(splits, 0, 0, 1, 1, 1, 1));
			Assert.IsNotNull(FindBox(splits, 1, 0, 1, 1, 1, 1));
			Assert.IsNotNull(FindBox(splits, 0, 1, 1, 1, 1, 1));
			Assert.IsNotNull(FindBox(splits, 1, 1, 1, 1, 1, 1));	// intersection with box2

			// parts of box 2
			Assert.IsNotNull(FindBox(splits, 1, 1, 1, 1, 1, 1));    // intersection with box1
			Assert.IsNotNull(FindBox(splits, 2, 1, 1, 1, 1, 1));
			Assert.IsNotNull(FindBox(splits, 1, 2, 1, 1, 1, 1));
			Assert.IsNotNull(FindBox(splits, 2, 2, 1, 1, 1, 1));

			Assert.IsNotNull(FindBox(splits, 1, 1, 2, 1, 1, 1));
			Assert.IsNotNull(FindBox(splits, 2, 1, 2, 1, 1, 1));
			Assert.IsNotNull(FindBox(splits, 1, 2, 2, 1, 1, 1));
			Assert.IsNotNull(FindBox(splits, 2, 2, 2, 1, 1, 1));
		}

		[TestMethod]
		public void ShouldSplitWhenBoxesIntersect_SameBoxes()
		{
			Box box1, box2;
			Box[] splits;

			box1 = new Box(0, 0, 0, 2, 2, 2);
			box2 = new Box(0, 0, 0, 2, 2, 2);

			splits = box1.SplitWith(box2).ToArray();
			Assert.AreEqual(1, splits.Length);
			Assert.IsNotNull(FindBox(splits, 0, 0, 0, 2, 2, 2));

			splits = box2.SplitWith(box1).ToArray();
			Assert.AreEqual(1, splits.Length);
			Assert.IsNotNull(FindBox(splits, 0, 0, 0, 2, 2, 2));
		}

		[TestMethod]
		public void ShouldSplitWhenBoxesIntersect_HorizontalShape()
		{
			Box box1, box2;
			Box[] splits;

			box1 = new Box(0, 0, 0, 2, 2, 2);
			box2 = new Box(1, 0, 0, 2, 2, 2);

			splits = box1.SplitWith(box2).ToArray();
			Assert.AreEqual(3, splits.Length);

			Assert.IsNotNull(FindBox(splits, 0, 0, 0, 1, 2, 2));
			Assert.IsNotNull(FindBox(splits, 1, 0, 0, 1, 2, 2));
			Assert.IsNotNull(FindBox(splits, 2, 0, 0, 1, 2, 2));

			splits = box2.SplitWith(box1).ToArray();
			Assert.AreEqual(3, splits.Length);

			Assert.IsNotNull(FindBox(splits, 0, 0, 0, 1, 2, 2));
			Assert.IsNotNull(FindBox(splits, 1, 0, 0, 1, 2, 2));
			Assert.IsNotNull(FindBox(splits, 2, 0, 0, 1, 2, 2));
		}


		[TestMethod]
		public void ShouldSplitWhenBoxesIntersect_Box2IntoBox1()
		{
			Box box1, box2;
			Box[] splits;

			box1 = new Box(0, 0, 0, 3, 3, 3);
			box2 = new Box(1, 1, 1, 1, 1, 1);

			splits = box1.SplitWith(box2).ToArray();
			Assert.AreEqual(27, splits.Length);
			for(int x=0;x<3; x++)
			{
				for (int y = 0; y < 3; y++)
				{
					for (int z = 0; z < 3; z++)
					{
						Assert.IsNotNull(FindBox(splits, x, y, z, 1, 1, 1));
					}
				}
			}

			splits = box2.SplitWith(box1).ToArray();
			Assert.AreEqual(27, splits.Length);
			for (int x = 0; x < 3; x++)
			{
				for (int y = 0; y < 3; y++)
				{
					for (int z = 0; z < 3; z++)
					{
						Assert.IsNotNull(FindBox(splits, x, y, z, 1, 1, 1));
					}
				}
			}

		}


		[TestMethod]
		public void ShouldSplitWhenBoxesIntersect_TShape()
		{
			Box box1, box2;
			Box[] splits;

			box1 = new Box(0, 0, 0, 3, 2, 3);
			box2 = new Box(1, 1, 1, 1, 2, 1);

			splits = box1.SplitWith(box2).ToArray();
			Assert.AreEqual(19, splits.Length);

			// parts of box 1
			Assert.IsNotNull(FindBox(splits, 0, 0, 0, 1, 1, 1));
			Assert.IsNotNull(FindBox(splits, 0, 0, 1, 1, 1, 1));
			Assert.IsNotNull(FindBox(splits, 0, 0, 2, 1, 1, 1));

			Assert.IsNotNull(FindBox(splits, 1, 0, 0, 1, 1, 1));
			Assert.IsNotNull(FindBox(splits, 1, 0, 1, 1, 1, 1));
			Assert.IsNotNull(FindBox(splits, 1, 0, 2, 1, 1, 1));

			Assert.IsNotNull(FindBox(splits, 2, 0, 0, 1, 1, 1));
			Assert.IsNotNull(FindBox(splits, 2, 0, 1, 1, 1, 1));
			Assert.IsNotNull(FindBox(splits, 2, 0, 2, 1, 1, 1));

			Assert.IsNotNull(FindBox(splits, 0, 1, 0, 1, 1, 1));
			Assert.IsNotNull(FindBox(splits, 0, 1, 1, 1, 1, 1));
			Assert.IsNotNull(FindBox(splits, 0, 1, 2, 1, 1, 1));

			Assert.IsNotNull(FindBox(splits, 1, 1, 0, 1, 1, 1));
			Assert.IsNotNull(FindBox(splits, 1, 1, 1, 1, 1, 1)); // intersection with box2
			Assert.IsNotNull(FindBox(splits, 1, 1, 2, 1, 1, 1));

			Assert.IsNotNull(FindBox(splits, 2, 1, 0, 1, 1, 1));
			Assert.IsNotNull(FindBox(splits, 2, 1, 1, 1, 1, 1));
			Assert.IsNotNull(FindBox(splits, 2, 1, 2, 1, 1, 1));

			// parts of box 2
			Assert.IsNotNull(FindBox(splits, 1, 1, 1, 1, 1, 1));	// intersection with box1
			Assert.IsNotNull(FindBox(splits, 1, 2, 1, 1, 1, 1));
		}






	}
}
