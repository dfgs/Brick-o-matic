using Brick_o_matic.Editor.ViewModels.Math;
using Brick_o_matic.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelLib;

namespace Brick_o_matic.Editor.ViewModels
{
	public class TileMapViewModel : PrimitiveViewModel<TileMap>
	{

		protected override IViewModel OnRegisterProperty(string Name, Type PropertyType)
		{
			switch(Name)
			{
				case "TileSize":return new SizeViewModel();
			}
			return base.OnRegisterProperty(Name, PropertyType);
		}
		public override Task RefreshAsync()
		{
			throw new NotImplementedException();
		}
	}
}
