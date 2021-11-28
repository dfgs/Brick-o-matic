using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.Primitives
{
	public class ImportedResources:ISceneObject
	{
		public Scene Scene
		{
			get;
			set;
		}

		public ImportedResources()
		{

		}
		public void Validate(IResourceProvider ResourceProvider, ILocker Locker)
		{
			if (ResourceProvider == null) throw new ArgumentNullException(nameof(ResourceProvider));
			if (Locker == null) throw new ArgumentNullException(nameof(Locker));
			
		}


	}
}
