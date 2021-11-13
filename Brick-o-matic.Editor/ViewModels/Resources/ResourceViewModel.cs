using Brick_o_matic.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ViewModelLib;

namespace Brick_o_matic.Editor.ViewModels
{
	public abstract class ResourceViewModel : ViewModel<Resource>
	{

		public ResourceViewModel():base()
		{

		}
		protected override IViewModel OnRegisterProperty(string Name, Type PropertyType)
		{
			switch (Name)
			{
				case "Name": return new PropertyViewModel<string>();
			}
			return base.OnRegisterProperty(Name, PropertyType);
		}
		public static ResourceViewModel CreateResourceViewModel(Resource Model)
		{
			switch(Model.Object)
			{
				case Color color: return new ColorResourceViewModel();
				case ColorRef colorRef: return new ColorRefResourceViewModel();
				case IPrimitive primitive: return new PrimitiveResourceViewModel();
				default: return new UnknownResourceViewModel();
			}
		}
		



	}
}
