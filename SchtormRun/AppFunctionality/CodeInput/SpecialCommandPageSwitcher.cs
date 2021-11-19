using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Controls;

using ICSharpCode.AvalonEdit;

namespace SchtormRun
{
    public class SpecialCommandPageSwitcher
    {
        private TextEditor _textEditor;

        public Dictionary<string, Page> CommandPageDictionary { get; set; }

        public SpecialCommandPageSwitcher(
            TextEditor textEditor, 
            Dictionary<string, Page> commandAndPageDictionary)
        {
            CommandPageDictionary = commandAndPageDictionary;

            _textEditor = textEditor;
            _textEditor.TextChanged += TextChanged;
        }

        private void TextChanged(object sender, EventArgs e)
        {
            if (_textEditor.Text.Contains(" "))
                return;

            foreach (var commandAndPage in CommandPageDictionary)
            {
                var pattern = $"(^|^\\s+)({commandAndPage.Key})";

                if (Regex.IsMatch(_textEditor.Text, pattern))
                {
                    CenterNode.AppWindow.OpenPage(commandAndPage.Value);
                    (commandAndPage.Value as ILoadable)?.Load();
                    break;
                }
            }
        }
    }
}
