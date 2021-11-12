using Brick_o_matic.Editor.ViewModels.Math;
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
	public class BrickViewModel : PrimitiveViewModel<Brick>
	{
		public static readonly DependencyProperty SizeProperty = DependencyProperty.Register("Size", typeof(SizeViewModel), typeof(BrickViewModel));
		public SizeViewModel Size
		{
			get { return (SizeViewModel)GetValue(SizeProperty); }
			set { SetValue(SizeProperty, value); }
		}

		public static readonly DependencyProperty ColorProperty = DependencyProperty.Register("Color", typeof(ISceneObjectViewModel), typeof(BrickViewModel));
		public ISceneObjectViewModel Color
		{
			get { return (ISceneObjectViewModel)GetValue(ColorProperty); }
			set { SetValue(ColorProperty, value); }
		}

		public BrickViewModel()
		{
			Size = new SizeViewModel();
		}


		protected override async Task OnLoadAsync(Brick Model)
		{
			await base.OnLoadAsync(Model);
			await Size.LoadAsync(Model.Size);
			if (Model.Color is Color color)
			{
				this.Color = new ColorViewModel();
			}
			else
			{
				this.Color = new ColorRefViewModel();
			}
			await this.Color.LoadAsync(Model.Color);

		}

		public override Task RefreshAsync()
		{
			throw new NotImplementedException();
		}
	}
}
