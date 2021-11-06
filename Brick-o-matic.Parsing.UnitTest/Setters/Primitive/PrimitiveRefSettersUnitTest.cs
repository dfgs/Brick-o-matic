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
	public class PrimitiveRefSettersUnitTest
	{
		




		// PrimitiveRef Setters
		[TestMethod]
		public void ShouldParsePrimitiveRefPositionSetter()
		{
			PrimitiveRefPositionSetter setter;

			setter = Grammar.PrimitiveRefPositionSetter.Parse("Position:(1, 2,3)", ' ');
			Assert.IsNotNull(setter);
			Assert.AreEqual(new Position(1, 2, 3), setter.Value);
		}

		[TestMethod]
		public void ShouldParsePrimitiveRefNameSetter()
		{
			PrimitiveRefNameSetter setter;

			setter = Grammar.PrimitiveRefNameSetter.Parse("Name: Homer123", ' ');
			Assert.IsNotNull(setter);
			Assert.AreEqual("Homer123", setter.Value);
		}
		[TestMethod]
		public void ShouldParsePrimitiveRefResourceSetter()
		{
			PrimitiveRefResourcesSetter setter;

			setter = Grammar.PrimitiveRefResourcesSetter.Parse("Resources:ResourceName=Brick() ResourceName2=Part()", ' ');
			Assert.IsNotNull(setter);
			Assert.AreEqual(2, setter.Value.Count());
			Assert.AreEqual("ResourceName", setter.Value.First().Name);
		}

	}
}
