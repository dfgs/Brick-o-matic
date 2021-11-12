﻿
using Brick_o_matic.Math;
using Brick_o_matic.Parsing.Setters;
using Brick_o_matic.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.Parsing.Setters
{
	public class TileMapTileSizeXSetter : Setter<TileMap, int>, ITileMapSetter
	{
		
		public TileMapTileSizeXSetter(int Value) : base(Value)
		{
		}

		public override TileMap Set(TileMap Component)
		{
			Component.TileSizeX = Value;
			return Component;
		}
	}
}
