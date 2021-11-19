using System;
using System.Collections.Generic;

namespace SchtormRun
{
    public class AbbreviationAutoCompleteRule : IRule
    {
        public string LastInputCharacter { get; set; } = "!";
        public List<string> AutocompleteWords { get; set; } = new List<string>();
        public List<string> AutocompleteDescriptions { get; set; } = new List<string>();
        public List<string> AutocompleteIcons { get; set; } = new List<string>();

        public bool PreviousInputSuits(string input) => true;

        public AbbreviationAutoCompleteRule()
        {
            AutocompleteWords.Clear();
            AutocompleteDescriptions.Clear();
            AutocompleteIcons.Clear();

            foreach (var word in CenterNode.PreprocessorReplacement.Keys)
                AutocompleteWords.Add(word.Substring(1));

            while (AutocompleteDescriptions.Count < AutocompleteWords.Count) {
                AutocompleteDescriptions.Add("");
                AutocompleteIcons.Add("");
            }
        }
    }
}
