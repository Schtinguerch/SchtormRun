using System;
using System.Collections.Generic;
using System.Windows.Input;

using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.CodeCompletion;

using SchtormRun.Properties;

namespace SchtormRun
{
    public class AutoCompleter
    {
        private TextEditor _textEditor;
        private CompletionWindow _completionWindow;

        public List<IRule> AutocompletionRules { get; set; }
        
        public AutoCompleter(TextEditor textEditor, List<IRule> autocompletionRules)
        {
            _textEditor = textEditor;
            AutocompletionRules = autocompletionRules;

            _textEditor.TextArea.TextEntering += TextEntering;
            _textEditor.TextArea.TextEntered += TextEntered;
        }

        private void TextEntering(object sender, TextCompositionEventArgs e)
        {
            if (e.Text.Length > 0 && _completionWindow != null)
                if (!char.IsLetterOrDigit(e.Text[0]))
                    _completionWindow.CompletionList.RequestInsertion(e);
        }

        private void TextEntered(object sender, TextCompositionEventArgs e)
        {
            foreach (var rule in AutocompletionRules)
            {
                if (rule.LastInputCharacter == e.Text && rule.PreviousInputSuits(_textEditor.Text))
                {
                    if (_completionWindow == null)
                    {
                        _completionWindow = new CompletionWindow(_textEditor.TextArea);
                        _completionWindow.Show();

                        _completionWindow.MinWidth = 
                            (CenterNode.AppWindow.ActualWidth + 
                            CenterNode.AppWindow.Left - 
                            _completionWindow.Left).Max(440);

                        _completionWindow.Top = 
                            CenterNode.AppWindow.Top + 
                            CenterNode.AppWindow.ActualHeight + 
                            Settings.Default.AppBorderRadius;

                        _completionWindow.Closed += (s, ee) => _completionWindow = null;
                    }

                    for (int i = 0; i < rule.AutocompleteWords.Count; i++)
                        _completionWindow.CompletionList.CompletionData.Add(
                            new CompletionData(
                                rule.AutocompleteWords[i],
                                rule.AutocompleteDescriptions[i],
                                rule.AutocompleteIcons[i]));
                }
            }
        }
    }
}
