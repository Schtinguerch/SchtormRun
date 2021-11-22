/*
 * Custom command application sample
 * This application returns value as webpage
 * The output webpage shown in SchtormRun SubWindow
 * 
 * To share value there is necessary to use
 * namespace SchtormRunExchange from SchtormRunExchange.dll
 * 
 * SchtormRunExchange.SharedVariable is a simplified
 * MemoryMappedFile from System.IO.MemoryMappedFiles
 * 
 * Output variable:           "SRE_outputValue"
 * Application theme:         "SRE_appTheme"
 * Application startup path:  "SRE_appStartupPath"
 * Application window width:  "SRE_appWidth"
 * 
 * Or use Settings.Default from SchtormRunExchange.Properties
 * 
 * Output variable:           Settings.Default.OutputVariable
 * Application theme:         Settings.Default.ApplicationTheme
 * Application startup path:  Settings.Default.ApplicationStartupPath
 * Application window width:  Settings.Default.ApplicationWindowWidth
 */

using SchtormRunExchange;
using SchtormRunExchange.Properties;

namespace WebViewCustomCommandAppSample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var outputUrl = ConvertToWikiPage(args).Include(OutValueType.Webpage, GetValueWay.Path);
            var sharedValue = new SharedVariable(Settings.Default.OutputVariable);

            sharedValue.Value = outputUrl;
            sharedValue.RegisterShare();
        }

        static string ConvertToWikiPage(string[] words) =>
            $"https://ru.wikipedia.org/wiki/{UnitedToWikiSubpage(words)}";

        static string UnitedToWikiSubpage(string[] words)
        {
            if (words == null || words.Length == 0)
                return "";

            var subpageUrl = words[0];
            for (int i = 1; i < words.Length; i++)
                subpageUrl += '_' + words[i];

            return subpageUrl;
        }
    }
}
