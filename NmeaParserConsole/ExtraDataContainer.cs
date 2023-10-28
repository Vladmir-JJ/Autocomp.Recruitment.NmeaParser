using System.Text.Json.Serialization;

namespace NmeaParserConsole
{
    public enum ExtraDataType
    {
        None = 0,
        Status,
        ModeIndicator,
        Reference,
        CardinalDirections,
        WindSpeedUnits
    }

    [Serializable]
    public class ExtraDataContainer : IStorableData
    {
        public string ExtraDataType;
        public List<ExtraData> ExtraInfo;

        [JsonConstructor]
        public ExtraDataContainer(string ExtraDataType, List<ExtraData> ExtraInfo)
        {
            this.ExtraDataType = ExtraDataType;
            this.ExtraInfo = ExtraInfo;
        }

        public string GetIdentifier() => ExtraDataType;
    }

    [Serializable]
    public class ExtraData
    {
        public string Key;
        public string Value;

        public ExtraData(string Key, string Value)
        {
            this.Key = Key;
            this.Value = Value;
        }
    }
}