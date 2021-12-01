using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Navigation;

using SchtormRun.Properties;
using SchtormRun.Controls.Pages.SubWindowPages;

namespace SchtormRun.Controls.Windows
{
    /// <summary>
    /// Логика взаимодействия для SubWindow.xaml
    /// </summary>
    public partial class SubWindow : Window
    {
        private Page _lastOpenedPage;

        public SubWindow()
        {
            InitializeComponent();

            PreviewKeyDown += (s, e) =>
            {
                if (Keyboard.Modifiers == ModifierKeys.Alt && e.SystemKey == Key.Space)
                    e.Handled = true;
            };
        }

        public void Display()
        {
            Show();
            
            Left = CenterNode.AppWindow.Left;
            Width = CenterNode.AppWindow.Width;

            Top = CenterNode.AppWindow.Top + CenterNode.AppWindow.ActualHeight + Settings.Default.AppBorderRadius;
        }

        public void OpenPage(Page page)
        {
            if (_lastOpenedPage != null && _lastOpenedPage is WebViewPage)
                (_lastOpenedPage as WebViewPage).DisposeWebPage();

            Display();

            AdditionalFunctionalityFrame.Navigate(page);
            _lastOpenedPage = page;
        }

        public void ClearAndHide()
        {
            if (Visibility != Visibility.Hidden)
            {
                if (_lastOpenedPage != null && _lastOpenedPage is WebViewPage)
                    (_lastOpenedPage as WebViewPage).DisposeWebPage();

                Hide();
            }
        }
    }
}
