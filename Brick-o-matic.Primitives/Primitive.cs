﻿using Brick_o_matic.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.Primitives
{
	public abstract class Primitive : IPrimitive
	{

		public Position Position
		{
			get;
			set;
		}

		
		public Primitive()
		{

		}
		public Primitive(Position Position)
		{
			this.Position = Position;
		}
		public abstract Box GetBoundingBox();

		public abstract IEnumerable<Brick> Build(IScene Scene);
	}
}
