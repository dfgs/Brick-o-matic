using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.Math
{
    public struct Box:IBox
    {
        public Position Position
		{
            get;
            private set;
		}
            
        

        public Size Size
		{
            get;
            private set;
        }
             

        public Box(Position Position)
        {
            this.Position=Position;this.Size = new Size(1, 1, 1);
        }
         
        public Box(Position Position, Size Size)
        {
            this.Position = Position;
            this.Size = Size;
        }

        

		public override string ToString()
		{
			return $"{Position}/{Size}";
		}


	}
}
