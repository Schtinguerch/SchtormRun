using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;

using Localization = SchtormRun.Resources.Dictionaries.Localization.Resources;

namespace SchtormRun.Controls.Pages.CliPages
{
    /// <summary>
    /// Логика взаимодействия для TranslationResultPage.xaml
    /// </summary>
    public partial class TranslationResultPage : Page
    {
        private string _url;
        private string _text;

        public TranslationResultPage(string url, string text)
        {
            InitializeComponent();
            TranslationResultTextBox.Text = text;

            _url = url;
            _text = text;
        }

        private void CopyTranslationButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Clipboard.SetText(_text);
                CenterNode.NotificationWindow.ShowSuccess(Localization.TextCopiedToClipboard);
            }
            catch (Exception ex)
            {
                CenterNode.NotificationWindow.ShowError($"{Localization.Error}: {ex.Message}");
            }
        }

        private void EnterUrlButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(_url);
        }
    }
}
