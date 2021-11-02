using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Brick_o_matic.Math.UnitTest
{
	[TestClass]
	public class BoxUnitTest
	{
		[TestMethod]
		public void ShouldInstanciate()
		{
			Assert.IsNotNull( new Box(new Position(0, 0, 0), new Size(1, 1, 1)));
			Assert.IsNotNull( new Box(new Position(-1, -1, -1), new Size(1, 1, 1)));
			Assert.IsNotNull(new Box(new Position(0, 0, 0)));
		}

	



	}
}
