﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Brick_o_matic.Primitives;
using Brick_o_matic.Math;
using System.Linq;

namespace PrimitiveRef_o_matic.Primitives.UnitTest
{
	[TestClass]
	public class PrimitiveRefUnitTest
	{
		
		

		[TestMethod]
		public void ShouldReturnBoudingBox()
		{
			Scene scene;
			PrimitiveRef primitive;
			Box box;
			Brick b;

			scene = new Scene();
			b = new Brick(new Position(-1, -2, -3), new Size(1, 1, 1));
			scene.AddResource("b",b);

			primitive = new PrimitiveRef(new Position(0, 0, 0));
			primitive.Name = "b";

			box = primitive.GetBoundingBox(scene);
			Assert.AreEqual(new Position(-1,-2,-3), box.Position);
			Assert.AreEqual(new Size(1, 1, 1), box.Size);


			primitive = new PrimitiveRef(new Position(1, 2, 3));
			primitive.Name = "b";

			box = primitive.GetBoundingBox(scene);
			Assert.AreEqual(new Position(0, 0, 0), box.Position);
			Assert.AreEqual(new Size(1, 1, 1), box.Size);

		}
		


		[TestMethod]
		public void ShouldGetBricks()
		{
			Scene scene;
			PrimitiveRef primitive;
			Brick[] bricks;
			Brick b;

			scene = new Scene();
			b = new Brick(new Position(-1, -2, -3), new Size(1, 1, 1));
			scene.AddResource("b", b);

			primitive = new PrimitiveRef(new Position(0, 0, 0));
			primitive.Name = "b";

			bricks = primitive.Build(scene).ToArray();
			Assert.AreEqual(1, bricks.Length);
		}

		
	}
}