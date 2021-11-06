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
	public class IFlipSettersUnitTest
	{
		

		
		// IFlip Setters
		[TestMethod]
		public void ShouldParseFlipXPositionSetter()
		{
			IFlipPositionSetter setter;

			setter = Grammar.IFlipPositionSetter.Parse("Position:(1, 2,3)", ' ');
			Assert.IsNotNull(setter);
			Assert.AreEqual(new Position(1, 2, 3), setter.Value);
		}

		
		[TestMethod]
		public void ShouldParseIFlipItemSetter()
		{
			IFlipItemSetter setter;

			setter = Grammar.IFlipItemSetter.Parse("Item:Brick()", ' ');
			Assert.IsNotNull(setter);
			Assert.IsInstanceOfType(setter.Value, typeof(Brick));

		}

	

	}
}
