using Brick_o_matic.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ViewModelLib;
using Size = Brick_o_matic.Math.Size;

namespace Brick_o_matic.Editor.ViewModels.Math
{
	public class SizeViewModel : ViewModel<Size>
	{


		public static readonly DependencyProperty XProperty = DependencyProperty.Register("X", typeof(int), typeof(SizeViewModel), new PropertyMetadata(0));
		public int X
		{
			get { return (int)GetValue(XProperty); }
			set { SetValue(XProperty, value); }
		}

		public static readonly DependencyProperty YProperty = DependencyProperty.Register("Y", typeof(int), typeof(SizeViewModel), new PropertyMetadata(0));
		public int Y
		{
			get { return (int)GetValue(YProperty); }
			set { SetValue(YProperty, value); }
		}

		public static readonly DependencyProperty ZProperty = DependencyProperty.Register("Z", typeof(int), typeof(SizeViewModel), new PropertyMetadata(0));
		public int Z
		{
			get { return (int)GetValue(ZProperty); }
			set { SetValue(ZProperty, value); }
		}


		protected override async Task OnLoadAsync(Size Model)
		{
			await base.OnLoadAsync(Model);
			this.X = Model.X;
			this.Y = Model.Y;
			this.Z = Model.Z;
		}

		public override Task RefreshAsync()
		{
			throw new NotImplementedException();
		}

		public override string ToString()
		{
			return $"({X},{Y},{Z})";
		}
	}
}
