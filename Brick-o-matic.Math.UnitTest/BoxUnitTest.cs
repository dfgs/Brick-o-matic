using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Brick_o_matic.Math.UnitTest
{
	[TestClass]
	public class BoxUnitTest
	{
		[TestMethod]
		public void ShouldInstanciate()
		{
			Assert.IsNotNull( new Box(new Position(0, 0, 0), new Size(1, 1, 1)));
			Assert.IsNotNull( new Box(new Position(-1, -1, -1), new Size(1, 1, 1)));
			Assert.IsNotNull(new Box(new Position(0, 0, 0)));
		}


		[TestMethod]
		public void ShouldRotateX()
		{
			Box b, rotate;

			b = new Box(new Position(5, 0, 1), new Size(5, 2, 4));
			rotate = b.RotateX(0);
			Assert.AreEqual(new Position(5, 0, 1), rotate.Position);
			Assert.AreEqual(new Size(5, 2, 4), rotate.Size);
			rotate = b.RotateX(4);
			Assert.AreEqual(new Position(5, 0, 1), rotate.Position);
			Assert.AreEqual(new Size(5, 2, 4), rotate.Size);

			b = new Box(new Position(5, 0, 1), new Size(5, 2, 4));
			rotate = b.RotateX(1);
			Assert.AreEqual(new Position(5, -5, 0), rotate.Position);
			Assert.AreEqual(new Size(5, 4, 2), rotate.Size);
			b = new Box(new Position(5, 0, 1), new Size(5, 2, 4));
			rotate = b.RotateX(5);
			Assert.AreEqual(new Position(5, -5, 0), rotate.Position);
			Assert.AreEqual(new Size(5, 4, 2), rotate.Size);

			b = new Box(new Position(5, 0, 1), new Size(5, 2, 4));
			rotate = b.RotateX(2);
			Assert.AreEqual(new Position(5, -2, -5), rotate.Position);
			Assert.AreEqual(new Size(5, 2, 4), rotate.Size);
			b = new Box(new Position(5, 0, 1), new Size(5, 2, 4));
			rotate = b.RotateX(6);
			Assert.AreEqual(new Position(5, -2, -5), rotate.Position);
			Assert.AreEqual(new Size(5, 2, 4), rotate.Size);

			b = new Box(new Position(5, 0, 1), new Size(5, 2, 4));
			rotate = b.RotateX(3);
			Assert.AreEqual(new Position(5, 1, -2), rotate.Position);
			Assert.AreEqual(new Size(5, 4, 2), rotate.Size);
			b = new Box(new Position(5, 0, 1), new Size(5, 2, 4));
			rotate = b.RotateX(7);
			Assert.AreEqual(new Position(5, 1, -2), rotate.Position);
			Assert.AreEqual(new Size(5, 4, 2), rotate.Size);//*/

			b = new Box(new Position(5, 0, 1), new Size(5, 2, 4));
			rotate = b.RotateX(-1);
			Assert.AreEqual(new Position(5, 1, -2), rotate.Position);
			Assert.AreEqual(new Size(5, 4, 2), rotate.Size);
		}

		[TestMethod]
		public void ShouldRotateY()
		{
			Box b, rotate;

			b = new Box(new Position(1, 5, 0), new Size(4, 5, 2));
			rotate = b.RotateY(0);
			Assert.AreEqual(new Position(1, 5, 0), rotate.Position);
			Assert.AreEqual(new Size(4, 5, 2), rotate.Size);
			rotate = b.RotateY(4);
			Assert.AreEqual(new Position(1, 5, 0), rotate.Position);
			Assert.AreEqual(new Size(4, 5, 2), rotate.Size);

			b = new Box(new Position(1, 5, 0), new Size(4, 5, 2));
			rotate = b.RotateY(1);
			Assert.AreEqual(new Position(0, 5, -5), rotate.Position);
			Assert.AreEqual(new Size(2, 5, 4), rotate.Size);
			b = new Box(new Position(1, 5, 0), new Size(4, 5, 2));
			rotate = b.RotateY(5);
			Assert.AreEqual(new Position(0, 5, -5), rotate.Position);
			Assert.AreEqual(new Size(2, 5, 4), rotate.Size);

			b = new Box(new Position(1, 5, 0), new Size(4, 5, 2));
			rotate = b.RotateY(2);
			Assert.AreEqual(new Position(-5, 5, -2), rotate.Position);
			Assert.AreEqual(new Size(4, 5, 2), rotate.Size);
			b = new Box(new Position(1, 5, 0), new Size(4, 5, 2));
			rotate = b.RotateY(6);
			Assert.AreEqual(new Position(-5, 5, -2), rotate.Position);
			Assert.AreEqual(new Size(4, 5, 2), rotate.Size);

			b = new Box(new Position(1, 5, 0), new Size(4, 5, 2));
			rotate = b.RotateY(3);
			Assert.AreEqual(new Position(-2, 5, 1), rotate.Position);
			Assert.AreEqual(new Size(2, 5, 4), rotate.Size);
			b = new Box(new Position(1, 5, 0), new Size(4, 5, 2));
			rotate = b.RotateY(7);
			Assert.AreEqual(new Position(-2, 5, 1), rotate.Position);
			Assert.AreEqual(new Size(2, 5, 4), rotate.Size);

			b = new Box(new Position(1, 5, 0), new Size(4, 5, 2));
			rotate = b.RotateY(-1);
			Assert.AreEqual(new Position(-2, 5, 1), rotate.Position);
			Assert.AreEqual(new Size(2, 5, 4), rotate.Size);
		}

		[TestMethod]
		public void ShouldRotateZ()
		{
			Box b, rotate;

			b = new Box(new Position(1, 0, 5), new Size(4, 2, 5));
			rotate = b.RotateZ(0);
			Assert.AreEqual(new Position(1, 0, 5), rotate.Position);
			Assert.AreEqual(new Size(4, 2, 5), rotate.Size);
			rotate = b.RotateZ(4);
			Assert.AreEqual(new Position(1, 0, 5), rotate.Position);
			Assert.AreEqual(new Size(4, 2, 5), rotate.Size);

			b = new Box(new Position(1, 0, 5), new Size(4, 2, 5));
			rotate = b.RotateZ(1);
			Assert.AreEqual(new Position(-2, 1, 5), rotate.Position);
			Assert.AreEqual(new Size(2, 4, 5), rotate.Size);
			b = new Box(new Position(1, 0, 5), new Size(4, 2, 5));
			rotate = b.RotateZ(5);
			Assert.AreEqual(new Position(-2, 1, 5), rotate.Position);
			Assert.AreEqual(new Size(2, 4, 5), rotate.Size);

			b = new Box(new Position(1, 0, 5), new Size(4, 2, 5));
			rotate = b.RotateZ(2);
			Assert.AreEqual(new Position(-5, -2, 5), rotate.Position);
			Assert.AreEqual(new Size(4, 2, 5), rotate.Size);
			b = new Box(new Position(1, 0, 5), new Size(4, 2, 5));
			rotate = b.RotateZ(6);
			Assert.AreEqual(new Position(-5, -2, 5), rotate.Position);
			Assert.AreEqual(new Size(4, 2, 5), rotate.Size);

			b = new Box(new Position(1, 0, 5), new Size(4, 2, 5));
			rotate = b.RotateZ(3);
			Assert.AreEqual(new Position(0, -5, 5), rotate.Position);
			Assert.AreEqual(new Size(2, 4, 5), rotate.Size);
			b = new Box(new Position(1, 0, 5), new Size(4, 2, 5));
			rotate = b.RotateZ(7);
			Assert.AreEqual(new Position(0, -5, 5), rotate.Position);
			Assert.AreEqual(new Size(2, 4, 5), rotate.Size);

			b = new Box(new Position(1, 0, 5), new Size(4, 2, 5));
			rotate = b.RotateZ(-1);
			Assert.AreEqual(new Position(0, -5, 5), rotate.Position);
			Assert.AreEqual(new Size(2, 4, 5), rotate.Size);
		}


	}
}
