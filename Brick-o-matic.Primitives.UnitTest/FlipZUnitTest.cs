using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Brick_o_matic.Primitives;
using Brick_o_matic.Math;
using System.Linq;

namespace Brick_o_matic.Primitives.UnitTest
{
	[TestClass]
	public class FlipZUnitTest
	{
		[TestMethod]
		public void ShouldInstanciate()
		{
			FlipZ transform;

			transform = new FlipZ();
			Assert.IsNotNull(transform);
			Assert.AreEqual(new Position(), transform.Position);

			transform = new FlipZ(new Position(1, 2, 3));
			Assert.IsNotNull(transform);
			Assert.AreEqual(new Position(1, 2, 3), transform.Position);
		}
		
		[TestMethod]
		public void ShouldReturnFlatBoundingBoxWhenHasNoPrimitive()
		{
			FlipZ transform;
			Box box;

			transform = new FlipZ(new Position(1,2,3));
			Assert.IsNotNull(transform);
			box = transform.GetBoundingBox(new Scene());
			Assert.AreEqual(1, box.Position.X);
			Assert.AreEqual(2, box.Position.Y);
			Assert.AreEqual(3, box.Position.Z);
			Assert.AreEqual(new Size(0, 0, 0), box.Size);
		}

		[TestMethod]
		public void ShouldReturnBoudingBox()
		{
			FlipZ transform;
			Box box;
			Brick b;

			b = new Brick(new Position(1, 1, 2), new Size(1, 1, 2), new Color());

			transform = new FlipZ(new Position(0, 0, 0));
			transform.Item = b;
			box = transform.GetBoundingBox(new Scene());
			Assert.AreEqual(new Position(1, 1, -3), box.Position);
			Assert.AreEqual(new Size(1, 1, 2), box.Size);
			

			transform = new FlipZ(new Position(2, 2, 2));
			transform.Item = b;
			box = transform.GetBoundingBox(new Scene());
			Assert.AreEqual(new Position(1 +2 , 1 +2 , -3 + 2), box.Position);
			Assert.AreEqual(new Size(1, 1, 2), box.Size);
		}


		[TestMethod]
		public void ShouldGetEmptyBricks()
		{
			FlipZ part;
			Brick[] bricks;
			Scene scene;

			scene = new Scene();


			part = new FlipZ();
			bricks = part.Build(scene).ToArray();
			Assert.AreEqual(0, bricks.Length);
		}

		[TestMethod]
		public void ShouldGetBricks()
		{
			FlipZ transform;
			Brick b;
			Brick[] bricks;

			b = new Brick(new Position(1, 1, 2), new Size(1, 1, 2), new Color());


			transform = new FlipZ(new Position(0, 0, 0));
			transform.Item = b;
			bricks = transform.Build(new Scene()).ToArray();
			Assert.AreEqual(1, bricks.Length);
			Assert.AreEqual(new Position(1, 1, -3), bricks[0].Position);
			Assert.AreEqual(new Size(1, 1, 2), bricks[0].Size);


			transform = new FlipZ(new Position(2, 2, 2));
			transform.Item = b;
			bricks = transform.Build(new Scene()).ToArray();
			Assert.AreEqual(1, bricks.Length);
			Assert.AreEqual(new Position(1 + 2, 1 + 2, -3 + 2), bricks[0].Position);
			Assert.AreEqual(new Size(1, 1, 2), bricks[0].Size);
		}



		[TestMethod]
		public void ShouldReturnFlatICSGNodeWhenHasNoPrimitive()
		{
			FlipZ transform;
			ICSGNode node;

			transform = new FlipZ(new Position(1, 2, 3));
			Assert.IsNotNull(transform);
			node = transform.BuildCSGNode(new Scene());
			Assert.AreEqual(1, node.BoundingBox.Position.X);
			Assert.AreEqual(2, node.BoundingBox.Position.Y);
			Assert.AreEqual(3, node.BoundingBox.Position.Z);
			Assert.AreEqual(new Size(0, 0, 0), node.BoundingBox.Size);
			Assert.AreEqual("FlipZ", node.Name);
			Assert.AreEqual(0, node.Count);
			Assert.AreEqual(transform, node.Primitive);
		}

		[TestMethod]
		public void ShouldReturnBoudingICSGNode()
		{
			FlipZ transform;
			ICSGNode node;
			Brick b;

			b = new Brick(new Position(1, 1, 2), new Size(1, 1, 2), new Color());

			transform = new FlipZ(new Position(0, 0, 0));
			transform.Item = b;
			node = transform.BuildCSGNode(new Scene());
			Assert.AreEqual(new Position(1, 1, -3), node.BoundingBox.Position);
			Assert.AreEqual(new Size(1, 1, 2), node.BoundingBox.Size);
			Assert.AreEqual("FlipZ", node.Name);
			Assert.AreEqual(1, node.Count);
			Assert.AreEqual(transform, node.Primitive);


			transform = new FlipZ(new Position(2, 2, 2));
			transform.Item = b;
			node = transform.BuildCSGNode(new Scene());
			Assert.AreEqual(new Position(1 + 2, 1 + 2, -3 + 2), node.BoundingBox.Position);
			Assert.AreEqual(new Size(1, 1, 2), node.BoundingBox.Size);
			Assert.AreEqual("FlipZ", node.Name);
			Assert.AreEqual(1, node.Count);
			Assert.AreEqual(transform, node.Primitive);
		}

	}
}
