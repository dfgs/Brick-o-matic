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
	public class GrammarUnitTest
	{
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
		public void ShouldParseColor()
		{
			Assert.AreEqual(new Color(1, 2, 3), Grammar.Color.Parse("(1,2,3)", ' '));
			Assert.AreEqual(new Color(255, 255, 255), Grammar.Color.Parse(" (255, 255, 255)", ' '));
		}

		[TestMethod]
		public void ShouldNotParseInvalidColor()
		{
			Assert.ThrowsException<UnexpectedCharException>(() => Grammar.Color.Parse("(256,2,3)", ' '));
			Assert.ThrowsException<UnexpectedCharException>(() => Grammar.Color.Parse("(-1,2,3)", ' '));
		}

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
		}


		// Part Setters
		[TestMethod]
		public void ShouldParsePartPositionSetter()
		{
			PartPositionSetter setter;

			setter = Grammar.PartPositionSetter.Parse("Position:(1, 2,3)", ' ');
			Assert.IsNotNull(setter);
			Assert.AreEqual(new Position(1, 2, 3), setter.Value);

		}

		// CSG Setters
		[TestMethod]
		public void ShouldParseCSGASetter()
		{
			CSGASetter setter;

			setter = Grammar.CSGASetter.Parse("A:Brick()", ' ');
			Assert.IsNotNull(setter);
			Assert.IsTrue( setter.Value is Brick);

		}
		[TestMethod]
		public void ShouldParseCSGBSetter()
		{
			CSGBSetter setter;

			setter = Grammar.CSGBSetter.Parse("B:Brick()", ' ');
			Assert.IsNotNull(setter);
			Assert.IsTrue(setter.Value is Brick);

		}


		// Primitives
		[TestMethod]
		public void ShouldParseBrick()
		{
			Brick b;

			b = Grammar.Brick.Parse("Brick()",' ') ;
			Assert.IsNotNull(b);
			Assert.AreEqual(new Position(),b.Position);

			b = Grammar.Brick.Parse("Brick(Position:(1,2,3))", ' ');
			Assert.IsNotNull(b);
			Assert.AreEqual(new Position(1, 2, 3), b.Position);

			b = Grammar.Brick.Parse("Brick(Size:(1,2,3))", ' ') ;
			Assert.IsNotNull(b);
			Assert.AreEqual(new Size(1, 2, 3), b.Size);

			b = Grammar.Brick.Parse("Brick(Size:(1,2,3) Position:(3,2,1))", ' ');
			Assert.IsNotNull(b);
			Assert.AreEqual(new Size(1, 2, 3), b.Size);
			Assert.AreEqual(new Position(3, 2, 1), b.Position);

			b = Grammar.Brick.Parse("Brick(Size:(1,2,3) Position:(3,2,1) Color:(128,127,126) )", ' ');
			Assert.IsNotNull(b);
			Assert.AreEqual(new Size(1, 2, 3), b.Size);
			Assert.AreEqual(new Position(3, 2, 1), b.Position);
			Assert.AreEqual(new Color(128, 127, 126), b.Color);
		}
		[TestMethod]
		public void ShouldParsePart()
		{
			Part p;

			p = Grammar.Part.Parse("Part()", ' ');
			Assert.IsNotNull(p);
			Assert.AreEqual(new Position(), p.Position);

			p = Grammar.Part.Parse("Part(Position:(1,2,3))", ' ');
			Assert.IsNotNull(p);
			Assert.AreEqual(new Position(1, 2, 3), p.Position);

		}

		[TestMethod]
		public void ShouldParseDifference()
		{
			ICSG p;

			p = Grammar.Difference.Parse("Difference()", ' ');
			Assert.IsNotNull(p);

			p = Grammar.Difference.Parse("Difference(A:Brick() B:Brick())", ' ');
			Assert.IsNotNull(p);
			Assert.IsNotNull(p.A);
			Assert.IsNotNull(p.B);
			Assert.IsTrue(p.A is Brick);
			Assert.IsTrue(p.B is Brick);
		}
		[TestMethod]
		public void ShouldParsePrimitives()
		{
			IPrimitive[] items;

			items = Grammar.Primitives.Parse("Part()", ' ').ToArray();
			Assert.IsNotNull(items);
			Assert.AreEqual(1, items.Length);
			Assert.IsInstanceOfType(items[0], typeof(Part));

			items = Grammar.Primitives.Parse("Part() Brick(Position:(1,2,3))", ' ').ToArray();
			Assert.IsNotNull(items);
			Assert.AreEqual(2, items.Length);
			Assert.IsInstanceOfType(items[0], typeof(Part));
			Assert.IsInstanceOfType(items[1], typeof(Brick));
		}


	}
}
