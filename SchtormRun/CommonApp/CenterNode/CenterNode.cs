using System.Collections.Generic;
using System.IO;
using SchtormRun.Controls.Windows;
using SchtormRun.Properties;
using Newtonsoft.Json;

namespace SchtormRun
{
    public static class CenterNode
    {
        public static CommandLineWindow AppWindow { get; set; }
        public static SubWindow SubWindow { get; set; }
        public static NotificationWindow NotificationWindow { get; set; }
        public static Dictionary<string, string> PreprocessorReplacement { get; set; }
        public static Dictionary<string, string> CustomCommandsDictionary { get; set; }
        public static CommandHistory CommandHistory { get; set; }

        public static void Initialize()
        {
            var abbreviationPath = Settings.Default.ReplacementDictionaryPath;
            var customCommandsPath = Settings.Default.CustomCommandsDictionaryPath;

            PreprocessorReplacement = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(abbreviationPath));
            CustomCommandsDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(customCommandsPath));

            DiscordRPC.Enabled = Settings.Default.EnabledDiscordRPC;
            CommandHistory = new CommandHistory();
        }
    }
}
