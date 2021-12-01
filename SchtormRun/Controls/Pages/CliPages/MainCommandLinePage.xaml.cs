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
        private CommandProcessor _command = new CommandProcessor(new List<Command>()
        {
            new RunApplication(),
            new GoogleTranslator(),
            new ProcessKiller(),
        });

        private List<IRule> _autocompletionRules = new List<IRule>()
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

            var completer = new AutoCompleter(CommandTextEditor, _autocompletionRules);
            var switcher = new SpecialCommandPageSwitcher(CommandTextEditor, SpecialPageCommands);

             foreach (var command in CenterNode.CustomCommandsDictionary)
                _command.Commands.Add(new CustomCommand(command.Key, command.Value));
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
                    _command.Run(expression);
                }
                
                catch (Exception ex)
                {
                    CenterNode.NotificationWindow.ShowError(ex.Message);
                }
            }
        }

        private void PreviousCommandButton_Click(object sender, RoutedEventArgs e) =>
            CommandTextEditor.Text = CenterNode.CommandHistory.PreviousCommand();

        private void NextCommandButton_Click(object sender, RoutedEventArgs e) =>
            CommandTextEditor.Text = CenterNode.CommandHistory.NextCommand();

        private void CommandTextEditor_TextChanged(object sender, EventArgs e) =>
            CenterNode.SubWindow.ClearAndHide();
    }
}
