namespace NmeaParserConsole.MessageData
{ 
    public class FieldTypeFloat : IPrintableData
    {
        private float _floatValue;
        private string _description;

        public FieldTypeFloat(float floatValue, string description)
        {
            _floatValue = floatValue;
            _description = description;
        }

        public string GetPrintData()
        {
            return _description +_floatValue;
        }
    }
}
