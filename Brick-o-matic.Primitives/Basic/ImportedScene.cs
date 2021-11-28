using Brick_o_matic.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.Primitives
{
	public class ImportedScene : Primitive,IImportedScene
	{


		public IScene Scene
		{
			get;
			set;
		}


		public ImportedScene()
		{
		}
		public ImportedScene(Position Position):base(Position)
		{
			
		}
		

		public override IBox GetBoundingBox(IResourceProvider ResourceProvider)
		{
			IBox childBox;

			if (ResourceProvider == null) throw new ArgumentNullException(nameof(ResourceProvider));

			if (this.Scene == null) childBox= new Box();
			else childBox = this.Scene.GetBoundingBox(ResourceProvider);
			
			return new Box(Position + childBox.Position, childBox.Size);

		}

		public override IEnumerable<Brick> Build(IResourceProvider ResourceProvider)
		{
			if (ResourceProvider == null) throw new ArgumentNullException(nameof(ResourceProvider));

			if (this.Scene == null) yield break;

			foreach (Brick brick in this.Scene.Build(ResourceProvider))
			{
				yield return new Brick(this.Position + brick.Position, brick.Size,brick.Color);
			}
		}

		public override void Validate(IResourceProvider ResourceProvider, ILocker Locker)
		{
			if (Locker == null) throw new ArgumentNullException(nameof(Locker));
			if (ResourceProvider == null) throw new ArgumentNullException(nameof(ResourceProvider));
			Scene?.Validate();
		}

	}
}
