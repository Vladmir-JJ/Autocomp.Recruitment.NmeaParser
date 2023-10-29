using System.Text.Json.Serialization;

namespace NmeaParserConsole.Data.SerializableData.HeaderDefinition
{
    [Serializable]
    public class HeaderDefinitionFieldCharacteristics
    {
        public string Description { get; set; }
        public string FieldType { get; set; }
        public string Format { get; set; }
        public string ExtraData { get; set; }

        [JsonConstructor]
        public HeaderDefinitionFieldCharacteristics(string Description, string FieldType, string Format, string ExtraData = "None")
        {
            this.Description = Description;
            this.FieldType = FieldType;
            this.Format = Format;
            this.ExtraData = ExtraData;
        }

        public static int GetFieldCount() => typeof(HeaderDefinitionFieldCharacteristics).GetProperties().Count();
    }
}
