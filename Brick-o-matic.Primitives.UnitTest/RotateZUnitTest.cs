using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Brick_o_matic.Primitives;
using Brick_o_matic.Math;
using System.Linq;

namespace Brick_o_matic.Primitives.UnitTest
{
	[TestClass]
	public class RotateZUnitTest
	{
		[TestMethod]
		public void ShouldInstanciate()
		{
			RotateZ transform;

			transform = new RotateZ();
			Assert.IsNotNull(transform);
			Assert.AreEqual(new Position(), transform.Position);

			transform = new RotateZ(new Position(1, 2, 3));
			Assert.IsNotNull(transform);
			Assert.AreEqual(new Position(1, 2, 3), transform.Position);
		}
		
		[TestMethod]
		public void ShouldReturnFlatBoundingBoxWhenHasNoPrimitive()
		{
			RotateZ transform;
			Box box;

			transform = new RotateZ(new Position(1,2,3));
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
			RotateZ transform;
			Box box;
			Brick b;

			b = new Brick(new Position(1, 0, 5), new Size(4, 2, 5), new Color());

			transform = new RotateZ(new Position(0, 0, 0));
			transform.Count = 1;transform.Item = b;
			box = transform.GetBoundingBox(new Scene());
			Assert.AreEqual(new Position(-1, 1, 5), box.Position);
			Assert.AreEqual(new Size(2, 4, 5), box.Size);
			

			transform = new RotateZ(new Position(2, 2, 2));
			transform.Count = -1; transform.Item = b;
			box = transform.GetBoundingBox(new Scene());
			Assert.AreEqual(new Position(0 + 2, -4 + 2, 5 + 2), box.Position);
			Assert.AreEqual(new Size(2, 4, 5), box.Size);
		}


		[TestMethod]
		public void ShouldGetEmptyBricks()
		{
			RotateZ part;
			Brick[] bricks;
			Scene scene;

			scene = new Scene();


			part = new RotateZ();
			bricks = part.Build(scene).ToArray();
			Assert.AreEqual(0, bricks.Length);
		}

		[TestMethod]
		public void ShouldGetBricks()
		{
			RotateZ transform;
			Brick b;
			Brick[] bricks;

			b = new Brick(new Position(1, 0, 5), new Size(4, 2, 5), new Color());

			transform = new RotateZ(new Position(0, 0, 0));
			transform.Count = 1; transform.Item = b;
			bricks = transform.Build(new Scene()).ToArray();
			Assert.AreEqual(1, bricks.Length);
			Assert.AreEqual(new Position(-1, 1, 5), bricks[0].Position);
			Assert.AreEqual(new Size(2, 4, 5), bricks[0].Size);


			transform = new RotateZ(new Position(2, 2, 2));
			transform.Count = -1; transform.Item = b;
			bricks = transform.Build(new Scene()).ToArray();
			Assert.AreEqual(1, bricks.Length);
			Assert.AreEqual(new Position(0 + 2, -4 + 2, 5 + 2), bricks[0].Position);
			Assert.AreEqual(new Size(2, 4, 5), bricks[0].Size);
		}


		/*[TestMethod]
		public void ShouldReturnFlatICSGNodeWhenHasNoPrimitive()
		{
			RotateZ transform;
			ICSGNode node;

			transform = new RotateZ(new Position(1, 2, 3));
			Assert.IsNotNull(transform);
			node = transform.BuildCSGNode(new Scene(), new Position());
			Assert.AreEqual(1, node.BoundingBox.Position.X);
			Assert.AreEqual(2, node.BoundingBox.Position.Y);
			Assert.AreEqual(3, node.BoundingBox.Position.Z);
			Assert.AreEqual(new Size(0, 0, 0), node.BoundingBox.Size);
			Assert.AreEqual("RotateZ", node.Name);
			Assert.AreEqual(0, node.Count);
			Assert.AreEqual(transform, node.Primitive);

		}

		[TestMethod]
		public void ShouldReturnBoudingICSGNode()
		{
			RotateZ transform;
			ICSGNode node;
			Brick b;

			b = new Brick(new Position(1, 0, 5), new Size(4, 2, 5), new Color());

			transform = new RotateZ(new Position(0, 0, 0));
			transform.Count = 1; transform.Item = b;
			node = transform.BuildCSGNode(new Scene(), new Position());
			Assert.AreEqual(new Position(-1, 1, 5), node.BoundingBox.Position);
			Assert.AreEqual(new Size(2, 4, 5), node.BoundingBox.Size);
			Assert.AreEqual("RotateZ", node.Name);
			Assert.AreEqual(1, node.Count);
			Assert.AreEqual(transform, node.Primitive);


			transform = new RotateZ(new Position(2, 2, 2));
			transform.Count = -1; transform.Item = b;
			node = transform.BuildCSGNode(new Scene(), new Position());
			Assert.AreEqual(new Position(0 + 2, -4 + 2, 5 + 2), node.BoundingBox.Position);
			Assert.AreEqual(new Size(2, 4, 5), node.BoundingBox.Size);
			Assert.AreEqual("RotateZ", node.Name);
			Assert.AreEqual(1, node.Count);
			Assert.AreEqual(transform, node.Primitive);
		}
		*/

	}
}
