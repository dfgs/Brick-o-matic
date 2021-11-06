using Brick_o_matic.Math;
using Brick_o_matic.Parsing.Setters;
using Brick_o_matic.Primitives;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParserLib;
using System;
using System.Linq;

namespace Brick_o_matic.Parsing.UnitTest
{
	[TestClass]
	public class BrickSettersUnitTest
	{
		

		

		// Brick Setters
		[TestMethod]
		public void ShouldParseBrickPositionSetter()
		{
			BrickPositionSetter setter;

			setter = Grammar.BrickPositionSetter.Parse("Position:(1, 2,3)", ' ') ;
			Assert.IsNotNull(setter);
			Assert.AreEqual(new Position(1, 2, 3), setter.Value);
		}

		[TestMethod]
		public void ShouldParseBrickSizeSetter()
		{
			BrickSizeSetter setter;

			setter = Grammar.BrickSizeSetter.Parse("Size:(1, 2,3)", ' ');
			Assert.IsNotNull(setter);
			Assert.AreEqual(new Size(1, 2, 3), setter.Value);
		}

		[TestMethod]
		public void ShouldParseBrickColorSetter()
		{
			BrickColorSetter setter;

			setter = Grammar.BrickColorSetter.Parse("Color:(1, 2,3)", ' ');
			Assert.IsNotNull(setter);
			Assert.AreEqual(new Color(1, 2, 3), setter.Value);

			setter = Grammar.BrickColorSetter.Parse("Color:(128,127,126)", ' ');
			Assert.IsNotNull(setter);
			Assert.AreEqual(new Color(128, 127, 126), setter.Value);

			setter = Grammar.BrickColorSetter.Parse("Color:Red", ' ');
			Assert.IsNotNull(setter);
		}


		
	}
}
