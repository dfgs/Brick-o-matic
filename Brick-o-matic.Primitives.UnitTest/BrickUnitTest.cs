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


		[TestMethod]
		public void ShouldBuildModel()
		{
			Brick b;
			Brick[] bricks;
			Scene scene;

			scene = new Scene();

			b = new Brick();
			bricks = b.Build(scene).ToArray();
			Assert.AreEqual(1, bricks.Length);
			Assert.AreEqual(0, bricks[0].Position.X);
			Assert.AreEqual(0, bricks[0].Position.Y);
			Assert.AreEqual(0, bricks[0].Position.Z);
			Assert.AreEqual(new Size(1, 1, 1), bricks[0].Size);

			b = new Brick(new Position(1, 2, 3));
			bricks = b.Build(scene).ToArray();
			Assert.AreEqual(1, bricks.Length);
			Assert.AreEqual(1, bricks[0].Position.X);
			Assert.AreEqual(2, bricks[0].Position.Y);
			Assert.AreEqual(3, bricks[0].Position.Z);
			Assert.AreEqual(new Size(1, 1, 1), bricks[0].Size);

			b = new Brick(new Position(1, 2, 3), new Size(5, 5, 5));
			bricks = b.Build(scene).ToArray();
			Assert.AreEqual(1, bricks.Length);
			Assert.AreEqual(1, bricks[0].Position.X);
			Assert.AreEqual(2, bricks[0].Position.Y);
			Assert.AreEqual(3, bricks[0].Position.Z);
			Assert.AreEqual(new Size(5, 5, 5), bricks[0].Size);
		}

		[TestMethod]
		public void ShouldReturnBoudingBox()
		{
			Brick b;
			Box box;

			b = new Brick(new Position(1, 2, 3), new Size(5, 5, 5));
			box = b.GetBoundingBox(new Scene());
			Assert.AreEqual(new Position(1, 2, 3), box.Position);
			Assert.AreEqual(new Size(5, 5, 5), box.Size);

		}

		[TestMethod]
		public void ShouldReturnICSGNode()
		{
			Brick b;
			ICSGNode node;

			b = new Brick(new Position(1, 2, 3), new Size(5, 5, 5));
			node = b.BuildCSGNode(new Scene());
			Assert.AreEqual(new Position(1, 2, 3), node.BoundingBox.Position);
			Assert.AreEqual(new Size(5, 5, 5), node.BoundingBox.Size);
			Assert.AreEqual("Brick", node.Name);
			Assert.AreEqual(0, node.Count);
			Assert.AreEqual(b, node.Primitive);

		}
		[TestMethod]
		public void ShouldRotateX()
		{
			Brick b, rotate;

			b = new Brick(new Position(5, 0, 1), new Size(5, 2, 4), new Color());
			rotate = b.RotateX(0);
			Assert.AreEqual(new Position(5, 0, 1), rotate.Position);
			Assert.AreEqual(new Size(5, 2, 4), rotate.Size);
			rotate = b.RotateX(4);
			Assert.AreEqual(new Position(5, 0, 1), rotate.Position);
			Assert.AreEqual(new Size(5, 2, 4), rotate.Size);

			b = new Brick(new Position(5, 0, 1), new Size(5, 2, 4), new Color());
			rotate = b.RotateX(1);
			Assert.AreEqual(new Position(5, -4, 0), rotate.Position);
			Assert.AreEqual(new Size(5, 4, 2), rotate.Size);
			b = new Brick(new Position(5, 0, 1), new Size(5, 2, 4), new Color());
			rotate = b.RotateX(5);
			Assert.AreEqual(new Position(5, -4, 0), rotate.Position);
			Assert.AreEqual(new Size(5, 4, 2), rotate.Size);

			b = new Brick(new Position(5, 0, 1), new Size(5, 2, 4), new Color());
			rotate = b.RotateX(2);
			Assert.AreEqual(new Position(5, -1, -4), rotate.Position);
			Assert.AreEqual(new Size(5, 2, 4), rotate.Size);
			b = new Brick(new Position(5, 0, 1), new Size(5, 2, 4), new Color());
			rotate = b.RotateX(6);
			Assert.AreEqual(new Position(5, -1, -4), rotate.Position);
			Assert.AreEqual(new Size(5, 2, 4), rotate.Size);

			b = new Brick(new Position(5, 0, 1), new Size(5, 2, 4), new Color());
			rotate = b.RotateX(3);
			Assert.AreEqual(new Position(5, 1, -1), rotate.Position);
			Assert.AreEqual(new Size(5, 4, 2), rotate.Size);
			b = new Brick(new Position(5, 0, 1), new Size(5, 2, 4), new Color());
			rotate = b.RotateX(7);
			Assert.AreEqual(new Position(5, 1, -1), rotate.Position);
			Assert.AreEqual(new Size(5, 4, 2), rotate.Size);//*/

			b = new Brick(new Position(5, 0, 1), new Size(5, 2, 4), new Color());
			rotate = b.RotateX(-1);
			Assert.AreEqual(new Position(5, 1, -1), rotate.Position);
			Assert.AreEqual(new Size(5, 4, 2), rotate.Size);
		}

		[TestMethod]
		public void ShouldRotateXLineShape()
		{
			Brick b, rotate;

			b = new Brick(new Position(0, 0, 1), new Size(1, 1, 4), new Color());
			rotate = b.RotateX(-1);
			Assert.AreEqual(new Position(0, 1, 0), rotate.Position);

			b = new Brick(new Position(0, 0, 1), new Size(1, 1, 4), new Color());
			rotate = b.RotateX(-2);
			Assert.AreEqual(new Position(0, 0, -4), rotate.Position);

			b = new Brick(new Position(0, 0, 1), new Size(1, 1, 4), new Color());
			rotate = b.RotateX(-3);
			Assert.AreEqual(new Position(0, -4, 0), rotate.Position);
		}

		[TestMethod]
		public void ShouldRotateY()
		{
			Brick b, rotate;

			b = new Brick(new Position(1, 5, 0), new Size(4, 5, 2), new Color());
			rotate = b.RotateY(0);
			Assert.AreEqual(new Position(1, 5, 0), rotate.Position);
			Assert.AreEqual(new Size(4, 5, 2), rotate.Size);
			rotate = b.RotateY(4);
			Assert.AreEqual(new Position(1, 5, 0), rotate.Position);
			Assert.AreEqual(new Size(4, 5, 2), rotate.Size);

			b = new Brick(new Position(1, 5, 0), new Size(4, 5, 2), new Color());
			rotate = b.RotateY(1);
			Assert.AreEqual(new Position(0, 5, -4), rotate.Position);
			Assert.AreEqual(new Size(2, 5, 4), rotate.Size);
			b = new Brick(new Position(1, 5, 0), new Size(4, 5, 2), new Color());
			rotate = b.RotateY(5);
			Assert.AreEqual(new Position(0, 5, -4), rotate.Position);
			Assert.AreEqual(new Size(2, 5, 4), rotate.Size);

			b = new Brick(new Position(1, 5, 0), new Size(4, 5, 2), new Color());
			rotate = b.RotateY(2);
			Assert.AreEqual(new Position(-4, 5, -1), rotate.Position);
			Assert.AreEqual(new Size(4, 5, 2), rotate.Size);
			b = new Brick(new Position(1, 5, 0), new Size(4, 5, 2), new Color());
			rotate = b.RotateY(6);
			Assert.AreEqual(new Position(-4, 5, -1), rotate.Position);
			Assert.AreEqual(new Size(4, 5, 2), rotate.Size);

			b = new Brick(new Position(1, 5, 0), new Size(4, 5, 2), new Color());
			rotate = b.RotateY(3);
			Assert.AreEqual(new Position(-1, 5, 1), rotate.Position);
			Assert.AreEqual(new Size(2, 5, 4), rotate.Size);
			b = new Brick(new Position(1, 5, 0), new Size(4, 5, 2), new Color());
			rotate = b.RotateY(7);
			Assert.AreEqual(new Position(-1, 5, 1), rotate.Position);
			Assert.AreEqual(new Size(2, 5, 4), rotate.Size);

			b = new Brick(new Position(1, 5, 0), new Size(4, 5, 2), new Color());
			rotate = b.RotateY(-1);
			Assert.AreEqual(new Position(-1, 5, 1), rotate.Position);
			Assert.AreEqual(new Size(2, 5, 4), rotate.Size);


		}
		[TestMethod]
		public void ShouldRotateYLineShape()
		{
			Brick b, rotate;

			b = new Brick(new Position(1, 0, 0), new Size(4, 1, 1), new Color());
			rotate = b.RotateY(-1);
			Assert.AreEqual(new Position(0, 0, 1), rotate.Position);
			
			b = new Brick(new Position(1, 0, 0), new Size(4, 1, 1), new Color());
			rotate = b.RotateY(-2);
			Assert.AreEqual(new Position(-4, 0, 0), rotate.Position);

			b = new Brick(new Position(1, 0, 0), new Size(4, 1, 1), new Color());
			rotate = b.RotateY(-3);
			Assert.AreEqual(new Position(0, 0, -4), rotate.Position);
		}

		[TestMethod]
		public void ShouldRotateZ()
		{
			Brick b,rotate;

			b = new Brick(new Position(1, 0,5), new Size(4, 2,5), new Color());
			rotate = b.RotateZ(0);
			Assert.AreEqual(new Position(1, 0, 5), rotate.Position);
			Assert.AreEqual(new Size(4, 2, 5), rotate.Size);
			rotate = b.RotateZ(4);
			Assert.AreEqual(new Position(1, 0, 5), rotate.Position);
			Assert.AreEqual(new Size(4, 2, 5), rotate.Size);

			b = new Brick(new Position(1, 0, 5), new Size(4, 2, 5), new Color());
			rotate = b.RotateZ(1);
			Assert.AreEqual(new Position(-1, 1, 5), rotate.Position);
			Assert.AreEqual(new Size(2, 4, 5), rotate.Size);
			b = new Brick(new Position(1, 0, 5), new Size(4, 2, 5), new Color());
			rotate = b.RotateZ(5);
			Assert.AreEqual(new Position(-1, 1, 5), rotate.Position);
			Assert.AreEqual(new Size(2, 4, 5), rotate.Size);

			b = new Brick(new Position(1, 0, 5), new Size(4, 2, 5), new Color());
			rotate = b.RotateZ(2);
			Assert.AreEqual(new Position(-4, -1, 5), rotate.Position);
			Assert.AreEqual(new Size(4, 2, 5), rotate.Size);
			b = new Brick(new Position(1, 0, 5), new Size(4, 2, 5), new Color());
			rotate = b.RotateZ(6);
			Assert.AreEqual(new Position(-4, -1, 5), rotate.Position);
			Assert.AreEqual(new Size(4, 2, 5), rotate.Size);

			b = new Brick(new Position(1, 0, 5), new Size(4, 2, 5), new Color());
			rotate = b.RotateZ(3);
			Assert.AreEqual(new Position(0, -4, 5), rotate.Position);
			Assert.AreEqual(new Size(2, 4, 5), rotate.Size);
			b = new Brick(new Position(1, 0, 5), new Size(4, 2, 5), new Color());
			rotate = b.RotateZ(7);
			Assert.AreEqual(new Position(0, -4, 5), rotate.Position);
			Assert.AreEqual(new Size(2, 4, 5), rotate.Size);

			b = new Brick(new Position(1, 0, 5), new Size(4, 2, 5), new Color());
			rotate = b.RotateZ(-1);
			Assert.AreEqual(new Position(0, -4, 5), rotate.Position);
			Assert.AreEqual(new Size(2, 4, 5), rotate.Size);
		}

		[TestMethod]
		public void ShouldRotateZLineShape()
		{
			Brick b, rotate;

			b = new Brick(new Position(0, 1, 0), new Size(1, 4, 1), new Color());
			rotate = b.RotateZ(-1);
			Assert.AreEqual(new Position(1, 0, 0), rotate.Position);

			b = new Brick(new Position(0, 1, 0), new Size(1, 4, 1), new Color());
			rotate = b.RotateZ(-2);
			Assert.AreEqual(new Position(0, -4, 0), rotate.Position);

			b = new Brick(new Position(0, 1, 0), new Size(1, 4, 1), new Color());
			rotate = b.RotateZ(-3);
			Assert.AreEqual(new Position(-4, 0, 0), rotate.Position);
		}


	}
}
