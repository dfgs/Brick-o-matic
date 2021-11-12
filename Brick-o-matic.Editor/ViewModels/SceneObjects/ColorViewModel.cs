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
	public class ColorViewModel : SceneObjectViewModel<Color>
	{


		public static readonly DependencyProperty ColorProperty = DependencyProperty.Register("Color", typeof(System.Windows.Media.Color), typeof(ColorViewModel));
		public System.Windows.Media.Color Color
		{
			get { return (System.Windows.Media.Color)GetValue(ColorProperty); }
			set { SetValue(ColorProperty, value); }
		}



		protected override async Task OnLoadAsync(Color Model)
		{
			await base.OnLoadAsync(Model);
			this.Color = System.Windows.Media.Color.FromRgb(Model.R, Model.G, Model.B);
		}

		public override Task RefreshAsync()
		{
			throw new NotImplementedException();
		}
	}
}
