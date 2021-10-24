using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.Math
{
	public interface IBox
	{
        int X1
        {
            get;
        }
        int Y1
        {
            get;
        }
        int Z1
        {
            get;
        }

        Vector Size
		{
            get;
		}
      

        int X2
        {
            get;
        }
        int Y2
        {
            get;
        }
        int Z2
        {
            get;
        }

        bool IntersectWith(Box Other);
        bool IsInside(Vector Coordinate);

        IEnumerable<Box> SplitWith(Box Other);

    }
}
