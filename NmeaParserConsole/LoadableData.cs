using System.Text.Json.Serialization;

namespace NmeaParserConsole
{
    [Serializable]
    public class LoadableData : IStorableData
    {      
        public string Header { get; set; }
        public List<FieldCharacteristics> RequiredFields { get; set; }

        [JsonConstructor]
        public LoadableData(string Header, List<FieldCharacteristics> RequiredFields)
        {
            this.Header = Header;
            this.RequiredFields = RequiredFields;
        }

        public string GetIdentifier() => Header;
    }

    [Serializable]
    public class FieldCharacteristics
    {
        public string Description { get; set; }
        public string FieldType { get; set; }
        public string Format { get; set; }
        public string ExtraData { get; set; }

        [JsonConstructor]
        public FieldCharacteristics(string Description, string FieldType, string Format, string ExtraData = "None")
        {
            this.Description = Description;
            this.FieldType = FieldType;
            this.Format = Format;
            this.ExtraData = ExtraData;
        }
    }
}
