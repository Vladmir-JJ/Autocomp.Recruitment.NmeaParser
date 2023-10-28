using System.Text.Json.Serialization;
using NmeaParserConsole.Interfaces;

namespace NmeaParserConsole.Data.SerializableData.HeaderDefinition
{
    [Serializable]
    public class HeaderDefinitionData : ISerializableData
    {
        public string Header { get; set; }
        public string MessageDescription { get; set; }
        public string MessageFormat { get; set; }
        public List<HeaderDefinitionFieldCharacteristics> RequiredFields { get; set; }

        [JsonConstructor]
        public HeaderDefinitionData(string header, string messageDescription, string messageFormat, List<HeaderDefinitionFieldCharacteristics> requiredFields)
        {
            Header = header;
            MessageDescription = messageDescription;
            MessageFormat = messageFormat;
            RequiredFields = requiredFields;
        }

        public string GetIdentifier() => Header;
    }
}
