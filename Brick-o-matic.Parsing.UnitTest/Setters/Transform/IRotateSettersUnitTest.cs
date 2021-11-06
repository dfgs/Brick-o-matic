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
	public class IRotateSettersUnitTest
	{
		

		// IRotate Setters
		[TestMethod]
		public void ShouldParseRotateXPositionSetter()
		{
			IRotatePositionSetter setter;

			setter = Grammar.IRotatePositionSetter.Parse("Position:(1, 2,3)", ' ');
			Assert.IsNotNull(setter);
			Assert.AreEqual(new Position(1, 2, 3), setter.Value);
		}

		[TestMethod]
		public void ShouldParseRotateXCountSetter()
		{
			IRotateCountSetter setter;

			setter = Grammar.IRotateCountSetter.Parse("Count:15", ' ');
			Assert.IsNotNull(setter);
			Assert.AreEqual(15, setter.Value);
		}

		[TestMethod]
		public void ShouldParseIRotateItemSetter()
		{
			IRotateItemSetter setter;

			setter = Grammar.IRotateItemSetter.Parse("Item:Brick()", ' ');
			Assert.IsNotNull(setter);
			Assert.IsInstanceOfType(setter.Value,typeof(Brick));

		}

		

	}
}
