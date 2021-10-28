﻿using System.Collections.Generic;
using SchtormRun.Controls.Windows;
using SchtormRun.Properties;
using Newtonsoft.Json;

namespace SchtormRun
{
    public static class CenterNode
    {
        public static CommandLineWindow AppWindow { get; set; }
        public static SubWindow SubWindow { get; set; }
        public static Dictionary<string, string> PreprocessorReplacement { get; set; }

        public static void Initialize()
        {
            DiscordRPC.Enabled = Settings.Default.EnabledDiscordRPC;
            PreprocessorReplacement = JsonConvert.DeserializeObject<Dictionary<string, string>>(Settings.Default.ReplacementDictionaryPath);
        }
    }
}