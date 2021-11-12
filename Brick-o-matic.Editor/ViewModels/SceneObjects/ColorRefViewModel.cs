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
	public class ColorRefViewModel : SceneObjectViewModel<ColorRef>
	{

		public static readonly DependencyProperty NameProperty = DependencyProperty.Register("Name", typeof(string), typeof(ColorRefViewModel));
		public string Name
		{
			get { return (string)GetValue(NameProperty); }
			set { SetValue(NameProperty, value); }
		}



		protected override async Task OnLoadAsync(ColorRef Model)
		{
			await base.OnLoadAsync(Model);
			this.Name = Model.Name;
		}

		public override Task RefreshAsync()
		{
			throw new NotImplementedException();
		}
	}
}
