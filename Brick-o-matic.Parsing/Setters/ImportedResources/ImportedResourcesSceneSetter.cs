using Brick_o_matic.Math;
using Brick_o_matic.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.Parsing.Setters
{
	public class ImportedResourcesSceneSetter : Setter<ImportedResources, Scene>, IImportedResourcesSetter
	{
		
		public ImportedResourcesSceneSetter(Scene Value) : base(Value)
		{
		}

		public override ImportedResources Set(ImportedResources Component)
		{
			Component.Scene = Value;
			return Component;
		}
	}
}
