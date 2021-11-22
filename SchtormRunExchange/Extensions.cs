using System;
using SchtormRunExchange.Properties;

namespace SchtormRunExchange
{
    public static class Extensions
    {
        private static readonly string[] _valueTypes = new string[]
        {
            Settings.Default.SchtormTextType,
            Settings.Default.SchtormMarkdownType,
            Settings.Default.SchtormWebpageType,
        };

        public static string Include(this string baseInfo, ValueType valueType, GetValueWay getValueWay) =>
            _valueTypes[(int)valueType] + ">>>" + (getValueWay == GetValueWay.Path ? 
                $"{Settings.Default.SchtormPathWay}>>>" : 
                $"{Settings.Default.SchtormCodeWay}>>>")
            + baseInfo;
    }
}
