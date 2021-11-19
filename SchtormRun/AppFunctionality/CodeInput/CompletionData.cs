using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Editing;

using SchtormRun.Controls.UserConrols;

using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SchtormRun
{
    public class CompletionData : ICompletionData
    {
        private AutoCompleteItem _autoCompleteItem;
        private TextBlock _descriptionTextBlock;

        public ImageSource Image
        {
            get => null;// _autoCompleteItem.Icon.Source;
            set => _autoCompleteItem.Icon.Source = value;
        }

        public string Text
        {
            get => _autoCompleteItem.TextBlock.Text;
            set => _autoCompleteItem.TextBlock.Text = value;
        }

        public object Content => _autoCompleteItem;

        public object Description => _descriptionTextBlock;

        public double Priority { get; set; }

        public CompletionData(string text, string description, string uriPath)
        {
            _autoCompleteItem = new AutoCompleteItem();
            _descriptionTextBlock = new TextBlock() { Text = description };

            Text = text;

            if (!string.IsNullOrWhiteSpace(uriPath))
            {
                if (uriPath[0] == '>')
                {
                    Image = uriPath.Substring(1).GetIcon();
                    return;
                }
                    
                Image = new BitmapImage(new Uri(uriPath));
            }
        }

        public void Complete(TextArea textArea, ISegment completionSegment, EventArgs insertionRequestEventArgs) =>
            textArea.Document.Replace(completionSegment, Text);
    }
}
