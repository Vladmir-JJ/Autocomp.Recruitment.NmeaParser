using NmeaParserConsole.Data.SerializableData.ExtraData;

namespace NmeaParserConsole.Interfaces
{
    public interface IExtraData
    {
        ExtraDataContainer ExtraData { get; set; }

        public string GetExtraData();
    }
}
