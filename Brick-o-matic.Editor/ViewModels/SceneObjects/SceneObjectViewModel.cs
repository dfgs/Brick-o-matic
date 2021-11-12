using Brick_o_matic.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelLib;

namespace Brick_o_matic.Editor.ViewModels
{

	public static class SceneObjectViewModel
	{
		public static ISceneObjectViewModel CreateSceneObjectViewModel(ISceneObject SceneObject)
		{
			switch (SceneObject)
			{
				case Color color: return new ColorViewModel();
				case ColorRef colorRef: return new ColorRefViewModel();
				case IPrimitive primitive: return PrimitiveViewModel.CreatePrimitiveViewModel(primitive);
				default: return new UnknownPrimitiveViewModel();

			}
		}
	}
	public abstract class SceneObjectViewModel<T> : ViewModel<T>,ISceneObjectViewModel
		where T:ISceneObject
	{
		public override Task RefreshAsync()
		{
			throw new NotImplementedException();
		}

		Task IViewModel<ISceneObject>.LoadAsync(ISceneObject Model)
		{
			return LoadAsync((T)Model);
		}
	}
}
