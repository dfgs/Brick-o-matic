using Brick_o_matic.Math;
using Brick_o_matic.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.Parsing.Setters
{
	public class ImportPositionSetter : Setter<Import, Position>, IImportSetter
	{
		
		public ImportPositionSetter(Position Value) : base(Value)
		{
		}

		public override Import Set(Import Component)
		{
			Component.Position = Value;
			return Component;
		}
	}
}
