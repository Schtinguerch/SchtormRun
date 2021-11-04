using System;
using System.Collections.Generic;
using System.Net;
using System.Web;
using SchtormRun.Controls.Pages.CliPages;
using ScriptMaker;

using Localization = SchtormRun.Resources.Dictionaries.Localization.Resources;

namespace SchtormRun.AppFunctionality.Commands
{
    public class GoogleTranslator : Command
    {
        public override string Name { get; set; } = "translate";

        public override void Run(List<string> arguments, object processingObject = null)
        {
            var inputLanguageCode = arguments[0];
            var outputLanguageCode = arguments[1];

            arguments.RemoveRange(0, 2);
            SetAdditional(arguments);

            var text = UnitedStringArgument;

            var translatedText = TranslatedText(text, inputLanguageCode, outputLanguageCode);
            var textUrl = GetBrowserUrl(text, inputLanguageCode, outputLanguageCode);

            CenterNode.SubWindow.Display();

            try
            {
                CenterNode.SubWindow.OpenPage(new TranslationResultPage(textUrl, translatedText));
            }
            
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        private string TranslatedText(string inputText, string inputLanguage, string outputLanguage)
        {
            var url = GetApiUrl(inputText, inputLanguage, outputLanguage);
            var webClient = new WebClient
            {
                Encoding = System.Text.Encoding.UTF8
            };

            var result = webClient.DownloadString(url);

            try
            {
                result = result.Substring(4, result.IndexOf("\"", 4, StringComparison.Ordinal) - 4);
                return result;
            }
            catch
            {
                return "";
            }
        } 

        private string GetApiUrl(string inputText, string inputLanguage, string outputLanguage)
        {
            var urlValidText = HttpUtility.UrlEncode(inputText);
            var url = $"https://translate.googleapis.com/translate_a/single?client=gtx&sl={inputLanguage}&tl={outputLanguage}&dt=t&q={urlValidText}";

            return url;
        }

        private string GetBrowserUrl(string inputText, string inputLanguage, string outputLanguage)
        {
            var urlValidText = HttpUtility.UrlEncode(inputText);
            var url = $"https://translate.google.com/?hl=en&sl={inputLanguage}&tl={outputLanguage}&text={urlValidText}&op=translate";

            return url;
        }
    }
}
