/*
 * Custom command application sample
 * This application returns value as plain text
 * The output text shown in SchtormRun SubWindow
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
using System.Text;

using SchtormRunExchange;
using SchtormRunExchange.Properties;

namespace TextCustomCommandAppSample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args == null || args.Length == 0)
                return;

            var baseFolder = UnitedString(args, 1);
            var pattern = args[0];

            var suitFiles = Directory.GetFiles(baseFolder, pattern, SearchOption.AllDirectories);
            var outputText = GlueTextFromFiles(suitFiles).Include(OutValueType.Text, GetValueWay.Code);

            var sharedValue = new SharedVariable(Settings.Default.OutputVariable);
            sharedValue.Value = outputText;

            sharedValue.RegisterShare();
        }

        static string UnitedString(string[] words, int startPosition)
        {
            if (words == null || words.Length == 0)
                return "";

            var textLine = "";
            for (int i = startPosition; i < words.Length; i++)
                textLine += words[i] + ' ';

            return textLine.Trim();
        }

        static string GlueTextFromFiles(string[] files)
        {
            var text = new StringBuilder();

            foreach (var file in files)
                text.Append(File.ReadAllText(file));

            return text.ToString();
        }
    }
}
