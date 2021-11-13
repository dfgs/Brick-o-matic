﻿using Brick_o_matic.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelLib;

namespace Brick_o_matic.Editor.ViewModels
{
	public class PrimitivesViewModel : ViewModelCollection<IPrimitiveViewModel,IPrimitive>
	{

		protected override IPrimitiveViewModel OnCreateNewItem(IPrimitive Model)
		{
			return PrimitiveViewModel.CreatePrimitiveViewModel(Model);
		}
		public override Task RefreshAsync()
		{
			throw new NotImplementedException();
		}
	}
}