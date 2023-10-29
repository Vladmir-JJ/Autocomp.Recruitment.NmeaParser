using NmeaParserConsole.Data.SerializableData.ExtraData;

namespace NmeaParserConsole.Data.DataFields
{
    public abstract class AbstractNmeaField
    {
        protected ExtraDataContainer? _extraData;
        protected string _description;

        public AbstractNmeaField(string description, ExtraDataContainer? extraData)
        {
            _description = description;
            _extraData = extraData;
        }

        /// <summary>
        /// Returns data for console print
        /// </summary>
        public abstract string GetPrintData();

        /// <summary>
        /// Returns extra data describing acronyms (i.e. A = status valid), if extra data is defined in ExtraData.json and ExtraDataType.cs
        /// </summary>
        public virtual string GetExtraData(string input)
        {
            if (_extraData == null)
                return "";
            var data = _extraData.ExtraInfo.FirstOrDefault(d => d.Key == input);
            if (data != default)
                return data.Value;
            return "";
        }
    }
}