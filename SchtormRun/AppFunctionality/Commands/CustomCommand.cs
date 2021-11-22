using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

using ScriptMaker;
using SchtormRunExchange;
using SchtormRunExchange.Properties;

using SchtormRun.Controls.Pages.CliPages;
using SchtormRun.Controls.Pages.SubWindowPages;

namespace SchtormRun.AppFunctionality.Commands
{
    public class CustomCommand : Command
    {
        private string _executionFilename;

        public override string Name { get; set; }

        public CustomCommand(string commandName, string executionFilename)
        {
            Name = commandName;
            _executionFilename = executionFilename;
        }

        public override void Run(List<string> arguments, object processingObject = null)
        {
            SetAdditional(arguments); 

            if (!File.Exists(_executionFilename))
                throw new FileNotFoundException("", _executionFilename);

            var customApp = new Process()
            {
                StartInfo = new ProcessStartInfo()
                {
                    FileName = _executionFilename,
                    Arguments = UnitedStringArgument,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true,
                }
            };

            customApp.Start();
            CollectOutputData(customApp);
        }

        private void CollectOutputData(Process process)
        {
            var outputSharedVariableFlag = new SharedVariable(Settings.Default.DataAccepted);

            while (!process.StandardOutput.EndOfStream)
            {
                var textLine = process.StandardOutput.ReadLine();
                if (textLine.Length >= 12 && textLine.Substring(0, 12) == "<SchtormRun>")
                {
                    var textValue = new SharedVariable(Settings.Default.OutputVariable).Value;
                    outputSharedVariableFlag.Value = "true";

                    var tokens = textValue.Split(new string[] { ">>>" }, StringSplitOptions.RemoveEmptyEntries);

                    var valueType = tokens[0].ToLower();
                    var valueWay = tokens[1].ToLower();

                    var values = tokens.ToList();

                    values.RemoveRange(0, 2);
                    var value = values.GlueToString();

                    switch (valueType)
                    {
                        case "schtorm_text":
                            ShowOutputText(value, valueWay);
                            break;

                        case "schtorm_markdown":
                            ShowMarkdownMarkup(value, valueWay);
                            break;

                        case "schtorm_webpage":
                            ShowWebPage(value, valueWay);
                            break;
                    }
                }
            }
        }

        private void ShowOutputText(string text, string way)
        {
            if (way == Settings.Default.SchtormPathWay)
                text = File.ReadAllText(text);

            CenterNode.SubWindow.OpenPage(new TranslationResultPage(text));
        }

        private void ShowMarkdownMarkup(string text, string way)
        {
            if (way == Settings.Default.SchtormCodeWay)
            {
                var uri = "TempSchtormMdPage.md";
                File.WriteAllText(uri, text);

                text = uri;
            }
                

            CenterNode.SubWindow.OpenPage(new MarkdownPage(text));
        }

        private void ShowWebPage(string text, string way)
        {
            if (way == Settings.Default.SchtormCodeWay)
            {
                var uri = "TempSchtormWebPage.md";
                File.WriteAllText(uri, text);

                text = uri;
            }

            CenterNode.SubWindow.OpenPage(new WebViewPage(text));
        }
    }
}
