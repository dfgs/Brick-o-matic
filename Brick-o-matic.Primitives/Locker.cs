using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.Primitives
{
	public class Locker : ILocker
	{
		private List<string> items;

		public Locker()
		{
			items = new List<string>();
		}

		public void Lock(string Name)
		{
			if (items.Contains(Name)) throw new InvalidOperationException($"Self referenced primitive detected ({Name})");
			items.Add(Name);
		}

		public void Release(string Name)
		{
			items.Remove(Name);
		}
	}
}
