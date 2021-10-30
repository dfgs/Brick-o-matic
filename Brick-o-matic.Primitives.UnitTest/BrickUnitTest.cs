using Brick_o_matic.Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Brick_o_matic.Primitives.UnitTest
{
	[TestClass]
	public class BrickUnitTest
	{
		[TestMethod]
		public void ShouldInstanciate()
		{
			Brick b;

			b = new Brick();
			Assert.IsNotNull(b) ;
			Assert.AreEqual(new Vector(), b.Position);
			Assert.AreEqual(new Vector(1,1,1), b.Size);

			b = new Brick(new Vector(1, 2, 3));
			Assert.IsNotNull(b);
			Assert.AreEqual(new Vector(1,2,3), b.Position);
			Assert.AreEqual(new Vector(1, 1, 1), b.Size);

			b = new Brick(new Vector(1, 2, 3), new Vector(5, 5, 5));
			Assert.IsNotNull(b);
			Assert.AreEqual(new Vector(1,2,3), b.Position);
			Assert.AreEqual(new Vector(5, 5, 5), b.Size);
		}
		[TestMethod]
		public void ShouldNotInstanciate()
		{
			Assert.ThrowsException<ArgumentException>(() => new Brick(new Vector(), new Vector()));
			Assert.ThrowsException<ArgumentException>(() => new Brick(new Vector(), new Vector(0, 1, 1)));
			Assert.ThrowsException<ArgumentException>(() => new Brick(new Vector(), new Vector(1, 0, 1)));
			Assert.ThrowsException<ArgumentException>(() => new Brick(new Vector(), new Vector(1, 1, 0)));
			Assert.ThrowsException<ArgumentException>(() => new Brick(new Vector(), new Vector(-1, 1, 1)));
			Assert.ThrowsException<ArgumentException>(() => new Brick(new Vector(), new Vector(1, -1, 1)));
			Assert.ThrowsException<ArgumentException>(() => new Brick(new Vector(), new Vector(1, 1, -1)));
		}

		[TestMethod]
		public void ShouldNotSetInvalidSize()
		{
			Brick b;

			b = new Brick();
			Assert.ThrowsException<ArgumentException>(() => b.Size = new Vector());
			Assert.ThrowsException<ArgumentException>(() => b.Size = new Vector(0, 1, 1));
			Assert.ThrowsException<ArgumentException>(() => b.Size = new Vector(1, 0, 1));
			Assert.ThrowsException<ArgumentException>(() => b.Size = new Vector(1, 1, 0));
			Assert.ThrowsException<ArgumentException>(() => b.Size = new Vector(-1, 1, 1));
			Assert.ThrowsException<ArgumentException>(() => b.Size = new Vector(1, -1, 1));
			Assert.ThrowsException<ArgumentException>(() => b.Size = new Vector(1, 1, -1));
		}

		[TestMethod]
		public void ShouldReturnBoundingBox()
		{
			Brick b;
			Box box;

			b = new Brick();
			Assert.IsNotNull(b);
			box = b.GetBoudingBox();
			Assert.AreEqual(0, box.X1);
			Assert.AreEqual(0, box.Y1);
			Assert.AreEqual(0, box.Z1);
			Assert.AreEqual(new Vector(1, 1, 1), box.Size);

			b = new Brick(new Vector(1, 2, 3));
			Assert.IsNotNull(b);
			box = b.GetBoudingBox();
			Assert.AreEqual(1, box.X1);
			Assert.AreEqual(2, box.Y1);
			Assert.AreEqual(3, box.Z1);
			Assert.AreEqual(new Vector(1, 1, 1), box.Size);

			b = new Brick(new Vector(1, 2, 3), new Vector(5, 5, 5));
			Assert.IsNotNull(b);
			box = b.GetBoudingBox();
			Assert.AreEqual(1, box.X1);
			Assert.AreEqual(2, box.Y1);
			Assert.AreEqual(3, box.Z1);
			Assert.AreEqual(new Vector(5, 5, 5), box.Size);
		}
		[TestMethod]
		public void ShouldGetBricks()
		{
			Brick b;
			Brick[] items;

			b = new Brick();
			items = b.GetBricks().ToArray();
			Assert.AreEqual(1, items.Length);
			Assert.AreEqual(b, items[0]);
		}




		}
	}
