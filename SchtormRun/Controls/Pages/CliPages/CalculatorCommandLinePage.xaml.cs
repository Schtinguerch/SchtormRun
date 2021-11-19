using SchtormRun.Controls.Pages.SubWindowPages;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SchtormRun.Controls.Pages.CliPages
{
    /// <summary>
    /// Логика взаимодействия для CalculatorCommandLinePage.xaml
    /// </summary>
    public partial class CalculatorCommandLinePage : Page, ILoadable
    {
        public delegate void CalculationDoneHandler(string expression, double value = 0);
        public delegate void CalculationErrorHandler(string message);

        public event CalculationDoneHandler OnCalculationDone;
        public event CalculationErrorHandler OnCalculationError;

        private readonly MathCalculator _calculator = new MathCalculator();
        private bool _isRendered = false;

        public CalculatorCommandLinePage()
        {
            InitializeComponent();
            ExpressionTextEditor.Focus();

            CenterNode.AppWindow.OnCommandLineShown += () =>
            {
                if (_isRendered)
                    CenterNode.SubWindow.Show();

                ExpressionTextEditor.Focus();
            };
        }

        private void Page_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                try
                {
                    double value = _calculator.Compute(ExpressionTextEditor.Text);
                    OnCalculationDone?.Invoke(ExpressionTextEditor.Text, value);
                }

                catch (Exception ex)
                {
                    OnCalculationError?.Invoke(ex.Message);
                }
            }
        }

        private void CloseFrameButton_Click(object sender, RoutedEventArgs e)
        {
            _isRendered = false;

            CenterNode.AppWindow.MainCommandLineFrame.GoBack();
            CenterNode.SubWindow.Hide();
        }

        public void Load()
        {
            CenterNode.SubWindow.OpenPage(new CalculatorResultPage(this));
            _isRendered = true;
        }
    }
}
