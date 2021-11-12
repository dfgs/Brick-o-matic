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
	public static class PrimitiveViewModel
	{
		public static IPrimitiveViewModel CreatePrimitiveViewModel(IPrimitive Primitive)
		{
			switch (Primitive)
			{
				case Brick brick: return new BrickViewModel();
				case Part part: return new PartViewModel();
				case PrimitiveRef primitiveRef: return new PrimitiveRefViewModel();
				case TileMap tileMap: return new TileMapViewModel();
				case RotateX rotateX: return new RotateXViewModel();
				case RotateY rotateY: return new RotateYViewModel();
				case RotateZ rotateZ: return new RotateZViewModel();
				case FlipX flipX: return new FlipXViewModel();
				case FlipY flipY: return new FlipYViewModel();
				case FlipZ flipZ: return new FlipZViewModel();
				default: return new UnknownPrimitiveViewModel();

			}
		}
	}
	public abstract class PrimitiveViewModel<T> : ViewModel<T>,IPrimitiveViewModel
		where T:IPrimitive
	{


		public static readonly DependencyProperty PositionProperty = DependencyProperty.Register("Position", typeof(PositionViewModel), typeof(PrimitiveViewModel<T>));
		public PositionViewModel Position
		{
			get { return (PositionViewModel)GetValue(PositionProperty); }
			set { SetValue(PositionProperty, value); }
		}
		

		public PrimitiveViewModel()
		{
			Position = new PositionViewModel();
		}



		public override Task RefreshAsync()
		{
			throw new NotImplementedException();
		}

		protected override async Task OnLoadAsync(T Model)
		{
			await base.OnLoadAsync(Model);
			await Position.LoadAsync(Model.Position);

		}
		Task IViewModel<IPrimitive>.LoadAsync(IPrimitive Model)
		{
			return LoadAsync((T)Model);
		}

		Task IViewModel<ISceneObject>.LoadAsync(ISceneObject Model)
		{
			return LoadAsync((T)Model);
		}
	}
}
