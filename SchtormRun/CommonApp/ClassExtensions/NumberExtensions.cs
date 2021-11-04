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
