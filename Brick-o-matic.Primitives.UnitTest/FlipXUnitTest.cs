using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Brick_o_matic.Primitives;
using Brick_o_matic.Math;
using System.Linq;

namespace Brick_o_matic.Primitives.UnitTest
{
	[TestClass]
	public class FlipXUnitTest
	{
		[TestMethod]
		public void ShouldInstanciate()
		{
			FlipX transform;

			transform = new FlipX();
			Assert.IsNotNull(transform);
			Assert.AreEqual(new Position(), transform.Position);

			transform = new FlipX(new Position(1, 2, 3));
			Assert.IsNotNull(transform);
			Assert.AreEqual(new Position(1, 2, 3), transform.Position);
		}
		
		[TestMethod]
		public void ShouldReturnFlatBoundingBoxWhenHasNoPrimitive()
		{
			FlipX transform;
			Box box;

			transform = new FlipX(new Position(1,2,3));
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
			FlipX transform;
			Box box;
			Brick b;

			b = new Brick(new Position(2, 1, 1), new Size(2, 1, 1), new Color());

			transform = new FlipX(new Position(0, 0, 0));
			transform.Item = b;
			box = transform.GetBoundingBox(new Scene());
			Assert.AreEqual(new Position(-3, 1, 1), box.Position);
			Assert.AreEqual(new Size(2, 1, 1), box.Size);
			

			transform = new FlipX(new Position(2, 2, 2));
			transform.Item = b;
			box = transform.GetBoundingBox(new Scene());
			Assert.AreEqual(new Position(-3 +2 , 1 +2 , 1 +2), box.Position);
			Assert.AreEqual(new Size(2, 1, 1), box.Size);
		}


		[TestMethod]
		public void ShouldGetEmptyBricks()
		{
			FlipX part;
			Brick[] bricks;
			Scene scene;

			scene = new Scene();


			part = new FlipX();
			bricks = part.Build(scene).ToArray();
			Assert.AreEqual(0, bricks.Length);
		}

		[TestMethod]
		public void ShouldGetBricks()
		{
			FlipX transform;
			Brick b;
			Brick[] bricks;

			b = new Brick(new Position(2, 1, 1), new Size(2, 1, 1), new Color());


			transform = new FlipX(new Position(0, 0, 0));
			transform.Item = b;
			bricks = transform.Build(new Scene()).ToArray();
			Assert.AreEqual(1, bricks.Length);
			Assert.AreEqual(new Position(-3, 1, 1), bricks[0].Position);
			Assert.AreEqual(new Size(2, 1, 1), bricks[0].Size);


			transform = new FlipX(new Position(2, 2, 2));
			transform.Item = b;
			bricks = transform.Build(new Scene()).ToArray();
			Assert.AreEqual(1, bricks.Length);
			Assert.AreEqual(new Position(-3 + 2, 1 + 2, 1 + 2), bricks[0].Position);
			Assert.AreEqual(new Size(2, 1, 1), bricks[0].Size);
		}

	}
}
