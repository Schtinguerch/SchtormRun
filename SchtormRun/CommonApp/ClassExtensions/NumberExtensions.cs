using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchtormRun
{
    public static class NumberExtensions
    {
        public static double Max(this double x, double y)
        {
            if (x > y)
                return x;

            return y;
        }

        public static double Min(this double x, double y)
        {
            if (x < y)
                return x;

            return y;
        }
    }
}
