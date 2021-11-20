using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.Math
{
	public interface IBox
	{
       
        Position Position
        {
            get;
        }
        Size Size
		{
            get;
		}


        IBox RotateX(int Count);
        IBox RotateY(int Count);
        IBox RotateZ(int Count);



    }
}
