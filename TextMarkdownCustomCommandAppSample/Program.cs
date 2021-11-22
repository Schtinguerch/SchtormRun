/*
 * Custom command application sample
 * This application returns value as markdown document
 * The output document shown in SchtormRun SubWindow
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

using System.IO;
using SchtormRunExchange;
using SchtormRunExchange.Properties;

namespace TextMarkdownCustomCommandAppSample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var fileName = "sample.md";
            var outputFilename = fileName.Include(OutValueType.Markdown, GetValueWay.Path);

            var sharedValue = new SharedVariable(Settings.Default.OutputVariable);
            sharedValue.Value = outputFilename;

            sharedValue.RegisterShare();
        }
    }
}
