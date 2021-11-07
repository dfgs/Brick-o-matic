using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brick_o_matic.Primitives.UnitTest
{
	
	[TestClass]
	public class ResourceProviderRouterUnitTest
	{
		

		[TestMethod]
		public void ShouldHaveValidConstructor()
		{
			Assert.ThrowsException<ArgumentNullException>(() => new ResourceProviderRouter(null, new Scene()));
			Assert.ThrowsException<ArgumentNullException>(() => new ResourceProviderRouter(new Scene(),null ));
		}


	}
}
