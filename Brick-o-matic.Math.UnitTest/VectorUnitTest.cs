using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Brick_o_matic.Math.UnitTest
{
	[TestClass]
	public class VectorUnitTest
	{
		[TestMethod]
		public void ShouldInstanciate()
		{
			Vector v;

			v = new Vector(0, 0, 0);
			Assert.IsNotNull( v);
			Assert.AreEqual(0, v.X);
			Assert.AreEqual(0, v.Y);
			Assert.AreEqual(0, v.Z);

			v = new Vector(1, 2, 3);
			Assert.IsNotNull(v);
			Assert.AreEqual(1, v.X);
			Assert.AreEqual(2, v.Y);
			Assert.AreEqual(3, v.Z);
		}

		[TestMethod]
		public void ShouldEquals()
		{

			Assert.IsTrue(new Vector(1, 2, 3).Equals((object)new Vector(1, 2, 3)));
			Assert.IsTrue(new Vector(-1, 2, -3).Equals((object)new Vector(-1, 2, -3)));
			Assert.IsFalse(new Vector(1, 2, 3).Equals((object)new Vector(0, 2, 3)));
			Assert.IsFalse(new Vector(1, 2, 3).Equals((object)new Vector(1, 0, 3)));
			Assert.IsFalse(new Vector(1, 2, 3).Equals((object)new Vector(1, 2, 0)));
		}

		[TestMethod]
		public void ShouldEqualsWithIEquatable()
		{

			Assert.IsTrue(new Vector(1, 2, 3).Equals(new Vector(1, 2, 3)));
			Assert.IsTrue(new Vector(-1, 2, -3).Equals(new Vector(-1, 2, -3)));
			Assert.IsFalse(new Vector(1, 2, 3).Equals(new Vector(0, 2, 3)));
			Assert.IsFalse(new Vector(1, 2, 3).Equals(new Vector(1, 0, 3)));
			Assert.IsFalse(new Vector(1, 2, 3).Equals(new Vector(1, 2, 0)));
		}


		[TestMethod]
		public void ShouldEqualsWithOperator()
		{
			Assert.IsTrue(new Vector(1, 2, 3)==new Vector(1, 2, 3));
			Assert.IsTrue(new Vector(-1, 2, -3) == new Vector(-1, 2, -3));
			Assert.IsFalse(new Vector(1, 2, 3) == new Vector(0, 2, 3));
			Assert.IsFalse(new Vector(1, 2, 3) == new Vector(1, 0, 3));
			Assert.IsFalse(new Vector(1, 2, 3) == new Vector(1, 2, 0));
		}
		[TestMethod]
		public void ShouldNotEqualsWithOperator()
		{
			Assert.IsFalse(new Vector(1, 2, 3) != new Vector(1, 2, 3));
			Assert.IsFalse(new Vector(-1, 2, -3) != new Vector(-1, 2, -3));
			Assert.IsTrue(new Vector(1, 2, 3) != new Vector(0, 2, 3));
			Assert.IsTrue(new Vector(1, 2, 3) != new Vector(1, 0, 3));
			Assert.IsTrue(new Vector(1, 2, 3) != new Vector(1, 2, 0));
		}

		[TestMethod]
		public void ShouldAddWithOperator()
		{
			Assert.AreEqual(new Vector(3, 5, 7), new Vector(1, 2, 3) + new Vector(2, 3, 4));
		}
		[TestMethod]
		public void ShouldSubWithOperator()
		{
			Assert.AreEqual(new Vector(3, 5, 7), new Vector(5, 8, 11) - new Vector(2, 3, 4));
		}



	}
}
