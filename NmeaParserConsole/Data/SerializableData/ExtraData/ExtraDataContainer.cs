using System.Text.Json.Serialization;
using NmeaParserConsole.Interfaces;

namespace NmeaParserConsole.Data.SerializableData.ExtraData
{
    [Serializable]
    public class ExtraDataContainer : ISerializableData
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
}