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


	}
}
