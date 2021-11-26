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
	public class WhenSettersUnitTest
	{
		


		// When Setters
		[TestMethod]
		public void ShouldParseWhenPositionSetter()
		{
			WhenValueSetter setter;

			setter = Grammar.WhenValueSetter.Parse("Value:\"test\"", ' ');
			Assert.IsNotNull(setter);
			Assert.AreEqual("test", setter.Value);

		}
		[TestMethod]
		public void ShouldParseWhenReturnSetter()
		{
			WhenReturnSetter setter;

			setter = Grammar.WhenReturnSetter.Parse("Return: Brick()", ' ');
			Assert.IsNotNull(setter);
			Assert.IsInstanceOfType(setter.Value,typeof(Brick));

		}


	

	}
}
