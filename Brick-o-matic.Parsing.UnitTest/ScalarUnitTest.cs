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
	public class ScalarUnitTest
	{
		[TestMethod]
		public void ShouldParseComment()
		{
			Assert.AreEqual("//Homer", Grammar.Comment.Parse("//Homer\rSecondLine", ' ','\r','\n'));
		}
		

		[TestMethod]
		public void ShouldParseName()
		{
			Assert.AreEqual("A", Grammar.Name.Parse("A", ' '));
			Assert.AreEqual("Homer", Grammar.Name.Parse("Homer", ' '));
			Assert.AreEqual("hOMEr", Grammar.Name.Parse("hOMEr", ' '));
			Assert.AreEqual("Homer1", Grammar.Name.Parse("Homer1", ' '));
			Assert.AreEqual("Homer-1", Grammar.Name.Parse("Homer-1", ' '));
			Assert.AreEqual("Homer_1", Grammar.Name.Parse("Homer_1", ' '));
			Assert.AreEqual("Homer_1A", Grammar.Name.Parse("Homer_1A", ' '));
			Assert.AreEqual("Homer_1.A", Grammar.Name.Parse("Homer_1.A", ' '));
		}


		[TestMethod]
		public void ShouldParsePosition()
		{
			Assert.AreEqual(new Position(1, 2, 3), Grammar.Position.Parse("(1,2,3)", ' '));
			Assert.AreEqual(new Position(1, -2, 3), Grammar.Position.Parse("(1,-2,3)", ' '));
			Assert.AreEqual(new Position(100, 200, -300), Grammar.Position.Parse(" (100, 200, -300)", ' '));
		}
		[TestMethod]
		public void ShouldParseSize()
		{
			Assert.AreEqual(new Size(1, 2, 3), Grammar.Size.Parse("(1,2,3)", ' '));
			Assert.AreEqual(new Size(0, 0, 0), Grammar.Size.Parse("(0,0,0)", ' '));
			Assert.AreEqual(new Size(100, 200, 300), Grammar.Size.Parse(" (100, 200, 300)", ' '));
		}

		[TestMethod]
		public void ShouldParseColorRef()
		{
			ColorRef color;

			color = Grammar.ColorRef.Parse("Red");
			Assert.IsNotNull(color);
			Assert.AreEqual("Red", color.Name);
		}

		[TestMethod]
		public void ShouldParseColor()
		{
			Assert.AreEqual(new Color(1, 2, 3), Grammar.StaticColor.Parse("(1,2,3)", ' '));
			Assert.AreEqual(new Color(255, 255, 255), Grammar.StaticColor.Parse(" (255, 255, 255)", ' '));
		}

		[TestMethod]
		public void ShouldNotParseInvalidColor()
		{
			Assert.ThrowsException<UnexpectedCharException>(() => Grammar.StaticColor.Parse("(256,2,3)", ' '));
			Assert.ThrowsException<UnexpectedCharException>(() => Grammar.StaticColor.Parse("(-1,2,3)", ' '));
		}
		[TestMethod]
		public void ShouldParseEscapedChar()
		{
			Assert.AreEqual("A", Grammar.EscapedChar.Parse(@"\A", ' '));
			Assert.AreEqual("\"", Grammar.EscapedChar.Parse("\\\"", ' '));
		}
		[TestMethod]
		public void ShouldParseChar()
		{
			Assert.AreEqual("A", Grammar.Char.Parse(@"A", ' '));
			Assert.AreEqual("\"", Grammar.EscapedChar.Parse("\\\"", ' '));
		}
		[TestMethod]
		public void ShouldParseString()
		{
			Assert.AreEqual("test", Grammar.String.Parse("\"test\"", ' '));
			Assert.AreEqual("\"test\"", Grammar.String.Parse("\"\\\"test\\\"\"", ' '));
		}

		[TestMethod]
		public void ShouldParseVariable()
		{
			Assert.AreEqual("test", Grammar.Variable.Parse("\"test\"", ' ').Value);
			Assert.AreEqual("\"test\"", Grammar.Variable.Parse("\"\\\"test\\\"\"", ' ').Value);
		}
	}
}
