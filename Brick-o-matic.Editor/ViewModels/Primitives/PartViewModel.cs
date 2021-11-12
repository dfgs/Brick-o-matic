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
	public class PartViewModel : PrimitiveViewModel<Part>
	{
		public static readonly DependencyProperty ItemsProperty = DependencyProperty.Register("Items", typeof(PrimitivesViewModel), typeof(PartViewModel), new PropertyMetadata(null));
		public PrimitivesViewModel Items
		{
			get { return (PrimitivesViewModel)GetValue(ItemsProperty); }
			set { SetValue(ItemsProperty, value); }
		}

		public PartViewModel()
		{
			Items = new PrimitivesViewModel();
		}

		protected override async Task OnLoadAsync(Part Model)
		{

			await base.OnLoadAsync(Model);

			await Items.LoadAsync(Model.Items, PrimitiveViewModel.CreatePrimitiveViewModel);

		}

		public override Task RefreshAsync()
		{
			throw new NotImplementedException();
		}
	}
}
