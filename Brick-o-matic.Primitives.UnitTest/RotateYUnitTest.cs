using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Brick_o_matic.Primitives;
using Brick_o_matic.Math;
using System.Linq;

namespace Brick_o_matic.Primitives.UnitTest
{
	[TestClass]
	public class RotateYUnitTest
	{
		[TestMethod]
		public void ShouldInstanciate()
		{
			RotateY transform;

			transform = new RotateY();
			Assert.IsNotNull(transform);
			Assert.AreEqual(new Position(), transform.Position);

			transform = new RotateY(new Position(1, 2, 3));
			Assert.IsNotNull(transform);
			Assert.AreEqual(new Position(1, 2, 3), transform.Position);
		}
		
		[TestMethod]
		public void ShouldReturnFlatBoundingBoxWhenHasNoPrimitive()
		{
			RotateY transform;
			Box box;

			transform = new RotateY(new Position(1,2,3));
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
			RotateY transform;
			Box box;
			Brick b;

			b = new Brick(new Position(1, 5, 0), new Size(4, 5, 2), new Color());

			transform = new RotateY(new Position(0, 0, 0));
			transform.Count = 1;transform.Item = b;
			box = transform.GetBoundingBox(new Scene());
			Assert.AreEqual(new Position(0, 5, -4), box.Position);
			Assert.AreEqual(new Size(2, 5, 4), box.Size);
			

			transform = new RotateY(new Position(2, 2, 2));
			transform.Count = -1; transform.Item = b;
			box = transform.GetBoundingBox(new Scene());
			Assert.AreEqual(new Position(-1 +2 , 5 +2 , 1 +2), box.Position);
			Assert.AreEqual(new Size(2, 5, 4), box.Size);
		}


		[TestMethod]
		public void ShouldGetEmptyBricks()
		{
			RotateY part;
			Brick[] bricks;
			Scene scene;

			scene = new Scene();


			part = new RotateY();
			bricks = part.Build(scene).ToArray();
			Assert.AreEqual(0, bricks.Length);
		}

		[TestMethod]
		public void ShouldGetBricks()
		{
			RotateY transform;
			Brick b;
			Brick[] bricks;

			b = new Brick(new Position(1, 5, 0), new Size(4, 5, 2), new Color());

			transform = new RotateY(new Position(0, 0, 0));
			transform.Count = 1; transform.Item = b;
			bricks = transform.Build(new Scene()).ToArray();
			Assert.AreEqual(1, bricks.Length);
			Assert.AreEqual(new Position(0, 5, -4), bricks[0].Position);
			Assert.AreEqual(new Size(2, 5, 4), bricks[0].Size);


			transform = new RotateY(new Position(2, 2, 2));
			transform.Count = -1; transform.Item = b;
			bricks = transform.Build(new Scene()).ToArray();
			Assert.AreEqual(1, bricks.Length);
			Assert.AreEqual(new Position(-1 + 2, 5 + 2, 1 + 2), bricks[0].Position);
			Assert.AreEqual(new Size(2, 5, 4), bricks[0].Size);
		}

	}
}
