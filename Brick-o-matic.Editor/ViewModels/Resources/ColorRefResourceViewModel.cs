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
	public class ColorRefResourceViewModel : ResourceViewModel
	{

		public static readonly DependencyProperty ObjectProperty = DependencyProperty.Register("Object", typeof(ColorRefViewModel), typeof(ColorRefResourceViewModel));
		public ColorRefViewModel Object
		{
			get { return (ColorRefViewModel)GetValue(ObjectProperty); }
			set { SetValue(ObjectProperty, value); }
		}

		public ColorRefResourceViewModel()
		{
			Object = new ColorRefViewModel();
		}

		protected override async Task OnLoadAsync(Resource Model)
		{
			await base.OnLoadAsync(Model);
			await Object.LoadAsync((ColorRef)Model.Object);
		}

		public override Task RefreshAsync()
		{
			throw new NotImplementedException();
		}
	}
}
