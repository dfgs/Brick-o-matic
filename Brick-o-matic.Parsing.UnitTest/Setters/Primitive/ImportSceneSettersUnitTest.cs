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
	public class ImportSceneSettersUnitTest
	{
		




	

		// ImportScene Setters
		[TestMethod]
		public void ShouldParseImportScenePositionSetter()
		{
			ImportedScenePositionSetter setter;

			setter = Grammar.ImportedScenePositionSetter.Parse("Position:(1, 2,3)", ' ');
			Assert.IsNotNull(setter);
			Assert.AreEqual(new Position(1, 2, 3), setter.Value);
		}

		

		

	}
}
