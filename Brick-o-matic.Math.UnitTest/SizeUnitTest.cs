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



	}
}
