using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.Primitives
{
	public class Variable : IVariable
	{
	

		public string Value
		{
			get;
			set;
		}

		public Variable(string Value)
		{
			this.Value = Value;
		}
	}
}
