using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Brick_o_matic.Math.UnitTest
{
	[TestClass]
	public class IBoxExtensionUnitTest
	{

		private static Box FindBox(IEnumerable<Box> Items, Position Position, Size Size)
		{
			return Items.FirstOrDefault(item => (item.Position == Position) && (item.Size == Size));
		}


		[TestMethod]
		public void ShouldNotIntersectIfOtherBoxIsFlat()
		{
			Box box1,box2;

			box1 = new Box(new Position(0, 0, 0),new Size( 1, 1, 1));
			box2 = new Box(new Position(0, 0, 0), new Size(0, 1, 1));
			Assert.IsFalse(box1.IntersectWith(box2));
			Assert.IsFalse(box2.IntersectWith(box1));
		}

		[TestMethod]
		public void ShouldNotIntersect()
		{
			Box box1,box2;

			box1 = new Box(new Position(0, 0, 0), new Size(1, 1, 1));
			box2 = new Box(new Position(1, 0, 0), new Size(1, 1, 1));
			Assert.IsFalse(box1.IntersectWith(box2));
			Assert.IsFalse(box2.IntersectWith(box1));
		}
		[TestMethod]
		public void ShouldIntersect()
		{
			Box box1, box2;

			box1 = new Box(new Position(0, 0, 0), new Size(5, 1, 1));
			box2 = new Box(new Position(4, 0, 0), new Size(5, 1, 1));
			Assert.IsTrue(box1.IntersectWith(box2));
			Assert.IsTrue(box2.IntersectWith(box1));

			box1 = new Box(new Position(0, 0, 0), new Size(1, 5, 1));
			box2 = new Box(new Position(0, 4, 0), new Size(1, 5, 1));
			Assert.IsTrue(box1.IntersectWith(box2));
			Assert.IsTrue(box2.IntersectWith(box1));

			box1 = new Box(new Position(0, 0, 0), new Size(1, 1, 5));
			box2 = new Box(new Position(0, 0, 4), new Size(1, 1, 5));
			Assert.IsTrue(box1.IntersectWith(box2));
			Assert.IsTrue(box2.IntersectWith(box1));
			
			box1 = new Box(new Position(0, 0, 0), new Size(5, 5, 5));
			box2 = new Box(new Position(4, 4, 4), new Size(5, 5, 5));
			Assert.IsTrue(box1.IntersectWith(box2));
			Assert.IsTrue(box2.IntersectWith(box1));


			box1 = new Box(new Position(0, 0, 0), new Size(5, 5, 5));
			box2 = new Box(new Position(2, 2, 2), new Size(1, 1, 1));
			Assert.IsTrue(box1.IntersectWith(box2));
			Assert.IsTrue(box2.IntersectWith(box1));

		}
		[TestMethod]
		public void ShouldNotContainsCoordinate()
		{
			Box box1;

			box1 = new Box(new Position(0, 0, 0), new Size(1, 1, 1));
			Assert.IsFalse(box1.Contains(new Position(-1, 0, 0)));
			Assert.IsFalse(box1.Contains(new Position(1, 0, 0)));
			Assert.IsFalse(box1.Contains(new Position(0, -1, 0)));
			Assert.IsFalse(box1.Contains(new Position(0, 1, 0)));
			Assert.IsFalse(box1.Contains(new Position(0, 0, -1)));
			Assert.IsFalse(box1.Contains(new Position(0, 0, 1)));
		}

		[TestMethod]
		public void ShouldConstainsCoordinate()
		{
			Box box1;

			box1 = new Box(new Position(0, 0, 0), new Size(5, 5, 5));
			Assert.IsTrue(box1.Contains(new Position(0, 0, 0)));
			Assert.IsTrue(box1.Contains(new Position(4, 4, 4)));
			Assert.IsTrue(box1.Contains(new Position(1, 2, 3)));
			Assert.IsTrue(box1.Contains(new Position(3, 2, 1)));
		}




		[TestMethod]
		public void ShouldSplitWhenBoxesAreFlat()
		{
			Box box1, box2;
			Box[] splits;

			box1 = new Box(new Position(0, 0, 0), new Size(0, 1, 1));
			box2 = new Box(new Position(3, 3, 3), new Size(2, 0, 2));

			splits = box1.SplitWith(box2).ToArray();
			Assert.AreEqual(0, splits.Length);

			splits = box2.SplitWith(box1).ToArray();
			Assert.AreEqual(0, splits.Length);
		}

		[TestMethod]
		public void ShouldSplitWhenOneBoxeIsFlat()
		{
			Box box1, box2;
			Box[] splits;

			box1 = new Box(new Position(0, 0, 0), new Size(1, 1, 1));
			box2 = new Box(new Position(3, 3, 3), new Size(2, 0, 2));

			splits = box1.SplitWith(box2).ToArray();
			Assert.AreEqual(1, splits.Length);

			splits = box2.SplitWith(box1).ToArray();
			Assert.AreEqual(0, splits.Length);
		}


		[TestMethod]
		public void ShouldSplitWhenBoxesDoesntIntersect()
		{
			Box box1, box2;
			Box[] splits;

			box1 = new Box(new Position(0, 0, 0), new Size(1, 1, 1));
			box2 = new Box(new Position(3, 3, 3), new Size(2, 2, 2));

			splits = box1.SplitWith(box2).ToArray();
			Assert.AreEqual(1, splits.Length);
			Assert.IsNotNull(FindBox(splits, new Position(0, 0, 0), new Size(1, 1, 1)));
			//Assert.IsNotNull(FindBox(splits, new Position(3, 3, 3), new Size(2, 2, 2)));

			splits = box2.SplitWith(box1).ToArray();
			Assert.AreEqual(1, splits.Length);
			//Assert.IsNotNull(FindBox(splits, new Position(0, 0, 0), new Size(1, 1, 1)));
			Assert.IsNotNull(FindBox(splits, new Position(3, 3, 3), new Size(2, 2, 2)));
		}

		[TestMethod]
		public void ShouldSplitWhenBoxesIntersect_StairShape()
		{
			Box box1, box2;
			Box[] splits;

			box1 = new Box(new Position(0, 0, 0), new Size(2, 2, 2));
			box2 = new Box(new Position(1, 1, 1), new Size(2, 2, 2));

			splits = box1.SplitWith(box2).ToArray();
			Assert.AreEqual(8, splits.Length);

			// parts of box 1
			Assert.IsNotNull(FindBox(splits, new Position(0, 0, 0), new Size(1, 1, 1)));
			Assert.IsNotNull(FindBox(splits, new Position(1, 0, 0), new Size(1, 1, 1)));
			Assert.IsNotNull(FindBox(splits, new Position(0, 1, 0), new Size(1, 1, 1)));
			Assert.IsNotNull(FindBox(splits, new Position(1, 1, 0), new Size(1, 1, 1)));

			Assert.IsNotNull(FindBox(splits, new Position(0, 0, 1), new Size(1, 1, 1)));
			Assert.IsNotNull(FindBox(splits, new Position(1, 0, 1), new Size(1, 1, 1)));
			Assert.IsNotNull(FindBox(splits, new Position(0, 1, 1), new Size(1, 1, 1)));
			Assert.IsNotNull(FindBox(splits, new Position(1, 1, 1), new Size(1, 1, 1)));    // intersection with box2

			// parts of box 2
			/*Assert.IsNotNull(FindBox(splits, new Position(1, 1, 1), new Size(1, 1, 1)));    // intersection with box1
			Assert.IsNotNull(FindBox(splits, new Position(2, 1, 1), new Size(1, 1, 1)));
			Assert.IsNotNull(FindBox(splits, new Position(1, 2, 1), new Size(1, 1, 1)));
			Assert.IsNotNull(FindBox(splits, new Position(2, 2, 1), new Size(1, 1, 1)));

			Assert.IsNotNull(FindBox(splits, new Position(1, 1, 2), new Size(1, 1, 1)));
			Assert.IsNotNull(FindBox(splits, new Position(2, 1, 2), new Size(1, 1, 1)));
			Assert.IsNotNull(FindBox(splits, new Position(1, 2, 2), new Size(1, 1, 1)));
			Assert.IsNotNull(FindBox(splits, new Position(2, 2, 2), new Size(1, 1, 1)));//*/
		}

		[TestMethod]
		public void ShouldSplitWhenBoxesIntersect_SameBoxes()
		{
			Box box1, box2;
			Box[] splits;

			box1 = new Box(new Position(0, 0, 0), new Size(2, 2, 2));
			box2 = new Box(new Position(0, 0, 0), new Size(2, 2, 2));

			splits = box1.SplitWith(box2).ToArray();
			Assert.AreEqual(1, splits.Length);
			Assert.IsNotNull(FindBox(splits, new Position(0, 0, 0), new Size(2, 2, 2)));

			splits = box2.SplitWith(box1).ToArray();
			Assert.AreEqual(1, splits.Length);
			Assert.IsNotNull(FindBox(splits, new Position(0, 0, 0), new Size(2, 2, 2)));
		}

		[TestMethod]
		public void ShouldSplitWhenBoxesIntersect_HorizontalShape()
		{
			Box box1, box2;
			Box[] splits;

			box1 = new Box(new Position(0, 0, 0), new Size(2, 2, 2));
			box2 = new Box(new Position(1, 0, 0), new Size(2, 2, 2));

			splits = box1.SplitWith(box2).ToArray();
			Assert.AreEqual(2, splits.Length);

			Assert.IsNotNull(FindBox(splits, new Position(0, 0, 0), new Size(1, 2, 2)));
			Assert.IsNotNull(FindBox(splits, new Position(1, 0, 0), new Size(1, 2, 2)));
			//Assert.IsNotNull(FindBox(splits, new Position(2, 0, 0), new Size(1, 2, 2)));

			splits = box2.SplitWith(box1).ToArray();
			Assert.AreEqual(2, splits.Length);

			//Assert.IsNotNull(FindBox(splits, new Position(0, 0, 0), new Size(1, 2, 2)));
			Assert.IsNotNull(FindBox(splits, new Position(1, 0, 0), new Size(1, 2, 2)));
			Assert.IsNotNull(FindBox(splits, new Position(2, 0, 0), new Size(1, 2, 2)));
		}


		[TestMethod]
		public void ShouldSplitWhenBoxesIntersect_Box2IntoBox1()
		{
			Box box1, box2;
			Box[] splits;

			box1 = new Box(new Position(0, 0, 0), new Size(3, 3, 3));
			box2 = new Box(new Position(1, 1, 1), new Size(1, 1, 1));

			splits = box1.SplitWith(box2).ToArray();
			Assert.AreEqual(27, splits.Length);
			for (int x = 0; x < 3; x++)
			{
				for (int y = 0; y < 3; y++)
				{
					for (int z = 0; z < 3; z++)
					{
						Assert.IsNotNull(FindBox(splits, new Position(x, y, z), new Size(1, 1, 1)));
					}
				}
			}

			splits = box2.SplitWith(box1).ToArray();
			Assert.AreEqual(1, splits.Length);
			Assert.IsNotNull(FindBox(splits, new Position(1, 1, 1), new Size(1, 1, 1)));
			

		}


		[TestMethod]
		public void ShouldSplitWhenBoxesIntersect_TShape()
		{
			Box box1, box2;
			Box[] splits;

			box1 = new Box(new Position(0, 0, 0), new Size(3, 2, 3));
			box2 = new Box(new Position(1, 1, 1), new Size(1, 2, 1));

			splits = box1.SplitWith(box2).ToArray();
			Assert.AreEqual(18, splits.Length);

			// parts of box 1
			Assert.IsNotNull(FindBox(splits, new Position(0, 0, 0), new Size(1, 1, 1)));
			Assert.IsNotNull(FindBox(splits, new Position(0, 0, 1), new Size(1, 1, 1)));
			Assert.IsNotNull(FindBox(splits, new Position(0, 0, 2), new Size(1, 1, 1)));

			Assert.IsNotNull(FindBox(splits, new Position(1, 0, 0), new Size(1, 1, 1)));
			Assert.IsNotNull(FindBox(splits, new Position(1, 0, 1), new Size(1, 1, 1)));
			Assert.IsNotNull(FindBox(splits, new Position(1, 0, 2), new Size(1, 1, 1)));

			Assert.IsNotNull(FindBox(splits, new Position(2, 0, 0), new Size(1, 1, 1)));
			Assert.IsNotNull(FindBox(splits, new Position(2, 0, 1), new Size(1, 1, 1)));
			Assert.IsNotNull(FindBox(splits, new Position(2, 0, 2), new Size(1, 1, 1)));

			Assert.IsNotNull(FindBox(splits, new Position(0, 1, 0), new Size(1, 1, 1)));
			Assert.IsNotNull(FindBox(splits, new Position(0, 1, 1), new Size(1, 1, 1)));
			Assert.IsNotNull(FindBox(splits, new Position(0, 1, 2), new Size(1, 1, 1)));

			Assert.IsNotNull(FindBox(splits, new Position(1, 1, 0), new Size(1, 1, 1)));
			Assert.IsNotNull(FindBox(splits, new Position(1, 1, 1), new Size(1, 1, 1))); // intersection with box2
			Assert.IsNotNull(FindBox(splits, new Position(1, 1, 2), new Size(1, 1, 1)));

			Assert.IsNotNull(FindBox(splits, new Position(2, 1, 0), new Size(1, 1, 1)));
			Assert.IsNotNull(FindBox(splits, new Position(2, 1, 1), new Size(1, 1, 1)));
			Assert.IsNotNull(FindBox(splits, new Position(2, 1, 2), new Size(1, 1, 1)));

			// parts of box 2
			//Assert.IsNotNull(FindBox(splits, new Position(1, 1, 1), new Size(1, 1, 1)));    // intersection with box1
			//Assert.IsNotNull(FindBox(splits, new Position(1, 2, 1), new Size(1, 1, 1)));
		}









	}
}
