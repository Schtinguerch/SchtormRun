using System;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media;
using SchtormRun.Properties;

namespace SchtormRun.Controls.Pages.SubWindowPages
{
    /// <summary>
    /// Логика взаимодействия для MarkdownPage.xaml
    /// </summary>
    public partial class MarkdownPage : Page
    {
        public MarkdownPage(string path)
        {
            InitializeComponent();
            MarkdownViewer.Markdown = File.ReadAllText(path);
            MarkdownViewer.Document.Background = Brushes.White;

            MarkdownViewer.Document.FontSize = Settings.Default.AppSmallFontSize;
        }
    }
}
