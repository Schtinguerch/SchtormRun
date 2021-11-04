using System;
using System.Collections.Generic;
using System.Diagnostics;
using ScriptMaker;

namespace SchtormRun.AppFunctionality.Commands
{
    public class RunApplication : Command
    {
        public override string Name { get; set; } = "run";

        public override void Run(List<string> arguments, object processingObject = null)
        {
            SetAdditional(arguments);
            var tokens = UnitedStringArgument.Split(';');

            if (tokens.Length == 1)
            {
                Process.Start(tokens[0]);
                return;
            }

            Process.Start(tokens[0], tokens[1]);
            CenterNode.AppWindow.Hide();
        }
    }
}
