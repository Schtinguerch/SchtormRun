using System;
using System.Windows.Input;
using System.Collections.Generic;
using System.Windows.Controls;
using ScriptMaker;
using SchtormRun.AppFunctionality.Commands;
using System.Windows;

namespace SchtormRun.Controls.Pages.CliPages
{
    /// <summary>
    /// Логика взаимодействия для MainCommandLinePage.xaml
    /// </summary>
    public partial class MainCommandLinePage : Page
    {
        private readonly CommandProcessor Command = new CommandProcessor(new List<Command>()
        {
            new RunApplication(),
            new GoogleTranslator(),
            new ProcessKiller(),
        });

        private readonly List<IRule> AutocompletionRules = new List<IRule>()
        {
            new FileAutoCompleteRule(),
            new AbbreviationAutoCompleteRule(),
            new KillingProcessesAutoCompleteRule(),
        };

        private readonly Dictionary<string, Page> SpecialPageCommands = new Dictionary<string, Page>()
        {
            { "=|calc", new CalculatorCommandLinePage() },
        };

        public MainCommandLinePage()
        {
            InitializeComponent();
            CenterNode.AppWindow.OnCommandLineShown += BaseWindowShown;

            var completer = new AutoCompleter(CommandTextEditor, AutocompletionRules);
            var switcher = new SpecialCommandPageSwitcher(CommandTextEditor, SpecialPageCommands);
        }

        private void BaseWindowShown()
        {
            CommandTextEditor.Focus();
        }

        private void Page_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;

                try
                {
                    var expression = CommandTextEditor.Text.Preprocessed();

                    CenterNode.CommandHistory.AddCommand(expression);
                    Command.Run(expression);
                }
                
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void PreviousCommandButton_Click(object sender, RoutedEventArgs e) =>
            CommandTextEditor.Text = CenterNode.CommandHistory.PreviousCommand();

        private void NextCommandButton_Click(object sender, RoutedEventArgs e) =>
            CommandTextEditor.Text = CenterNode.CommandHistory.NextCommand();

        private void CommandTextEditor_TextChanged(object sender, EventArgs e) =>
            CenterNode.SubWindow.Hide();
    }
}
