using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Brick_o_matic.Math.UnitTest
{
	[TestClass]
	public class IBoxExtensionUnitTest
	{
		


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






		







	}
}
