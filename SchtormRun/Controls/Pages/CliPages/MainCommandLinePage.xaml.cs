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
                    command.Run(CommandTextEditor.Text);
                }
                
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
    }
}
