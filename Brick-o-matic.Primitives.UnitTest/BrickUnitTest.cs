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
			Assert.AreEqual(new Position(), b.Position);
			Assert.AreEqual(new Size(1,1,1), b.Size);

			b = new Brick(new Position(1, 2, 3));
			Assert.IsNotNull(b);
			Assert.AreEqual(new Position(1,2,3), b.Position);
			Assert.AreEqual(new Size(1, 1, 1), b.Size);

			b = new Brick(new Position(1, 2, 3), new Size(5, 5, 5));
			Assert.IsNotNull(b);
			Assert.AreEqual(new Position(1,2,3), b.Position);
			Assert.AreEqual(new Size(5, 5, 5), b.Size);
		}
		/*[TestMethod]
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
		}//*/

		[TestMethod]
		public void ShouldBuildModel()
		{
			Brick b;
			Model model;

			b = new Brick();
			Assert.IsNotNull(b);
			model = b.Build();
			Assert.AreEqual(0, model.BoundingBox.Position.X);
			Assert.AreEqual(0, model.BoundingBox.Position.Y);
			Assert.AreEqual(0, model.BoundingBox.Position.Z);
			Assert.AreEqual(new Size(1, 1, 1), model.BoundingBox.Size);

			b = new Brick(new Position(1, 2, 3));
			Assert.IsNotNull(b);
			model = b.Build();
			Assert.AreEqual(1, model.BoundingBox.Position.X);
			Assert.AreEqual(2, model.BoundingBox.Position.Y);
			Assert.AreEqual(3, model.BoundingBox.Position.Z);
			Assert.AreEqual(new Size(1, 1, 1), model.BoundingBox.Size);

			b = new Brick(new Position(1, 2, 3), new Size(5, 5, 5));
			Assert.IsNotNull(b);
			model = b.Build();
			Assert.AreEqual(1, model.BoundingBox.Position.X);
			Assert.AreEqual(2, model.BoundingBox.Position.Y);
			Assert.AreEqual(3, model.BoundingBox.Position.Z);
			Assert.AreEqual(new Size(5, 5, 5), model.BoundingBox.Size);
		}
		[TestMethod]
		public void ShouldGetBrickGeometry()
		{
			Brick b;
			Model model;

			b = new Brick();
			model = b.Build();
			Assert.AreEqual(1, model.Items.Length);
		}




		}
	}
