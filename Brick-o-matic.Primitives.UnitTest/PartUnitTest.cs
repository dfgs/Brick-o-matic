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
			Assert.AreEqual(new Position(), part.Position);

			part = new Part(new Position(1, 2, 3));
			Assert.IsNotNull(part);
			Assert.AreEqual(new Position(1, 2, 3), part.Position);
		}
		[TestMethod]
		public void ShouldAddChildItem()
		{
			Part part;
			Brick b;

			part = new Part();
			Assert.IsNotNull(part);
			Assert.AreEqual(0, part.Count);
			b = new Brick();
			part.Add(b);
			Assert.AreEqual(1, part.Count);

		}
		[TestMethod]
		public void ShouldNotAddNullChildItem()
		{
			Part part;

			part = new Part();
			Assert.IsNotNull(part);
			Assert.AreEqual(0, part.Count);
			Assert.ThrowsException<ArgumentNullException>(()=> part.Add(null));

		}
		[TestMethod]
		public void ShouldReturnBoundingBoxWhenPartIsEmpty()
		{
			Part part;
			Model model;

			part = new Part();
			Assert.IsNotNull(part);
			model = part.Build();
			Assert.AreEqual(0, model.BoundingBox.Position.X);
			Assert.AreEqual(0, model.BoundingBox.Position.Y);
			Assert.AreEqual(0, model.BoundingBox.Position.Z);
			Assert.AreEqual(new Size(0, 0, 0), model.BoundingBox.Size);
		}

		[TestMethod]
		public void ShouldReturnModel()
		{
			Part part;
			Model model;
			Brick b;

			part = new Part(new Position(0, 0, 0));
			Assert.IsNotNull(part);
			b = new Brick(new Position(-1, -2, -3), new Size(1, 1, 1));
			part.Add(b);
			model = part.Build();
			Assert.AreEqual(-1, model.BoundingBox.Position.X);
			Assert.AreEqual(-2, model.BoundingBox.Position.Y);
			Assert.AreEqual(-3, model.BoundingBox.Position.Z);
			Assert.AreEqual(new Size(1, 1, 1), model.BoundingBox.Size);

			part = new Part(new Position(1, 2, 3));
			Assert.IsNotNull(part);
			b = new Brick(new Position(-1, -2, -3), new Size(1, 1, 1));
			part.Add(b);
			model = part.Build();
			Assert.AreEqual(0, model.BoundingBox.Position.X);
			Assert.AreEqual(0, model.BoundingBox.Position.Y);
			Assert.AreEqual(0, model.BoundingBox.Position.Z);
			Assert.AreEqual(new Size(1, 1, 1), model.BoundingBox.Size);

			part = new Part(new Position(0, 0, 0));
			Assert.IsNotNull(part);
			b = new Brick(new Position(-1, -2, -3), new Size(1, 1, 1));
			part.Add(b);
			b = new Brick(new Position(1, 2, 3), new Size(1, 1, 1));
			part.Add(b);
			model = part.Build();
			Assert.AreEqual(-1, model.BoundingBox.Position.X);
			Assert.AreEqual(-2, model.BoundingBox.Position.Y);
			Assert.AreEqual(-3, model.BoundingBox.Position.Z);
			Assert.AreEqual(new Size(3, 5, 7), model.BoundingBox.Size);

			part = new Part(new Position(0, 0, 0));
			Assert.IsNotNull(part);
			b = new Brick(new Position(-1, -2, -3), new Size(2, 2, 2));
			part.Add(b);
			b = new Brick(new Position(1, 2, 3), new Size(2, 2, 2));
			part.Add(b);
			model = part.Build();
			Assert.AreEqual(-1, model.BoundingBox.Position.X);
			Assert.AreEqual(-2, model.BoundingBox.Position.Y);
			Assert.AreEqual(-3, model.BoundingBox.Position.Z);
			Assert.AreEqual(new Size(4, 6, 8), model.BoundingBox.Size);

		}
		[TestMethod]
		public void ShouldReturnNestedModel()
		{
			Part part;
			Part p1, p2;
			Brick b1, b2;
			Model model;

			part = new Part();

			p1 = new Part();
			b1 = new Brick(new Position(-1,-1,-1));
			p1.Add(b1);

			p2 = new Part();
			b2 = new Brick(new Position(1, 1, 1));
			p2.Add(b2);

			part.Add(b1);
			part.Add(b2);

			model = part.Build();
			Assert.AreEqual(-1, model.BoundingBox.Position.X);
			Assert.AreEqual(-1, model.BoundingBox.Position.Y);
			Assert.AreEqual(-1, model.BoundingBox.Position.Z);
			Assert.AreEqual(new Size(3, 3, 3), model.BoundingBox.Size);
		}


		[TestMethod]
		public void ShouldGetEmptyGeometry()
		{
			Part part;
			Model model;

			part = new Part();
			model = part.Build();
			Assert.AreEqual(0, model.Items.Length);
		}

		[TestMethod]
		public void ShouldGetGeometryItem()
		{
			Part part;
			Brick b;
			Model model;

			part = new Part();
			b = new Brick();
			part.Add(b);

			model = part.Build();
			Assert.AreEqual(1, model.Items.Length);
		}

		[TestMethod]
		public void ShouldGetNestedGeometryItems()
		{
			Part part;
			Part p1,p2;
			Brick b1,b2;
			Model model;

			part = new Part();

			p1 = new Part();
			b1 = new Brick();
			p1.Add(b1);
			
			p2 = new Part();
			b2 = new Brick();
			p2.Add(b2);

			part.Add(b1);
			part.Add(b2);

			model = part.Build();
			Assert.AreEqual(2, model.Items.Length);
		}
	}
}
