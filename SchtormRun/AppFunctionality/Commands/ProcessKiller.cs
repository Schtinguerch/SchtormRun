using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ScriptMaker;
using Localization = SchtormRun.Resources.Dictionaries.Localization.Resources;

namespace SchtormRun.AppFunctionality.Commands
{
    public class ProcessKiller : Command
    {
        public override string Name { get; set; } = "kill";

        public override void Run(List<string> arguments, object processingObject = null)
        {
            SetAdditional(arguments);

            if (arguments.First() == "id")
            {
                int wantedProcessId = int.Parse(SubCommandArguments.GlueToString());
                var runningProcess = Process.GetProcessById(wantedProcessId);

                runningProcess.Kill();
                return;
            }

            var wantedProcess = UnitedStringArgument;
            var runningProcesses = Process.GetProcessesByName(wantedProcess);

            if (runningProcesses == null || runningProcesses.Length == 0)
                throw new Exception($"{Localization.Error}: {Localization.ProcessesNotFound}");

            KillProcesses(runningProcesses);
        }

        private async void KillProcesses(IEnumerable<Process> runningProcesses)
        {
            await Task.Run(() =>
            {
                foreach (var runningProcess in runningProcesses)
                {
                    try
                    {
                        runningProcess.Kill();
                    }

                    catch { }
                }
            });
        }
    }
}
