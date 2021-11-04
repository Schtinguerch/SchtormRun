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
        private readonly CommandProcessor command = new CommandProcessor(new List<Command>()
        {
            new RunApplication(),
            new GoogleTranslator(),
        });

        public MainCommandLinePage()
        {
            InitializeComponent();
            CenterNode.AppWindow.OnCommandLineShown += BaseWindowShown;

            var completer = new AutoCompleter(CommandTextEditor,
                new List<IRule>()
                {
                    new FileAutoCompleteRule(),
                });
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
                    command.Run(expression);
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
