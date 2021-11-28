using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Brick_o_matic.Primitives;
using Brick_o_matic.Math;
using System.Linq;

namespace Brick_o_matic.Primitives.UnitTest
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
		public void ShouldReturnFlatBoundingBoxWhenHasNoItems()
		{
			Part part;
			IBox box;

			part = new Part(new Position(1,2,3));
			Assert.IsNotNull(part);
			box = part.GetBoundingBox(new Scene());
			Assert.AreEqual(1, box.Position.X);
			Assert.AreEqual(2, box.Position.Y);
			Assert.AreEqual(3, box.Position.Z);
			Assert.AreEqual(new Size(0, 0, 0), box.Size);
		}

		[TestMethod]
		public void ShouldReturnBoudingBox()
		{
			Part part;
			IBox box;
			Brick b;

			part = new Part(new Position(0, 0, 0));
			Assert.IsNotNull(part);
			b = new Brick(new Position(-1, -2, -3), new Size(1, 1, 1));
			part.Add(b);
			box = part.GetBoundingBox(new Scene());
			Assert.AreEqual(-1, box.Position.X);
			Assert.AreEqual(-2, box.Position.Y);
			Assert.AreEqual(-3, box.Position.Z);
			Assert.AreEqual(new Size(1, 1, 1), box.Size);

			part = new Part(new Position(1, 2, 3));
			Assert.IsNotNull(part);
			b = new Brick(new Position(-1, -2, -3), new Size(1, 1, 1));
			part.Add(b);
			box = part.GetBoundingBox(new Scene());
			Assert.AreEqual(0, box.Position.X);
			Assert.AreEqual(0, box.Position.Y);
			Assert.AreEqual(0, box.Position.Z);
			Assert.AreEqual(new Size(1, 1, 1), box.Size);

			part = new Part(new Position(0, 0, 0));
			Assert.IsNotNull(part);
			b = new Brick(new Position(-1, -2, -3), new Size(1, 1, 1));
			part.Add(b);
			b = new Brick(new Position(1, 2, 3), new Size(1, 1, 1));
			part.Add(b);
			box = part.GetBoundingBox(new Scene());
			Assert.AreEqual(-1, box.Position.X);
			Assert.AreEqual(-2, box.Position.Y);
			Assert.AreEqual(-3, box.Position.Z);
			Assert.AreEqual(new Size(3, 5, 7), box.Size);

			part = new Part(new Position(0, 0, 0));
			Assert.IsNotNull(part);
			b = new Brick(new Position(-1, -2, -3), new Size(2, 2, 2));
			part.Add(b);
			b = new Brick(new Position(1, 2, 3), new Size(2, 2, 2));
			part.Add(b);
			box = part.GetBoundingBox(new Scene());
			Assert.AreEqual(-1, box.Position.X);
			Assert.AreEqual(-2, box.Position.Y);
			Assert.AreEqual(-3, box.Position.Z);
			Assert.AreEqual(new Size(4, 6, 8), box.Size);

		}
		[TestMethod]
		public void ShouldReturnNestedBoudingBox()
		{
			Part part;
			Part p1, p2;
			Brick b1, b2;
			IBox box;

			part = new Part();

			p1 = new Part();
			b1 = new Brick(new Position(-1,-1,-1));
			p1.Add(b1);

			p2 = new Part();
			b2 = new Brick(new Position(1, 1, 1));
			p2.Add(b2);

			part.Add(p1);
			part.Add(p2);

			box = part.GetBoundingBox(new Scene());
			Assert.AreEqual(-1, box.Position.X);
			Assert.AreEqual(-1, box.Position.Y);
			Assert.AreEqual(-1, box.Position.Z);
			Assert.AreEqual(new Size(3, 3, 3), box.Size);
		}


		[TestMethod]
		public void ShouldGetEmptyBricks()
		{
			Part part;
			Brick[] bricks;
			Scene scene;

			scene = new Scene();


			part = new Part();
			bricks = part.Build(scene).ToArray();
			Assert.AreEqual(0, bricks.Length);
		}

		[TestMethod]
		public void ShouldGetBricks()
		{
			Part part;
			Brick b;
			Brick[] bricks;
			Scene scene;

			scene = new Scene();


			part = new Part();
			b = new Brick();
			part.Add(b);

			bricks = part.Build(scene).ToArray();
			Assert.AreEqual(1, bricks.Length);
		}

		[TestMethod]
		public void ShouldGetNestedBricks()
		{
			Part part;
			Part p1,p2;
			Brick b1,b2;
			Brick[] bricks;
			Scene scene;

			scene = new Scene();


			part = new Part();

			p1 = new Part();
			b1 = new Brick();
			p1.Add(b1);
			
			p2 = new Part();
			b2 = new Brick();
			p2.Add(b2);

			part.Add(p1);
			part.Add(p2);

			bricks = part.Build(scene).ToArray();
			Assert.AreEqual(2, bricks.Length);
		}

		/*[TestMethod]
		public void ShouldReturnFlatICSGNodeWhenHasNoItems()
		{
			Part part;
			ICSGNode node;

			part = new Part(new Position(1, 2, 3));
			Assert.IsNotNull(part);
			node = part.BuildCSGNode(new Scene(),new Position());
			Assert.AreEqual(1, node.BoundingBox.Position.X);
			Assert.AreEqual(2, node.BoundingBox.Position.Y);
			Assert.AreEqual(3, node.BoundingBox.Position.Z);
			Assert.AreEqual(new Size(0, 0, 0), node.BoundingBox.Size);

			Assert.AreEqual("Part", node.Name);
			Assert.AreEqual(0, node.Count);
			Assert.AreEqual(part, node.Primitive);

		}

		[TestMethod]
		public void ShouldReturnICSGNode()
		{
			Part part;
			ICSGNode node;
			Brick b;

			part = new Part(new Position(0, 0, 0));
			Assert.IsNotNull(part);
			b = new Brick(new Position(-1, -2, -3), new Size(1, 1, 1));
			part.Add(b);
			node = part.BuildCSGNode(new Scene(), new Position());
			Assert.AreEqual(-1, node.BoundingBox.Position.X);
			Assert.AreEqual(-2, node.BoundingBox.Position.Y);
			Assert.AreEqual(-3, node.BoundingBox.Position.Z);
			Assert.AreEqual(new Size(1, 1, 1), node.BoundingBox.Size);
			Assert.AreEqual("Part", node.Name);
			Assert.AreEqual(1, node.Count);
			Assert.AreEqual(part, node.Primitive);
			Assert.AreEqual(new Position(-1, -2, -3), node.Nodes.ElementAt(0).BoundingBox.Position);

			part = new Part(new Position(1, 2, 3));
			Assert.IsNotNull(part);
			b = new Brick(new Position(-1, -2, -3), new Size(1, 1, 1));
			part.Add(b);
			node = part.BuildCSGNode(new Scene(), new Position());
			Assert.AreEqual(0, node.BoundingBox.Position.X);
			Assert.AreEqual(0, node.BoundingBox.Position.Y);
			Assert.AreEqual(0, node.BoundingBox.Position.Z);
			Assert.AreEqual(new Size(1, 1, 1), node.BoundingBox.Size);
			Assert.AreEqual("Part", node.Name);
			Assert.AreEqual(1, node.Count);
			Assert.AreEqual(part, node.Primitive);
			Assert.AreEqual(new Position(0, 0, 0), node.Nodes.ElementAt(0).BoundingBox.Position);

			part = new Part(new Position(0, 0, 0));
			Assert.IsNotNull(part);
			b = new Brick(new Position(-1, -2, -3), new Size(1, 1, 1));
			part.Add(b);
			b = new Brick(new Position(1, 2, 3), new Size(1, 1, 1));
			part.Add(b);
			node = part.BuildCSGNode(new Scene(), new Position());
			Assert.AreEqual(-1, node.BoundingBox.Position.X);
			Assert.AreEqual(-2, node.BoundingBox.Position.Y);
			Assert.AreEqual(-3, node.BoundingBox.Position.Z);
			Assert.AreEqual(new Size(3, 5, 7), node.BoundingBox.Size);
			Assert.AreEqual("Part", node.Name);
			Assert.AreEqual(2, node.Count);
			Assert.AreEqual(part, node.Primitive);
			Assert.AreEqual(new Position(-1, -2, -3), node.Nodes.ElementAt(0).BoundingBox.Position);

			part = new Part(new Position(1, 1, 1));
			Assert.IsNotNull(part);
			b = new Brick(new Position(-1, -2, -3), new Size(2, 2, 2));
			part.Add(b);
			b = new Brick(new Position(1, 2, 3), new Size(2, 2, 2));
			part.Add(b);
			node = part.BuildCSGNode(new Scene(), new Position());
			Assert.AreEqual(0, node.BoundingBox.Position.X);
			Assert.AreEqual(-1, node.BoundingBox.Position.Y);
			Assert.AreEqual(-2, node.BoundingBox.Position.Z);
			Assert.AreEqual(new Size(4, 6, 8), node.BoundingBox.Size);
			Assert.AreEqual("Part", node.Name);
			Assert.AreEqual(2, node.Count);
			Assert.AreEqual(part, node.Primitive);
			Assert.AreEqual(new Position(0, -1, -2), node.Nodes.ElementAt(0).BoundingBox.Position);
			Assert.AreEqual(new Position(2, 3, 4), node.Nodes.ElementAt(1).BoundingBox.Position);
		
		}
		[TestMethod]
		public void ShouldReturnNestedBoudingICSGNode()
		{
			Part part;
			Part p1, p2;
			Brick b1, b2;
			ICSGNode node;

			part = new Part();

			p1 = new Part();
			b1 = new Brick(new Position(-1, -1, -1));
			p1.Add(b1);

			p2 = new Part();
			b2 = new Brick(new Position(1, 1, 1));
			p2.Add(b2);

			part.Add(p1);
			part.Add(p2);

			node = part.BuildCSGNode(new Scene(), new Position());
			Assert.AreEqual(-1, node.BoundingBox.Position.X);
			Assert.AreEqual(-1, node.BoundingBox.Position.Y);
			Assert.AreEqual(-1, node.BoundingBox.Position.Z);
			Assert.AreEqual(new Size(3, 3, 3), node.BoundingBox.Size);
			Assert.AreEqual("Part", node.Name);
			Assert.AreEqual(2, node.Count);
			Assert.AreEqual(part, node.Primitive);
		}*/



	}
}
