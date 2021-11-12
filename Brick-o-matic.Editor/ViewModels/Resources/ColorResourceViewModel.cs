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
	public class ColorResourceViewModel : ResourceViewModel
	{


		public static readonly DependencyProperty ObjectProperty = DependencyProperty.Register("Object", typeof(ColorViewModel), typeof(ColorResourceViewModel));
		public ColorViewModel Object
		{
			get { return (ColorViewModel )GetValue(ObjectProperty); }
			set { SetValue(ObjectProperty, value); }
		}


		public ColorResourceViewModel()
		{
			Object = new ColorViewModel();
		}



		protected override async Task OnLoadAsync(Resource Model)
		{
			await base.OnLoadAsync(Model);
			await Object.LoadAsync((Color)Model.Object);
		}

		public override Task RefreshAsync()
		{
			throw new NotImplementedException();
		}
	}
}
