using Brick_o_matic.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.Primitives
{
    public struct BoxGeometry
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

        public Color Color
        {
            get;
            set;
        }


       
        public BoxGeometry(Position Position, Size Size,Color Color)
        {
            this.Position = Position; this.Size = Size;
            this.Color = Color;
        }

        


	}
}
