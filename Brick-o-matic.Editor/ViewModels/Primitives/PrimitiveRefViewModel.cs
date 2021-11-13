using Brick_o_matic.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelLib;

namespace Brick_o_matic.Editor.ViewModels
{
	public class PrimitiveRefViewModel : PrimitiveViewModel<PrimitiveRef>
	{
		protected override IViewModel OnRegisterProperty(string Name, Type PropertyType)
		{
			switch(Name)
			{
				case "Name":return new PropertyViewModel<string>();
			}
			return base.OnRegisterProperty(Name, PropertyType);
		}
		public override Task RefreshAsync()
		{
			throw new NotImplementedException();
		}
	}
}
