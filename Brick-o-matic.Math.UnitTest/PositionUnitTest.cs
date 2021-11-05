using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Brick_o_matic.Math.UnitTest
{
	[TestClass]
	public class PositionUnitTest
	{
		[TestMethod]
		public void ShouldInstanciate()
		{
			Position v;

			v = new Position(0, 0, 0);
			Assert.IsNotNull( v);
			Assert.AreEqual(0, v.X);
			Assert.AreEqual(0, v.Y);
			Assert.AreEqual(0, v.Z);

			v = new Position(1, 2, 3);
			Assert.IsNotNull(v);
			Assert.AreEqual(1, v.X);
			Assert.AreEqual(2, v.Y);
			Assert.AreEqual(3, v.Z);
		}

		[TestMethod]
		public void ShouldEquals()
		{

			Assert.IsTrue(new Position(1, 2, 3).Equals((object)new Position(1, 2, 3)));
			Assert.IsTrue(new Position(-1, 2, -3).Equals((object)new Position(-1, 2, -3)));
			Assert.IsFalse(new Position(1, 2, 3).Equals((object)new Position(0, 2, 3)));
			Assert.IsFalse(new Position(1, 2, 3).Equals((object)new Position(1, 0, 3)));
			Assert.IsFalse(new Position(1, 2, 3).Equals((object)new Position(1, 2, 0)));
		}

		[TestMethod]
		public void ShouldEqualsWithIEquatable()
		{

			Assert.IsTrue(new Position(1, 2, 3).Equals(new Position(1, 2, 3)));
			Assert.IsTrue(new Position(-1, 2, -3).Equals(new Position(-1, 2, -3)));
			Assert.IsFalse(new Position(1, 2, 3).Equals(new Position(0, 2, 3)));
			Assert.IsFalse(new Position(1, 2, 3).Equals(new Position(1, 0, 3)));
			Assert.IsFalse(new Position(1, 2, 3).Equals(new Position(1, 2, 0)));
		}


		[TestMethod]
		public void ShouldEqualsWithOperator()
		{
			Assert.IsTrue(new Position(1, 2, 3)==new Position(1, 2, 3));
			Assert.IsTrue(new Position(-1, 2, -3) == new Position(-1, 2, -3));
			Assert.IsFalse(new Position(1, 2, 3) == new Position(0, 2, 3));
			Assert.IsFalse(new Position(1, 2, 3) == new Position(1, 0, 3));
			Assert.IsFalse(new Position(1, 2, 3) == new Position(1, 2, 0));
		}
		[TestMethod]
		public void ShouldNotEqualsWithOperator()
		{
			Assert.IsFalse(new Position(1, 2, 3) != new Position(1, 2, 3));
			Assert.IsFalse(new Position(-1, 2, -3) != new Position(-1, 2, -3));
			Assert.IsTrue(new Position(1, 2, 3) != new Position(0, 2, 3));
			Assert.IsTrue(new Position(1, 2, 3) != new Position(1, 0, 3));
			Assert.IsTrue(new Position(1, 2, 3) != new Position(1, 2, 0));
		}

		[TestMethod]
		public void ShouldAddWithOperator()
		{
			Assert.AreEqual(new Position(3, 5, 7), new Position(1, 2, 3) + new Position(2, 3, 4));
		}
		[TestMethod]
		public void ShouldSubWithOperator()
		{
			Assert.AreEqual(new Position(3, 5, 7), new Position(5, 8, 11) - new Position(2, 3, 4));
		}

		[TestMethod]
		public void ShouldAddSizeWithOperator()
		{
			Assert.AreEqual(new Position(3, 5, 7), new Position(1, 2, 3) + new Size(2, 3, 4));
		}
		[TestMethod]
		public void ShouldSubSizeWithOperator()
		{
			Assert.AreEqual(new Position(3, 5, 7), new Position(5, 8, 11) - new Size(2, 3, 4));
		}


		[TestMethod]
		public void ShouldRotateX()
		{
			Assert.AreEqual(new Position(2, 1, 0), new Position(2, 1, 0).RotateX(0));
			Assert.AreEqual(new Position(2, 1, 0), new Position(2, 1, 0).RotateX(4));

			Assert.AreEqual(new Position(2, 0, 1), new Position(2, 1, 0).RotateX(1));
			Assert.AreEqual(new Position(2, 0, 1), new Position(2, 1, 0).RotateX(5));

			Assert.AreEqual(new Position(2, -1, 0), new Position(2, 0, 1).RotateX(1));
			Assert.AreEqual(new Position(2, -1, 0), new Position(2, 0, 1).RotateX(5));

			Assert.AreEqual(new Position(2, 0, -1), new Position(2, -1, 0).RotateX(1));
			Assert.AreEqual(new Position(2, 0, -1), new Position(2, -1, 0).RotateX(5));

			Assert.AreEqual(new Position(2, 1, 0), new Position(2, 0, -1).RotateX(1));
			Assert.AreEqual(new Position(2, 1, 0), new Position(2, 0, -1).RotateX(5));

			Assert.AreEqual(new Position(2, -1, 0), new Position(2, 1, 0).RotateX(2));
			Assert.AreEqual(new Position(2, -1, 0), new Position(2, 1, 0).RotateX(-2));

			Assert.AreEqual(new Position(2, 0, 1), new Position(2, -1, 0).RotateX(-1));
			Assert.AreEqual(new Position(2, 0, 1), new Position(2, -1, 0).RotateX(-5));


			Assert.AreEqual(new Position(2, -1, 3), new Position(2, 3, 1).RotateX(1)); ;
			Assert.AreEqual(new Position(2, -1, 3), new Position(2, 3, 1).RotateX(5));
		}

		[TestMethod]
		public void ShouldRotateY()
		{
			Assert.AreEqual(new Position(1, 2, 0), new Position(1, 2, 0).RotateY(0));
			Assert.AreEqual(new Position(1, 2, 0), new Position(1, 2, 0).RotateY(4));

			Assert.AreEqual(new Position(0, 2, -1), new Position(1, 2, 0).RotateY(1));
			Assert.AreEqual(new Position(0, 2, -1), new Position(1, 2, 0).RotateY(5));

			Assert.AreEqual(new Position(-1, 2, 0), new Position(0, 2, -1).RotateY(1));
			Assert.AreEqual(new Position(-1, 2, 0), new Position(0, 2, -1).RotateY(5));

			Assert.AreEqual(new Position(0, 2, 1), new Position(-1, 2, 0).RotateY(1));
			Assert.AreEqual(new Position(0, 2, 1), new Position(-1, 2, 0).RotateY(5));

			Assert.AreEqual(new Position(1, 2, 0), new Position(0, 2, 1).RotateY(1));
			Assert.AreEqual(new Position(1, 2, 0), new Position(0, 2, 1).RotateY(5));

			Assert.AreEqual(new Position(-1, 2, 0), new Position(1, 2, 0).RotateY(2));
			Assert.AreEqual(new Position(-1, 2, 0), new Position(1, 2, 0).RotateY(-2));

			Assert.AreEqual(new Position(0, 2, 1), new Position(1, 2, 0).RotateY(-1));
			Assert.AreEqual(new Position(0, 2, 1), new Position(1, 2, 0).RotateY(-5));


			Assert.AreEqual(new Position(1, 2, -3), new Position(3, 2, 1).RotateY(1)); ;
			Assert.AreEqual(new Position(1, 2, -3), new Position(3, 2, 1).RotateY(5));
		}

		[TestMethod]
		public void ShouldRotateZ()
		{
			Assert.AreEqual(new Position(1, 0, 2), new Position(1, 0, 2).RotateZ(0));
			Assert.AreEqual(new Position(1, 0, 2), new Position(1, 0, 2).RotateZ(4));

			Assert.AreEqual(new Position(0, 1, 2), new Position(1, 0, 2).RotateZ(1));
			Assert.AreEqual(new Position(0, 1, 2), new Position(1, 0, 2).RotateZ(5));

			Assert.AreEqual(new Position(-1, 0, 2), new Position(0, 1, 2).RotateZ(1));
			Assert.AreEqual(new Position(-1, 0, 2), new Position(0, 1, 2).RotateZ(5));

			Assert.AreEqual(new Position(0, -1, 2), new Position(-1, 0, 2).RotateZ(1));
			Assert.AreEqual(new Position(0, -1, 2), new Position(-1, 0, 2).RotateZ(5));

			Assert.AreEqual(new Position(1, 0, 2), new Position(0, -1, 2).RotateZ(1));
			Assert.AreEqual(new Position(1, 0, 2), new Position(0, -1, 2).RotateZ(5));

			Assert.AreEqual(new Position(-1,0, 2), new Position(1, 0, 2).RotateZ(2));
			Assert.AreEqual(new Position(-1, 0, 2), new Position(1,0, 2).RotateZ(-2));

			Assert.AreEqual(new Position(0, -1, 2), new Position(1, 0, 2).RotateZ(-1));
			Assert.AreEqual(new Position(0, -1, 2), new Position(1, 0, 2).RotateZ(-5));


			Assert.AreEqual(new Position(-1, 3, 2), new Position(3, 1, 2).RotateZ(1)); ;
			Assert.AreEqual(new Position(-1, 3, 2), new Position(3, 1, 2).RotateZ(5));
		}
	}
}
