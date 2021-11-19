using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace SchtormRun
{
    public class KillingProcessesAutoCompleteRule : IRule
    {
        public string LastInputCharacter { get; set; } = " ";
        public List<string> AutocompleteWords { get; set; } = new List<string>();
        public List<string> AutocompleteDescriptions { get; set; } = new List<string>();
        public List<string> AutocompleteIcons { get; set; } = new List<string>();

        public bool PreviousInputSuits(string input)
        {
            AutocompleteWords.Clear();
            AutocompleteDescriptions.Clear();
            AutocompleteIcons.Clear();

            if (!Regex.IsMatch(input, "^\\s*kill"))
                return false;

            var allRunningProcesses = Process.GetProcesses();

            foreach (var process in allRunningProcesses)
            {
                AutocompleteWords.Add(process.ProcessName);
                AutocompleteDescriptions.Add($"id: {process.Id}");

                AutocompleteIcons.Add("");
            }    

            return true;
        }
    }
}
