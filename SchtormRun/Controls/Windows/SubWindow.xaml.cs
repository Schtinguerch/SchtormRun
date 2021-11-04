using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
    }
}
