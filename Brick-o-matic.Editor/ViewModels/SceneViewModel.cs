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


		public static readonly DependencyProperty ResourcesProperty = DependencyProperty.Register("Resources", typeof(ResourcesViewModel), typeof(SceneViewModel), new PropertyMetadata(null));
		public ResourcesViewModel Resources
		{
			get { return (ResourcesViewModel)GetValue(ResourcesProperty); }
			set { SetValue(ResourcesProperty, value); }
		}
		public static readonly DependencyProperty ItemsProperty = DependencyProperty.Register("Items", typeof(PrimitivesViewModel), typeof(SceneViewModel), new PropertyMetadata(null));
		public PrimitivesViewModel Items
		{
			get { return (PrimitivesViewModel)GetValue(ItemsProperty); }
			set { SetValue(ItemsProperty, value); }
		}

		public SceneViewModel()
		{
			Resources = new ResourcesViewModel();
			Items = new PrimitivesViewModel();
		}


		

		protected override async Task OnLoadAsync(Scene Model)
		{

			await base.OnLoadAsync(Model);

			await Resources.LoadAsync(Model.GetResources(), ResourceViewModel.CreateResourceViewModel );
			await Items.LoadAsync(Model.Items, PrimitiveViewModel.CreatePrimitiveViewModel);

		}
		public override Task RefreshAsync()
		{
			throw new NotImplementedException();
		}
	}
}
