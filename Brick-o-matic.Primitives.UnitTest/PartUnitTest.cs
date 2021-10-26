using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Brick_o_matic.Primitives;
using Brick_o_matic.Math;

namespace Part_o_matic.Primitives.UnitTest
{
	[TestClass]
	public class PartUnitTest
	{
		[TestMethod]
		public void ShouldInstanciate()
		{
			Part part;

			part = new Part();
			Assert.IsNotNull(part) ;
			Assert.AreEqual(new Vector(), part.Position);

			part = new Part(new Vector(1, 2, 3));
			Assert.IsNotNull(part);
			Assert.AreEqual(new Vector(1,2,3), part.Position);
		}

		[TestMethod]
		public void ShouldReturnBoundingBoxWhenPartIsEmpty()
		{
			Part part;
			Box box;

			part = new Part();
			Assert.IsNotNull(part);
			box = part.GetBoudingBox();
			Assert.AreEqual(0, box.X1);
			Assert.AreEqual(0, box.Y1);
			Assert.AreEqual(0, box.Z1);
			Assert.AreEqual(new Vector(1, 1, 1), box.Size);
		}

		[TestMethod]
		public void ShouldReturnBoundingBox()
		{
			Part part;
			Box box;
			Brick b;

			part = new Part(new Vector(0, 0, 0));
			Assert.IsNotNull(part);
			b = new Brick(new Vector(-1, -2, -3), new Vector(1, 1, 1));
			part.Items.Add(b);
			box = part.GetBoudingBox();
			Assert.AreEqual(-1, box.X1);
			Assert.AreEqual(-2, box.Y1);
			Assert.AreEqual(-3, box.Z1);
			Assert.AreEqual(new Vector(1, 1, 1), box.Size);

			part = new Part(new Vector(1, 2, 3));
			Assert.IsNotNull(part);
			b = new Brick(new Vector(-1, -2, -3), new Vector(1, 1, 1));
			part.Items.Add(b);
			box = part.GetBoudingBox();
			Assert.AreEqual(0, box.X1);
			Assert.AreEqual(0, box.Y1);
			Assert.AreEqual(0, box.Z1);
			Assert.AreEqual(new Vector(1, 1, 1), box.Size);

			part = new Part(new Vector(0, 0, 0));
			Assert.IsNotNull(part);
			b = new Brick(new Vector(-1, -2, -3), new Vector(1, 1, 1));
			part.Items.Add(b);
			b = new Brick(new Vector(1, 2, 3), new Vector(1, 1, 1));
			part.Items.Add(b);
			box = part.GetBoudingBox();
			Assert.AreEqual(-1, box.X1);
			Assert.AreEqual(-2, box.Y1);
			Assert.AreEqual(-3, box.Z1);
			Assert.AreEqual(new Vector(3, 5, 7), box.Size);

			part = new Part(new Vector(0, 0, 0));
			Assert.IsNotNull(part);
			b = new Brick(new Vector(-1, -2, -3), new Vector(2, 2, 2));
			part.Items.Add(b);
			b = new Brick(new Vector(1, 2, 3), new Vector(2, 2, 2));
			part.Items.Add(b);
			box = part.GetBoudingBox();
			Assert.AreEqual(-1, box.X1);
			Assert.AreEqual(-2, box.Y1);
			Assert.AreEqual(-3, box.Z1);
			Assert.AreEqual(new Vector(4, 6, 8), box.Size);

		}





	}
}
