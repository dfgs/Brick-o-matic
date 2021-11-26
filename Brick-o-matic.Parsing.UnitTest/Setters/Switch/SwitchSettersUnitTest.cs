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
	public class SwitchSettersUnitTest
	{
		


		// Switch Setters
		[TestMethod]
		public void ShouldParseSwitchPositionSetter()
		{
			SwitchPositionSetter setter;

			setter = Grammar.SwitchPositionSetter.Parse("Position:(1, 2,3)", ' ');
			Assert.IsNotNull(setter);
			Assert.AreEqual(new Position(1, 2, 3), setter.Value);

		}
		[TestMethod]
		public void ShouldParseSwitchVariableSetter()
		{
			SwitchVariableSetter setter;

			setter = Grammar.SwitchVariableSetter.Parse("Variable:myVar", ' ');
			Assert.IsNotNull(setter);
			Assert.AreEqual("myVar", setter.Value);

			setter = Grammar.SwitchVariableSetter.Parse("Variable: WallStyle", ' ');
			Assert.IsNotNull(setter);
			Assert.AreEqual("WallStyle", setter.Value);

		}

		[TestMethod]
		public void ShouldParseSwitchItemsSetter()
		{
			SwitchItemsSetter setter;

			setter = Grammar.SwitchItemsSetter.Parse("Items: When(Value:\"1\" Return:Brick()) When(Value:\"2\" Return:Brick())", ' ');
			Assert.IsNotNull(setter);
			Assert.AreEqual(2, setter.Value.Count());

			setter = Grammar.SwitchItemsSetter.Parse("Items: When(Value: \"Style1\" Return: Primitive(Name: Door Position: (-7, 0, 10))) When(Value: \"Style2\" Return: Brick(Position: (-7, 0, 10) Color: (255, 0, 0)))", ' ');
			Assert.IsNotNull(setter);
			Assert.AreEqual(2, setter.Value.Count());

		}




	}
}
