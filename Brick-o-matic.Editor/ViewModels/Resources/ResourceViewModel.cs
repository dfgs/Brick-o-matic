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
	public abstract class ResourceViewModel : ViewModel<Resource>
	{
		public static readonly DependencyProperty NameProperty = DependencyProperty.Register("Name", typeof(string), typeof(ResourceViewModel));
		public string Name
		{
			get { return (string)GetValue(NameProperty); }
			set { SetValue(NameProperty, value); }
		}
		public static ResourceViewModel CreateResourceViewModel(Resource Model)
		{
			switch(Model.Object)
			{
				case Color color: return new ColorResourceViewModel();
				case ColorRef colorRef: return new ColorRefResourceViewModel();
				case IPrimitive primitive: return new PrimitiveResourceViewModel();
				default: return new UnknownResourceViewModel();
			}
		}
		protected override async Task OnLoadAsync(Resource Model)
		{
			await base.OnLoadAsync(Model);
			this.Name = Model.Name;
		}



	}
}
