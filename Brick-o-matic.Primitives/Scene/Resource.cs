using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.Primitives
{
	public struct Resource
	{
		public string Name
		{
			get;
			private set;
		}

		public ISceneObject Object
		{
			get;
			private set;
		}

		public Resource(string Name,ISceneObject Object)
		{
			if (Name == null) throw new ArgumentNullException(nameof(Name));
			if (Object == null) throw new ArgumentNullException(nameof(Object));

			this.Name = Name;this.Object = Object;
		}

		
	}
}
