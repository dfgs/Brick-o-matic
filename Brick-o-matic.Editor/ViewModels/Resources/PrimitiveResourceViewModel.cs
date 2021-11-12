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
	public class PrimitiveResourceViewModel : ResourceViewModel
	{
		public static readonly DependencyProperty ObjectProperty = DependencyProperty.Register("Object", typeof(IPrimitiveViewModel), typeof(PrimitiveResourceViewModel), new PropertyMetadata(null));
		public IPrimitiveViewModel Object
		{
			get { return (IPrimitiveViewModel)GetValue(ObjectProperty); }
			set { SetValue(ObjectProperty, value); }
		}//*/



		public IEnumerable<IPrimitiveViewModel> Items
		{
			get 
			{ 
				yield return Object; 
			}
		}

		protected override async Task OnLoadAsync(Resource Model)
		{
			await base.OnLoadAsync(Model);
			
			Object = PrimitiveViewModel.CreatePrimitiveViewModel((IPrimitive)Model.Object);
			await Object.LoadAsync(Model.Object);
			OnPropertyChanged("Items");
		}

		public override Task RefreshAsync()
		{
			throw new NotImplementedException();
		}

	}
}
