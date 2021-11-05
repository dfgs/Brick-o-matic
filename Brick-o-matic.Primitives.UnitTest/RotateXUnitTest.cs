﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Brick_o_matic.Primitives;
using Brick_o_matic.Math;
using System.Linq;

namespace RotateX_o_matic.Primitives.UnitTest
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
			transform.Count = 1;transform.Primitive = b;
			box = transform.GetBoundingBox(new Scene());
			Assert.AreEqual(new Position(5, -5, 0), box.Position);
			Assert.AreEqual(new Size(5, 4, 2), box.Size);
			

			transform = new RotateX(new Position(2, 2, 2));
			transform.Count = -1; transform.Primitive = b;
			box = transform.GetBoundingBox(new Scene());
			Assert.AreEqual(new Position(5 +2 , 1 +2 , -2 +2), box.Position);
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
			transform.Count = 1; transform.Primitive = b;
			bricks = transform.Build(new Scene()).ToArray();
			Assert.AreEqual(1, bricks.Length);
			Assert.AreEqual(new Position(5, -5, 0), bricks[0].Position);
			Assert.AreEqual(new Size(5, 4, 2), bricks[0].Size);


			transform = new RotateX(new Position(2, 2, 2));
			transform.Count = -1; transform.Primitive = b;
			bricks = transform.Build(new Scene()).ToArray();
			Assert.AreEqual(1, bricks.Length);
			Assert.AreEqual(new Position(5 + 2, 1 + 2, -2 + 2), bricks[0].Position);
			Assert.AreEqual(new Size(5, 4, 2), bricks[0].Size);
		}

	}
}