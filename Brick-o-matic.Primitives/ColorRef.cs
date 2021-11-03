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

		public void GetComponents(IScene Scene, out byte R, out byte G, out byte B)
		{
			ISceneObject resource;
			Color color;

			resource = Scene.GetResource(Name);
			if (!(resource is  Color)) throw new InvalidOperationException($"Reference to color {Name} was not found");
			color = (Color)resource;
			R = color.R;G = color.G;B = color.B;
		}

		

		

	}
}
