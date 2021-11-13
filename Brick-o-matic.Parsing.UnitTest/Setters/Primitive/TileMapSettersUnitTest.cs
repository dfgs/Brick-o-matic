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
		public void ShouldParseTileMapTileSizeSetter()
		{
			TileMapTileSizeSetter setter;

			setter = Grammar.TileMapTileSizeSetter.Parse("TileSize:(123,456,789))", ' ');
			Assert.IsNotNull(setter);
			Assert.AreEqual(new Size(123,456,789), setter.Value);

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
