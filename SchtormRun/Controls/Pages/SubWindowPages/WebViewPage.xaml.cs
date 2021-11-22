using System;
using System.Windows.Controls;

namespace SchtormRun.Controls.Pages.SubWindowPages
{
    /// <summary>
    /// Логика взаимодействия для WebViewPage.xaml
    /// </summary>
    public partial class WebViewPage : Page
    {
        public WebViewPage(string path)
        {
            InitializeComponent();
            WebViewElement.Source = new Uri(path);
        }
    }
}
