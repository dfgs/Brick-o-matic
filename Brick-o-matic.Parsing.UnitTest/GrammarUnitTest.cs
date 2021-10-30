using Brick_o_matic.Math;
using Brick_o_matic.Parsing.Setters;
using Brick_o_matic.Primitives;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Brick_o_matic.Parsing.UnitTest
{
	[TestClass]
	public class GrammarUnitTest
	{
		[TestMethod]
		public void ShouldParseVector()
		{
			Assert.AreEqual(new Vector(1, 2, 3), Grammar.Vector.Parse("(1,2,3)", ' '));
			Assert.AreEqual(new Vector(1, -2, 3), Grammar.Vector.Parse("(1,-2,3)", ' '));
			Assert.AreEqual(new Vector(100, 200, -300), Grammar.Vector.Parse(" (100, 200, -300)", ' '));
		}

		// Brick Setters
		[TestMethod]
		public void ShouldParseBrickPositionSetter()
		{
			BrickPositionSetter setter;

			setter = Grammar.BrickPositionSetter.Parse("Position:(1, 2,3)", ' ') ;
			Assert.IsNotNull(setter);
			Assert.AreEqual(new Vector(1, 2, 3), setter.Value);

		}

		[TestMethod]
		public void ShouldParseBrickSizeSetter()
		{
			BrickSizeSetter setter;

			setter = Grammar.BrickSizeSetter.Parse("Size:(1, 2,3)", ' ') ;
			Assert.IsNotNull(setter);
			Assert.AreEqual(new Vector(1, 2, 3), setter.Value);

		}

		// Part Setters
		[TestMethod]
		public void ShouldParsePartPositionSetter()
		{
			PartPositionSetter setter;

			setter = Grammar.PartPositionSetter.Parse("Position:(1, 2,3)", ' ');
			Assert.IsNotNull(setter);
			Assert.AreEqual(new Vector(1, 2, 3), setter.Value);

		}




		// Primitives
		[TestMethod]
		public void ShouldParseBrick()
		{
			Brick b;

			b = Grammar.Brick.Parse("Brick()",' ') ;
			Assert.IsNotNull(b);
			Assert.AreEqual(new Vector(),b.Position);

			b = Grammar.Brick.Parse("Brick(Position:(1,2,3))", ' ');
			Assert.IsNotNull(b);
			Assert.AreEqual(new Vector(1, 2, 3), b.Position);

			b = Grammar.Brick.Parse("Brick(Size:(1,2,3))", ' ') ;
			Assert.IsNotNull(b);
			Assert.AreEqual(new Vector(1, 2, 3), b.Size);

			b = Grammar.Brick.Parse("Brick(Size:(1,2,3) Position:(3,2,1))", ' ');
			Assert.IsNotNull(b);
			Assert.AreEqual(new Vector(1, 2, 3), b.Size);
			Assert.AreEqual(new Vector(3, 2, 1), b.Position);
		}
		[TestMethod]
		public void ShouldParsePart()
		{
			Part p;

			p = Grammar.Part.Parse("Part()", ' ');
			Assert.IsNotNull(p);
			Assert.AreEqual(new Vector(), p.Position);

			p = Grammar.Part.Parse("Part(Position:(1,2,3))", ' ');
			Assert.IsNotNull(p);
			Assert.AreEqual(new Vector(1, 2, 3), p.Position);

			/*b = Grammar.Brick.Parse("Brick(Size:(1,2,3))", ' ');
			Assert.IsNotNull(b);
			Assert.AreEqual(new Vector(1, 2, 3), b.Size);

			b = Grammar.Brick.Parse("Brick(Size:(1,2,3) Position:(3,2,1))", ' ');
			Assert.IsNotNull(b);
			Assert.AreEqual(new Vector(1, 2, 3), b.Size);
			Assert.AreEqual(new Vector(3, 2, 1), b.Position);*/
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
