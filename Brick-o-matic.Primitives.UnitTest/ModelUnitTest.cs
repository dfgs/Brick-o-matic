using Brick_o_matic.Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Brick_o_matic.Primitives.UnitTest
{
	[TestClass]
	public class ModelUnitTest
	{
		[TestMethod]
		public void ShouldThrowConstructorErrorWhenParametersAreInvalid()
		{
			Assert.ThrowsException<ArgumentNullException>(() => new Model(null));

		}

		/*[TestMethod]
		public void ShouldReturnDefautBoudingBoxWithEmptyItems()
		{
			Model model;

			model = new Model();
			Assert.AreEqual(new Vector(1, 1, 1), model.BoundingBox.Size);

		}*/

		[TestMethod]
		public void ShouldReturnValidBoudingBox()
		{
			Model model;

			model = new Model(new BoxGeometry(new Position(1,1,1),new Size(1,1,1),new Color()));
			Assert.AreEqual(new Size(1, 1, 1), model.BoundingBox.Size);

			model = new Model(
				new BoxGeometry(new Position(1, 1, 1), new Size(1, 1, 1), new Color()),
				new BoxGeometry(new Position(2, 2, 2), new Size(1, 1, 1), new Color())
				);
			Assert.AreEqual(new Size(2, 2, 2), model.BoundingBox.Size);

			model = new Model(
				new BoxGeometry(new Position(1, 1, 1), new Size(1, 1, 1), new Color()),
				new BoxGeometry(new Position(-1, -1, -1), new Size(1, 1, 1), new Color())
				);
			Assert.AreEqual(new Size(3, 3, 3), model.BoundingBox.Size);
		}


	}


}
