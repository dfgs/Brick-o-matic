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
	public class TileMapSettersUnitTest
	{
		


		// TileMap Setters
		[TestMethod]
		public void ShouldParseTileMapPositionSetter()
		{
			TileMapPositionSetter setter;

			setter = Grammar.TileMapPositionSetter.Parse("Position:(1, 2,3)", ' ');
			Assert.IsNotNull(setter);
			Assert.AreEqual(new Position(1, 2, 3), setter.Value);

		}
		[TestMethod]
		public void ShouldParseTileMapTileSizeXSetter()
		{
			TileMapTileSizeXSetter setter;

			setter = Grammar.TileMapTileSizeXSetter.Parse("TileSizeX:123)", ' ');
			Assert.IsNotNull(setter);
			Assert.AreEqual(123, setter.Value);

		}
		[TestMethod]
		public void ShouldParseTileMapTileSizeYSetter()
		{
			TileMapTileSizeYSetter setter;

			setter = Grammar.TileMapTileSizeYSetter.Parse("TileSizeY:123)", ' ');
			Assert.IsNotNull(setter);
			Assert.AreEqual(123, setter.Value);

		}
		[TestMethod]
		public void ShouldParseTileMapTileSizeZSetter()
		{
			TileMapTileSizeZSetter setter;

			setter = Grammar.TileMapTileSizeZSetter.Parse("TileSizeZ:123)", ' ');
			Assert.IsNotNull(setter);
			Assert.AreEqual(123, setter.Value);

		}

		[TestMethod]
		public void ShouldParseTileMapItemsSetter()
		{
			TileMapItemsSetter setter;

			setter = Grammar.TileMapItemsSetter.Parse("Items: Brick() Part()", ' ');
			Assert.IsNotNull(setter);
			Assert.AreEqual(2,setter.Value.Count());

		}


	

	}
}
