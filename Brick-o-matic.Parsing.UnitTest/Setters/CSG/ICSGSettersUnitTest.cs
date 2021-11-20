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
	public class ICSGSettersUnitTest
	{
		

		
		// ICSG Setters
		[TestMethod]
		public void ShouldParseCSGPositionSetter()
		{
			ICSGPositionSetter setter;

			setter = Grammar.ICSGPositionSetter.Parse("Position:(1, 2,3)", ' ');
			Assert.IsNotNull(setter);
			Assert.AreEqual(new Position(1, 2, 3), setter.Value);
		}


		[TestMethod]
		public void ShouldParseICSGItemASetter()
		{
			ICSGItemASetter setter;

			setter = Grammar.ICSGItemASetter.Parse("ItemA:Brick()", ' ');
			Assert.IsNotNull(setter);
			Assert.IsInstanceOfType(setter.Value, typeof(Brick));

		}
		[TestMethod]
		public void ShouldParseICSGItemBSetter()
		{
			ICSGItemBSetter setter;

			setter = Grammar.ICSGItemBSetter.Parse("ItemB:Brick()", ' ');
			Assert.IsNotNull(setter);
			Assert.IsInstanceOfType(setter.Value, typeof(Brick));

		}



	}
}
