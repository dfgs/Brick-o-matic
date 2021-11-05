using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Brick_o_matic.Math.UnitTest
{
	[TestClass]
	public class SizeUnitTest
	{
		[TestMethod]
		public void ShouldInstanciate()
		{
			Size v;

			v = new Size(0, 0, 0);
			Assert.IsNotNull( v);
			Assert.AreEqual(0, v.X);
			Assert.AreEqual(0, v.Y);
			Assert.AreEqual(0, v.Z);

			v = new Size(1, 2, 3);
			Assert.IsNotNull(v);
			Assert.AreEqual(1, v.X);
			Assert.AreEqual(2, v.Y);
			Assert.AreEqual(3, v.Z);
		}
		[TestMethod]
		public void ShouldNotInstanciate()
		{
			Assert.ThrowsException<ArgumentException>(() => new Size(-1, 0, 0));
			Assert.ThrowsException<ArgumentException>(() => new Size(0, -1, 0));
			Assert.ThrowsException<ArgumentException>(() => new Size(0, 0, -1));
		}

		[TestMethod]
		public void ShouldEquals()
		{

			Assert.IsTrue(new Size(1, 2, 3).Equals((object)new Size(1, 2, 3)));
			Assert.IsFalse(new Size(1, 2, 3).Equals((object)new Size(0, 2, 3)));
			Assert.IsFalse(new Size(1, 2, 3).Equals((object)new Size(1, 0, 3)));
			Assert.IsFalse(new Size(1, 2, 3).Equals((object)new Size(1, 2, 0)));
		}

		[TestMethod]
		public void ShouldEqualsWithIEquatable()
		{

			Assert.IsTrue(new Size(1, 2, 3).Equals(new Size(1, 2, 3)));
			Assert.IsFalse(new Size(1, 2, 3).Equals(new Size(0, 2, 3)));
			Assert.IsFalse(new Size(1, 2, 3).Equals(new Size(1, 0, 3)));
			Assert.IsFalse(new Size(1, 2, 3).Equals(new Size(1, 2, 0)));
		}


		[TestMethod]
		public void ShouldEqualsWithOperator()
		{
			Assert.IsTrue(new Size(1, 2, 3)==new Size(1, 2, 3));
			Assert.IsFalse(new Size(1, 2, 3) == new Size(0, 2, 3));
			Assert.IsFalse(new Size(1, 2, 3) == new Size(1, 0, 3));
			Assert.IsFalse(new Size(1, 2, 3) == new Size(1, 2, 0));
		}
		[TestMethod]
		public void ShouldNotEqualsWithOperator()
		{
			Assert.IsFalse(new Size(1, 2, 3) != new Size(1, 2, 3));
			Assert.IsTrue(new Size(1, 2, 3) != new Size(0, 2, 3));
			Assert.IsTrue(new Size(1, 2, 3) != new Size(1, 0, 3));
			Assert.IsTrue(new Size(1, 2, 3) != new Size(1, 2, 0));
		}

		[TestMethod]
		public void ShouldTestIfSizeIfFlat()
		{
			Size v;

			v = new Size(0, 1, 1);
			Assert.IsTrue(v.IsFlat);

			v = new Size(1, 0, 1);
			Assert.IsTrue(v.IsFlat);

			v = new Size(1, 1, 0);
			Assert.IsTrue(v.IsFlat);

			v = new Size(1, 1, 1);
			Assert.IsFalse(v.IsFlat);
		}
		[TestMethod]
		public void ShouldRotateX()
		{
			Assert.AreEqual(new Size(2, 0, 1), new Size(2, 0, 1).RotateX(0));
			Assert.AreEqual(new Size(2, 0, 1), new Size(2, 0, 1).RotateX(4));

			Assert.AreEqual(new Size(2, 1, 0), new Size(2, 0, 1).RotateX(1));
			Assert.AreEqual(new Size(2, 1, 0), new Size(2, 0, 1).RotateX(5));

			Assert.AreEqual(new Size(2, 0, 1), new Size(2, 1, 0).RotateX(1));
			Assert.AreEqual(new Size(2, 0, 1), new Size(2, 1, 0).RotateX(5));

			Assert.AreEqual(new Size(2, 1, 0), new Size(2, 0, 1).RotateX(1));
			Assert.AreEqual(new Size(2, 1, 0), new Size(2, 0, 1).RotateX(5));

			Assert.AreEqual(new Size(2, 0, 1), new Size(2, 1, 0).RotateX(1));
			Assert.AreEqual(new Size(2, 0, 1), new Size(2, 1, 0).RotateX(5));

			Assert.AreEqual(new Size(2, 0, 1), new Size(2, 0, 1).RotateX(2));
			Assert.AreEqual(new Size(2, 1, 0), new Size(2, 1, 0).RotateX(2));

			Assert.AreEqual(new Size(2, 1, 0), new Size(2, 0, 1).RotateX(1));
			Assert.AreEqual(new Size(2, 1, 0), new Size(2, 0, 1).RotateX(-5));
		}

		[TestMethod]
		public void ShouldRotateY()
		{
			Assert.AreEqual(new Size(1, 2, 0), new Size(1, 2, 0).RotateY(0));
			Assert.AreEqual(new Size(1, 2, 0), new Size(1, 2, 0).RotateY(4));

			Assert.AreEqual(new Size(0, 2, 1), new Size(1, 2, 0).RotateY(1));
			Assert.AreEqual(new Size(0, 2, 1), new Size(1, 2, 0).RotateY(5));

			Assert.AreEqual(new Size(1, 2, 0), new Size(0, 2, 1).RotateY(1));
			Assert.AreEqual(new Size(1, 2, 0), new Size(0, 2, 1).RotateY(5));

			Assert.AreEqual(new Size(0, 2, 1), new Size(1, 2, 0).RotateY(1));
			Assert.AreEqual(new Size(0, 2, 1), new Size(1, 2, 0).RotateY(5));

			Assert.AreEqual(new Size(1, 2, 0), new Size(0, 2, 1).RotateY(1));
			Assert.AreEqual(new Size(1, 2, 0), new Size(0, 2, 1).RotateY(5));

			Assert.AreEqual(new Size(1, 2, 0), new Size(1, 2, 0).RotateY(2));
			Assert.AreEqual(new Size(0, 2, 1), new Size(0, 2, 1).RotateY(2));

			Assert.AreEqual(new Size(0, 2, 1), new Size(1, 2, 0).RotateY(1));
			Assert.AreEqual(new Size(0, 2, 1), new Size(1, 2, 0).RotateY(-5));
		}

		[TestMethod]
		public void ShouldRotateZ()
		{
			Assert.AreEqual(new Size(1, 0, 2), new Size(1, 0, 2).RotateZ(0));
			Assert.AreEqual(new Size(1, 0, 2), new Size(1, 0, 2).RotateZ(4));

			Assert.AreEqual(new Size(0, 1, 2), new Size(1, 0, 2).RotateZ(1));
			Assert.AreEqual(new Size(0, 1, 2), new Size(1, 0, 2).RotateZ(5));

			Assert.AreEqual(new Size(1, 0, 2), new Size(0, 1, 2).RotateZ(1));
			Assert.AreEqual(new Size(1, 0, 2), new Size(0, 1, 2).RotateZ(5));

			Assert.AreEqual(new Size(0, 1, 2), new Size(1, 0, 2).RotateZ(1));
			Assert.AreEqual(new Size(0, 1, 2), new Size(1, 0, 2).RotateZ(5));

			Assert.AreEqual(new Size(1, 0, 2), new Size(0, 1, 2).RotateZ(1));
			Assert.AreEqual(new Size(1, 0, 2), new Size(0, 1, 2).RotateZ(5));

			Assert.AreEqual(new Size(1, 0, 2), new Size(1, 0, 2).RotateZ(2));
			Assert.AreEqual(new Size(0, 1, 2), new Size(0, 1, 2).RotateZ(2));

			Assert.AreEqual(new Size(0, 1, 2), new Size(1, 0, 2).RotateZ(1));
			Assert.AreEqual(new Size(0, 1, 2), new Size(1, 0, 2).RotateZ(-5));
		}


	}
}
