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
	public class TileMapTileSizeZSetter : Setter<TileMap, int>, ITileMapSetter
	{
		
		public TileMapTileSizeZSetter(int Value) : base(Value)
		{
		}

		public override TileMap Set(TileMap Component)
		{
			Component.TileSizeZ = Value;
			return Component;
		}
	}
}
