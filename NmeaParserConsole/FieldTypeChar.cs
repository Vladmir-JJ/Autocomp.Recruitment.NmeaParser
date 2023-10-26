using NmeaParserConsole.MessageData;

namespace NmeaParserConsole
{
    internal class FieldTypeChar : IPrintableData
    {
        private char _char;
        private string _description;

        public FieldTypeChar(char c, string description)
        {
            _char = c;
            _description = description;
        }

        public string GetPrintData()
        {
            return _description + _char;
        }
    }
}
