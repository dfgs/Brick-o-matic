using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.Parsing.Setters
{
	public abstract class Setter<TComponent,T> : ISetter<TComponent,T>
	{
		public T Value
		{
			get;
			set;
		}

		public Setter(T Value)
		{
			this.Value = Value;
		}

		public abstract TComponent Set(TComponent Component);
	}
}
