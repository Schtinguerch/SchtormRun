using System;
using System.IO.MemoryMappedFiles;
using SchtormRunExchange.Properties;

namespace SchtormRunExchange
{
    public class SharedVariable
    {
        public delegate void WrittenHandler(SharedVariable sender);
        public event WrittenHandler Written;

        private string _variableName;
        private MemoryMappedFile _sharedMemory;

        public string Value
        {
            get
            {
                try
                {
                    var sharedMemory = MemoryMappedFile.OpenExisting(_variableName);
                    int valueLength = 0;

                    using (MemoryMappedViewAccessor accessor = sharedMemory.
                        CreateViewAccessor(0, 4, MemoryMappedFileAccess.Read))
                    {
                        valueLength = accessor.ReadInt32(0);
                    }

                    using (MemoryMappedViewAccessor accessor = sharedMemory.
                        CreateViewAccessor(4, valueLength * 2, MemoryMappedFileAccess.Read))
                    {
                        var charArrayValue = new char[valueLength];
                        accessor.ReadArray<char>(0, charArrayValue, 0, valueLength);

                        return new string(charArrayValue);
                    }
                }
                
                catch
                {
                    //The situation when the value was not set
                    return string.Empty;
                }
            }

            set
            {
                int capacity = value.Length * 2 + 4;
                _sharedMemory = MemoryMappedFile.CreateOrOpen(_variableName, capacity);

                using (MemoryMappedViewAccessor accessor = _sharedMemory.CreateViewAccessor(0, capacity))
                {
                    var charArrayValue = value.ToCharArray();

                    accessor.Write(0, value.Length);
                    accessor.WriteArray<char>(4, charArrayValue, 0, charArrayValue.Length);

                    Written?.Invoke(this);
                }
            }
        }

        public void RegisterShare()
        {
            Console.WriteLine($"<SchtormRun> \"{_variableName}\" shared");
            var outputSharedVariableFlag = new SharedVariable(Settings.Default.DataAccepted);

            //waiting for acception from SchtormRun
            while (outputSharedVariableFlag.Value != "true");
        }
            

        public SharedVariable(string variableName) =>
            _variableName = variableName;
    }
}
