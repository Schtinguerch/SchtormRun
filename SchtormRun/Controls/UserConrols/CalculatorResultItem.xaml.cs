using System;
using System.Windows;
using System.Windows.Controls;

namespace SchtormRun.Controls.UserConrols
{
    /// <summary>
    /// Логика взаимодействия для CalculatorResultItem.xaml
    /// </summary>
    public partial class CalculatorResultItem : UserControl
    {
        public delegate void EditReadyHandler(string expression);
        public event EditReadyHandler OnEditReady;

        private string _expression;

        public CalculatorResultItem(string expresssionWithResult)
        {
            InitializeComponent();
            
            _expression = expresssionWithResult.Split('=')[0];
            ExpressionTextEditor.Text = expresssionWithResult;
        }

        private void CopyExpressionButton_Click(object sender, RoutedEventArgs e) =>
            Clipboard.SetText(_expression);

        private void EditExpressionButton_Click(object sender, RoutedEventArgs e) =>
            OnEditReady?.Invoke(_expression);
    }
}
