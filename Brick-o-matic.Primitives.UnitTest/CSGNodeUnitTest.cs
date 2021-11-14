using Brick_o_matic.Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Brick_o_matic.Primitives.UnitTest
{
	[TestClass]
	public class CSGNodeUnitTest
	{
		[TestMethod]
		public void ShouldAdd()
		{
			CSGNode node;

			node = new CSGNode();
			Assert.IsNotNull(node) ;
			Assert.AreEqual(0, node.Count);

			node.Add(new CSGNode());
			Assert.AreEqual(1, node.Count);
			node.Add(new CSGNode());
			Assert.AreEqual(2, node.Count);
		}

		[TestMethod]
		public void ShouldEnumerate()
		{
			CSGNode node,a,b,c;
			ICSGNode[] items;
			
			node = new CSGNode();
			a = new CSGNode();
			b = new CSGNode();
			c = new CSGNode();
			Assert.AreEqual(0, node.Count);

			node.Add(a);
			node.Add(b);
			node.Add(c);
			Assert.AreEqual(3, node.Count);

			items = node.Nodes.ToArray();
			Assert.AreEqual(a, items[0]);
			Assert.AreEqual(b, items[1]);
			Assert.AreEqual(c, items[2]);

		}



	}
}
