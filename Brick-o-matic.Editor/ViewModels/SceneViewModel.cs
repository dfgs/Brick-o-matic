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
	public class SceneViewModel : ViewModel<Scene>
	{


		

		public SceneViewModel()
		{
		}

		protected override IViewModel OnRegisterProperty(string Name, Type PropertyType)
		{
			switch(Name)
			{
				case "Resources":return new ResourcesViewModel();
				case "Items":return new PrimitivesViewModel();
			}
			return base.OnRegisterProperty(Name, PropertyType);
		}


		
		public override Task RefreshAsync()
		{
			throw new NotImplementedException();
		}
	}
}
