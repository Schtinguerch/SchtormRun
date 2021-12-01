using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchtormRun
{
    public class FileAutoCompleteRule : IRule
    {
        public string LastInputCharacter { get; set; } = "\\";
        public List<string> AutocompleteWords { get; set; } = new List<string>();
        public List<string> AutocompleteDescriptions { get; set; } = new List<string>();
        public List<string> AutocompleteIcons { get; set; } = new List<string>();

        public bool PreviousInputSuits(string input)
        {
            var baseDirectory = "";

            try
            {
                input = input.Preprocessed().Trim(' ');
                baseDirectory = input.Substring(input.IndexOf(':') - 1);
            }

            catch
            {
                return false;
            }
            

            string[] files, folders;

            try
            {
                files = Directory.GetDirectories(baseDirectory);
                folders = Directory.GetFiles(baseDirectory);

                AutocompleteWords.Clear();
                AutocompleteDescriptions.Clear();
                AutocompleteIcons.Clear();
            }
            
            catch
            {
                return false;
            }

            foreach (var folder in folders)
            {
                var folderInfo = new FileInfo(folder);

                AutocompleteWords.Add(folderInfo.Name);
                AutocompleteIcons.Add(">" + folderInfo.FullName);
            }

            foreach (var file in files)
            {
                var fileInfo = new FileInfo(file);

                AutocompleteWords.Add(fileInfo.Name);
                AutocompleteIcons.Add(">" + fileInfo.FullName);
            }

            while (AutocompleteDescriptions.Count < AutocompleteWords.Count)
                AutocompleteDescriptions.Add("");

            while (AutocompleteIcons.Count < AutocompleteWords.Count)
                AutocompleteIcons.Add("");

            return true;
        }
    }
}
