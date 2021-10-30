using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.Parsing.Setters
{
	public class SetterGroup<TComponent> : ISetter<TComponent>
	{
		private IEnumerable<ISetter<TComponent>> items;

		public SetterGroup(IEnumerable<ISetter<TComponent>> Items)
		{
			if (Items == null) throw new ArgumentNullException(nameof(Items));
			this.items = Items;
		}
		public TComponent Set(TComponent Component)
		{
			foreach(ISetter<TComponent> setter in items)
			{
				setter.Set(Component);
			}
			return Component;
		}
	}


}
