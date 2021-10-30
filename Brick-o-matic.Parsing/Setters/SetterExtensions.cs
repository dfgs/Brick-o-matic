using Brick_o_matic.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.Parsing.Setters
{
    public static class MathExtensions
    {
        public static TComponent Set<TComponent>(this TComponent Component,IEnumerable<ISetter<TComponent>> Setters)
        {
            if (Setters != null)
            {
                foreach(ISetter<TComponent> setter in Setters) setter.Set(Component);
            }
            return Component;
        }
       
    }
}
