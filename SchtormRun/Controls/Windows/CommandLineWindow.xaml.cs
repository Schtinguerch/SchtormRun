using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using gma.System.Windows;

namespace SchtormRun.Controls.Windows
{
    /// <summary>
    /// Логика взаимодействия для CommandLineWindow.xaml
    /// </summary>
    public partial class CommandLineWindow : Window
    {
        private readonly BackgroundKeyboardHook _hook = new BackgroundKeyboardHook();
        private bool _altPressed = false;
        private bool _spacePressed = false;

        public delegate void CommandLineShownHandler();
        public event CommandLineShownHandler OnCommandLineShown;

        public CommandLineWindow()
        {
            CenterNode.AppWindow = this;
            CenterNode.SubWindow = new SubWindow();
            CenterNode.NotificationWindow = new NotificationWindow();

            CenterNode.Initialize();
            InitializeComponent();
            
            _hook.KeyDown += BackgroundKeyDown;
            _hook.KeyUp += BackgroundKeyUp;
        }

        public void OpenPage(Page page) =>
            MainCommandLineFrame.Navigate(page);

        private void BackgroundKeyUp(object sender, System.Windows.Forms.KeyEventArgs e) =>
            SetPressedState(false, e);

        private void BackgroundKeyDown(object sender, System.Windows.Forms.KeyEventArgs e) =>
            SetPressedState(true, e);

        private void SetPressedState(bool value, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyValue == 164)
            {
                _altPressed = value;
                return;
            }

            if (e.KeyCode == System.Windows.Forms.Keys.Space)
            {
                _spacePressed = value;
            }

            if (_altPressed && _spacePressed)
            {
                _spacePressed = false;
                _altPressed = false;

                Show();
                OnCommandLineShown?.Invoke();
            }
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                if (Keyboard.Modifiers == ModifierKeys.Shift)
                {
                    if (!MainCommandLineFrame.CanGoBack)
                        return;

                    MainCommandLineFrame.GoBack();
                    return;
                }

                Hide();
                CenterNode.SubWindow.Hide();
            }

            if (Keyboard.Modifiers == ModifierKeys.Alt && e.SystemKey == Key.Space)
            {
                e.Handled = true;
            }
        }
    }
}
