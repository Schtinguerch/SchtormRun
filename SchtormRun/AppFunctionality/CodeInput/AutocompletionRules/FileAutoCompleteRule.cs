using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchtormRun
{
    internal class FileAutoCompleteRule : IRule
    {
        public string LastInputCharacter { get; set; } = "\\";
        public List<string> AutocompleteWords { get; set; } = new List<string>();
        public List<string> AutocompleteDescriptions { get; set; } = new List<string>();
        public List<string> AutocompleteIcons { get; set; } = new List<string>();

        public bool PreviousInputSuits(string input)
        {
            input = input.Preprocessed().Trim(' ');
            var baseDirectory = input.Substring(input.IndexOf(' ') + 1);

            string[] files, folders;

            try
            {
                files = Directory.GetDirectories(baseDirectory);
                folders = Directory.GetFiles(baseDirectory);

                AutocompleteWords.Clear();
            }
            
            catch
            {
                return false;
            }

            foreach (var folder in folders)
                AutocompleteWords.Add(new FileInfo(folder).Name);

            foreach (var file in files)
                AutocompleteWords.Add(new FileInfo(file).Name);

            while (AutocompleteDescriptions.Count < AutocompleteWords.Count)
                AutocompleteDescriptions.Add("");

            while (AutocompleteIcons.Count < AutocompleteWords.Count)
                AutocompleteIcons.Add("");

            return true;
        }
    }
}
