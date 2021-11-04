using System;
using System.Windows;
using System.Windows.Controls;

using SchtormRun.Properties;

namespace SchtormRun.Controls.Windows
{
    /// <summary>
    /// Логика взаимодействия для SubWindow.xaml
    /// </summary>
    public partial class SubWindow : Window
    {
        public SubWindow()
        {
            InitializeComponent();
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
            AdditionalFunctionalityFrame.Navigate(page);
        }
    }
}
