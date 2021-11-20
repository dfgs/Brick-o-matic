using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Brick_o_matic.Primitives;
using Brick_o_matic.Math;
using System.Linq;

namespace Brick_o_matic.Primitives.UnitTest
{
	[TestClass]
	public class RotateXUnitTest
	{
		[TestMethod]
		public void ShouldInstanciate()
		{
			RotateX transform;

			transform = new RotateX();
			Assert.IsNotNull(transform);
			Assert.AreEqual(new Position(), transform.Position);

			transform = new RotateX(new Position(1, 2, 3));
			Assert.IsNotNull(transform);
			Assert.AreEqual(new Position(1, 2, 3), transform.Position);
		}
		
		[TestMethod]
		public void ShouldReturnFlatBoundingBoxWhenHasNoPrimitive()
		{
			RotateX transform;
			Box box;

			transform = new RotateX(new Position(1,2,3));
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
			RotateX transform;
			Box box;
			Brick b;

			b = new Brick(new Position(5, 0, 1), new Size(5, 2, 4), new Color());

			transform = new RotateX(new Position(0, 0, 0));
			transform.Count = 1;transform.Item = b;
			box = transform.GetBoundingBox(new Scene());
			Assert.AreEqual(new Position(5, -4, 0), box.Position);
			Assert.AreEqual(new Size(5, 4, 2), box.Size);
			

			transform = new RotateX(new Position(2, 2, 2));
			transform.Count = -1; transform.Item = b;
			box = transform.GetBoundingBox(new Scene());
			Assert.AreEqual(new Position(5 +2 , 1 +2 , -1 +2), box.Position);
			Assert.AreEqual(new Size(5, 4, 2), box.Size);
		}


		[TestMethod]
		public void ShouldGetEmptyBricks()
		{
			RotateX part;
			Brick[] bricks;
			Scene scene;

			scene = new Scene();


			part = new RotateX();
			bricks = part.Build(scene).ToArray();
			Assert.AreEqual(0, bricks.Length);
		}

		[TestMethod]
		public void ShouldGetBricks()
		{
			RotateX transform;
			Brick b;
			Brick[] bricks;

			b = new Brick(new Position(5, 0, 1), new Size(5, 2, 4), new Color());

			transform = new RotateX(new Position(0, 0, 0));
			transform.Count = 1; transform.Item = b;
			bricks = transform.Build(new Scene()).ToArray();
			Assert.AreEqual(1, bricks.Length);
			Assert.AreEqual(new Position(5, -4, 0), bricks[0].Position);
			Assert.AreEqual(new Size(5, 4, 2), bricks[0].Size);


			transform = new RotateX(new Position(2, 2, 2));
			transform.Count = -1; transform.Item = b;
			bricks = transform.Build(new Scene()).ToArray();
			Assert.AreEqual(1, bricks.Length);
			Assert.AreEqual(new Position(5 + 2, 1 + 2, -1 + 2), bricks[0].Position);
			Assert.AreEqual(new Size(5, 4, 2), bricks[0].Size);
		}

		/*
		[TestMethod]
		public void ShouldReturnFlatICSGNodeWhenHasNoPrimitive()
		{
			RotateX transform;
			ICSGNode node;

			transform = new RotateX(new Position(1, 2, 3));
			Assert.IsNotNull(transform);
			node = transform.BuildCSGNode(new Scene(), new Position());
			Assert.AreEqual(1, node.BoundingBox.Position.X);
			Assert.AreEqual(2, node.BoundingBox.Position.Y);
			Assert.AreEqual(3, node.BoundingBox.Position.Z);
			Assert.AreEqual(new Size(0, 0, 0), node.BoundingBox.Size);
			Assert.AreEqual("RotateX", node.Name);
			Assert.AreEqual(0, node.Count);
			Assert.AreEqual(transform, node.Primitive);
		}

		[TestMethod]
		public void ShouldReturnBoudingICSGNode()
		{
			RotateX transform;
			ICSGNode node;
			Brick b;

			b = new Brick(new Position(5, 0, 1), new Size(5, 2, 4), new Color());

			transform = new RotateX(new Position(0, 0, 0));
			transform.Count = 1; transform.Item = b;
			node = transform.BuildCSGNode(new Scene(), new Position());
			Assert.AreEqual(new Position(5, -4, 0), node.BoundingBox.Position);
			Assert.AreEqual(new Size(5, 4, 2), node.BoundingBox.Size);
			Assert.AreEqual("RotateX", node.Name);
			Assert.AreEqual(1, node.Count);
			Assert.AreEqual(transform, node.Primitive);


			transform = new RotateX(new Position(2, 2, 2));
			transform.Count = -1; transform.Item = b;
			node = transform.BuildCSGNode(new Scene(), new Position());
			Assert.AreEqual(new Position(5 + 2, 1 + 2, -1 + 2), node.BoundingBox.Position);
			Assert.AreEqual(new Size(5, 4, 2), node.BoundingBox.Size);
			Assert.AreEqual("RotateX", node.Name);
			Assert.AreEqual(1, node.Count);
			Assert.AreEqual(transform, node.Primitive);
		}

		*/



	}
}
