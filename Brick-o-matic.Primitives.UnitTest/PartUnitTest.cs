using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Brick_o_matic.Primitives;
using Brick_o_matic.Math;
using System.Linq;

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
			Assert.IsNotNull(part);
			Assert.AreEqual(new Vector(), part.Position);

			part = new Part(new Vector(1, 2, 3));
			Assert.IsNotNull(part);
			Assert.AreEqual(new Vector(1, 2, 3), part.Position);
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
		[TestMethod]
		public void ShouldReturnNestedBoundingBox()
		{
			Part part;
			Part p1, p2;
			Brick b1, b2;
			Box box;

			part = new Part();

			p1 = new Part();
			b1 = new Brick(new Vector(-1,-1,-1));
			p1.Items.Add(b1);

			p2 = new Part();
			b2 = new Brick(new Vector(1, 1, 1));
			p2.Items.Add(b2);

			part.Items.Add(b1);
			part.Items.Add(b2);

			box = part.GetBoudingBox();
			Assert.AreEqual(-1, box.X1);
			Assert.AreEqual(-1, box.Y1);
			Assert.AreEqual(-1, box.Z1);
			Assert.AreEqual(new Vector(3, 3, 3), box.Size);
		}


		[TestMethod]
		public void ShouldGetEmptyBricks()
		{
			Part part;
			Brick[] items;

			part = new Part();
			items = part.GetBricks().ToArray();
			Assert.AreEqual(0, items.Length);
		}

		[TestMethod]
		public void ShouldGetBricks()
		{
			Part part;
			Brick b;
			Brick[] items;

			part = new Part();
			b = new Brick();
			part.Items.Add(b);

			items = part.GetBricks().ToArray();
			Assert.AreEqual(1, items.Length);
			Assert.AreEqual(b, items[0]);
		}

		[TestMethod]
		public void ShouldGetNestedBricks()
		{
			Part part;
			Part p1,p2;
			Brick b1,b2;
			Brick[] items;

			part = new Part();

			p1 = new Part();
			b1 = new Brick();
			p1.Items.Add(b1);
			
			p2 = new Part();
			b2 = new Brick();
			p2.Items.Add(b2);

			part.Items.Add(b1);
			part.Items.Add(b2);

			items = part.GetBricks().ToArray();
			Assert.AreEqual(2, items.Length);
			Assert.AreEqual(b1, items[0]);
			Assert.AreEqual(b2, items[1]);
		}
	}
}
