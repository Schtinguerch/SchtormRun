using System;
using System.Collections.Generic;

namespace SchtormRun
{
    public class CommandHistory
    {
        private List<string> _commandHistory;
        private int _chosenCommandIndex;

        public CommandHistory()
        {
            _commandHistory = new List<string>() { "" };
            _chosenCommandIndex = 0;
        }

        public string PreviousCommand()
        {
            if (_chosenCommandIndex > 0)
                _chosenCommandIndex--;

            return CurrentCommand();
        }

        public string NextCommand()
        {
            if (_chosenCommandIndex < _commandHistory.Count - 1)
                _chosenCommandIndex++;

            return CurrentCommand();
        }

        public string CurrentCommand() =>
            _commandHistory[_chosenCommandIndex];

        public void AddCommand(string command)
        {
            if (!_commandHistory.Contains(command))
            {
                int insertIndex = _chosenCommandIndex;

                if (insertIndex == _commandHistory.Count - 1)
                    _commandHistory.Add(command);
                else
                    _commandHistory.Insert(insertIndex, command);
            }
        }
    }
}
