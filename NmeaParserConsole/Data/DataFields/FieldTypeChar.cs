using NmeaParserConsole.Data.SerializableData.ExtraData;

namespace NmeaParserConsole.Data.DataFields
{
    public class FieldTypeChar : AbstractNmeaField
    {
        private char _char;

        public FieldTypeChar(char c, string description, ExtraDataContainer? extraData) : base(description, extraData)
        {
            _char = c;
        }

        public override string GetPrintData()
        {
            return _description + _char + " " + GetExtraData(_char.ToString());
        }
    }
}
