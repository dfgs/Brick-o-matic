using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.Primitives
{
	public class ColorRef:IColor
	{
		public string Name
		{
			get;
			private set;
		}
		
		
		public ColorRef(string Name)
		{
			this.Name = Name;
		}

		public override string ToString()
		{
			return $"(ref to {Name})";
		}

		public void GetComponents(IResourceProvider ResourceProvider, out byte R, out byte G, out byte B)
		{
			IColor color;

			if (ResourceProvider == null) throw new ArgumentNullException(nameof(ResourceProvider));

			if (!ResourceProvider.TryGetResource(Name,  out color))
			{
				throw new InvalidOperationException($"Reference to color {Name} was not found");
			}
			color.GetComponents(ResourceProvider, out R, out G, out B);
			
		}

		

		

	}
}
