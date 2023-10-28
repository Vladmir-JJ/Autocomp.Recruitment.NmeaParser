﻿namespace NmeaParserConsole.MessageData
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

        public abstract string GetPrintData();

        public virtual string GetExtraData(string input)
        {
            if (_extraData == null)
                return "";
            input.ToUpper();
            var data = _extraData.ExtraInfo.FirstOrDefault(d => d.Key == input);
            if (data != default)
                return data.Value;
            return "";
        }
    }
}