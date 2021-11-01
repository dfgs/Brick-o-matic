using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Brick_o_matic.Math.UnitTest
{
	[TestClass]
	public class ColorUnitTest
	{
		[TestMethod]
		public void ShouldInstanciate()
		{
			Color v;

			v = new Color(0, 0, 0);
			Assert.IsNotNull( v);
			Assert.AreEqual(0, v.R);
			Assert.AreEqual(0, v.G);
			Assert.AreEqual(0, v.B);

			v = new Color(1, 2, 3);
			Assert.IsNotNull(v);
			Assert.AreEqual(1, v.R);
			Assert.AreEqual(2, v.G);
			Assert.AreEqual(3, v.B);
		}

		[TestMethod]
		public void ShouldEquals()
		{

			Assert.IsTrue(new Color(1, 2, 3).Equals((object)new Color(1, 2, 3)));
			Assert.IsFalse(new Color(1, 2, 3).Equals((object)new Color(0, 2, 3)));
			Assert.IsFalse(new Color(1, 2, 3).Equals((object)new Color(1, 0, 3)));
			Assert.IsFalse(new Color(1, 2, 3).Equals((object)new Color(1, 2, 0)));
		}

		[TestMethod]
		public void ShouldEqualsWithIEquatable()
		{

			Assert.IsTrue(new Color(1, 2, 3).Equals(new Color(1, 2, 3)));
			Assert.IsFalse(new Color(1, 2, 3).Equals(new Color(0, 2, 3)));
			Assert.IsFalse(new Color(1, 2, 3).Equals(new Color(1, 0, 3)));
			Assert.IsFalse(new Color(1, 2, 3).Equals(new Color(1, 2, 0)));
		}


		[TestMethod]
		public void ShouldEqualsWithOperator()
		{
			Assert.IsTrue(new Color(1, 2, 3)==new Color(1, 2, 3));
			Assert.IsFalse(new Color(1, 2, 3) == new Color(0, 2, 3));
			Assert.IsFalse(new Color(1, 2, 3) == new Color(1, 0, 3));
			Assert.IsFalse(new Color(1, 2, 3) == new Color(1, 2, 0));
		}
		[TestMethod]
		public void ShouldNotEqualsWithOperator()
		{
			Assert.IsFalse(new Color(1, 2, 3) != new Color(1, 2, 3));
			Assert.IsTrue(new Color(1, 2, 3) != new Color(0, 2, 3));
			Assert.IsTrue(new Color(1, 2, 3) != new Color(1, 0, 3));
			Assert.IsTrue(new Color(1, 2, 3) != new Color(1, 2, 0));
		}

	



	}
}
