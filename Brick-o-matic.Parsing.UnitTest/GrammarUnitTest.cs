using Brick_o_matic.Math;
using Brick_o_matic.Parsing.Setters;
using Brick_o_matic.Primitives;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Brick_o_matic.Parsing.UnitTest
{
	[TestClass]
	public class GrammarUnitTest
	{
		[TestMethod]
		public void ShouldParseVector()
		{
			Assert.AreEqual(new Vector(1, 2, 3), Grammar.Vector.Parse("(1,2,3)"));
			Assert.AreEqual(new Vector(1, -2, 3), Grammar.Vector.Parse("(1,-2,3)"));
			Assert.AreEqual(new Vector(100, 200, -300), Grammar.Vector.Parse("(100,200,-300)"));
		}

		[TestMethod]
		public void ShouldPositionSetter()
		{
			PositionSetter setter;

			setter = Grammar.PositionSetter.Parse("Position:(1,2,3)") as PositionSetter;
			Assert.IsNotNull(setter);
			Assert.AreEqual(new Vector(1, 2, 3), setter.Value);

		}
		[TestMethod]
		public void ShouldParseBrick()
		{
			Brick b;

			b = Grammar.Brick.Parse("Brick()") as Brick;
			Assert.IsNotNull(b);
			Assert.AreEqual(new Vector(),b.Position);

			b = Grammar.Brick.Parse("Brick(Position:(1,2,3))") as Brick;
			Assert.IsNotNull(b);
			Assert.AreEqual(new Vector(1,2,3), b.Position);
		}



	}
}
