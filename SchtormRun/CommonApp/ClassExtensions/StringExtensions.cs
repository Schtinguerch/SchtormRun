using System;
using System.Reflection;

namespace SchtormRun
{
    public static class StringExtensions
    {
        public static Uri RelativeToAppUri(this string path) =>
            new Uri(string.Format("pack://application:,,,/{0};component/{1}",
                Assembly.GetExecutingAssembly().GetName().Name,
                path));
    }
}
