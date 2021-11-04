using System;
using System.Collections.Generic;

namespace SchtormRun
{
    public interface IRule
    {
        bool PreviousInputSuits(string input);
        string LastInputCharacter { get; set; }
        List<string> AutocompleteWords { get; set; }
        List<string> AutocompleteDescriptions { get; set; }
        List<string> AutocompleteIcons { get; set; }
    }
}
