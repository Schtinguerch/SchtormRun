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
using SchtormRun.Controls.Pages.CliPages;
using SchtormRun.Controls.UserConrols;

using Localization = SchtormRun.Resources.Dictionaries.Localization.Resources;

namespace SchtormRun.Controls.Pages.SubWindowPages
{
    /// <summary>
    /// Логика взаимодействия для CalculatorResultPage.xaml
    /// </summary>
    public partial class CalculatorResultPage : Page
    {
        private CalculatorCommandLinePage _parentPage;

        public CalculatorResultPage(CalculatorCommandLinePage parentPage)
        {
            InitializeComponent();
            _parentPage = parentPage;

            _parentPage.OnCalculationDone += CalculationDone;
            _parentPage.OnCalculationError += CalculationGotError;
        }

        private void CalculationDone(string expression, double value)
        {
            SymbolTextBoxRun.Text = "=";
            ResultTextBoxRun.Text = value.ToString();

            AddResultToHistory(expression, value);
        }

        private void CalculationGotError(string message)
        {
            SymbolTextBoxRun.Text = $"{Localization.Error}:";
            ResultTextBoxRun.Text = message;
        }

        private void AddResultToHistory(string expression, double value)
        {
            var expressionWithValue = $"{expression} = {value}";
            var resultItem = new CalculatorResultItem(expressionWithValue);

            resultItem.OnEditReady += (s) =>
            {
                _parentPage.ExpressionTextEditor.Text = s;
                _parentPage.ExpressionTextEditor.Focus();
            };

            CalculationResultsStackPanel.Children.Insert(0, resultItem);
        }

        private void ResultTextBoxRun_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Clipboard.SetText(ResultTextBoxRun.Text);
            CenterNode.NotificationWindow.ShowSuccess(Localization.TextCopiedToClipboard);
        }
    }
}
