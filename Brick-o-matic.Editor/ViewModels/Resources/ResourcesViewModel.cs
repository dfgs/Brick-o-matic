using Brick_o_matic.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelLib;

namespace Brick_o_matic.Editor.ViewModels
{
	public class ResourcesViewModel : ViewModelCollection<ResourceViewModel,Resource>
	{
		protected override ResourceViewModel OnCreateNewItem(Resource Model)
		{
			return ResourceViewModel.CreateResourceViewModel(Model);
		}
		public override Task RefreshAsync()
		{
			throw new NotImplementedException();
		}
	}
}
