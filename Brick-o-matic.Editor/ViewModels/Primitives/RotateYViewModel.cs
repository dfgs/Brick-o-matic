using Brick_o_matic.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelLib;

namespace Brick_o_matic.Editor.ViewModels
{
	public class RotateYViewModel : PrimitiveViewModel<RotateY>
	{
		protected override IViewModel OnRegisterProperty(string Name, Type PropertyType)
		{
			switch (Name)
			{
				case "Count": return new PropertyViewModel<int>();

			}
			return base.OnRegisterProperty(Name, PropertyType);
		}
		public override Task RefreshAsync()
		{
			throw new NotImplementedException();
		}
	}
}
