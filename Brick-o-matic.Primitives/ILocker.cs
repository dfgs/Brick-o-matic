using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.Primitives
{
	public interface ILocker
	{
		void Lock(string Name);
		void Release(string Name);

	}
}
