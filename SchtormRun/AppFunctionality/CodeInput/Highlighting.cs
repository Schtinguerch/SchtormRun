using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;

using System;
using System.Xml;
using SchtormRun.Properties;

namespace SchtormRun
{
    public static class Highlighting
    {
        public static IHighlightingDefinition FromFile(string path)
        {
            IHighlightingDefinition definition = null;

            using (var reader = new XmlTextReader(path))
            {
                definition = HighlightingLoader.Load(reader, HighlightingManager.Instance);
                reader.Close();
            }

            return definition;
        }

        public static IHighlightingDefinition StandardCommands =>
            FromFile(Settings.Default.StandardCommandsXmlPath);

        public static IHighlightingDefinition CalculatorCharacters =>
            FromFile(Settings.Default.CalculatorCharactersXmlPath);
    }
}
