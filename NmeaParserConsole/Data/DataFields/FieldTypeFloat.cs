using System.Globalization;
using NmeaParserConsole.Data.SerializableData.ExtraData;

namespace NmeaParserConsole.Data.DataFields
{
    public class FieldTypeFloat : AbstractNmeaField
    {
        private float _floatValue;

        public FieldTypeFloat(float floatValue, string description, ExtraDataContainer? extraData) : base(description, extraData)
        {
            _floatValue = floatValue;
        }

        public override string GetPrintData()
        {
            NumberFormatInfo nfi = new NumberFormatInfo();
            nfi.NumberDecimalSeparator = ".";
            return _description + _floatValue.ToString(nfi) + " " + GetExtraData(_floatValue.ToString());
        }
    }
}
