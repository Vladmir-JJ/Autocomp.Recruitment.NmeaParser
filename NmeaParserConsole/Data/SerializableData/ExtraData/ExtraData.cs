namespace NmeaParserConsole.Data.SerializableData.ExtraData
{
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