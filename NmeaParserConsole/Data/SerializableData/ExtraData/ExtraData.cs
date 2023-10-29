namespace NmeaParserConsole.Data.SerializableData.ExtraData
{
    [Serializable]
    public class ExtraData
    {
        public string Key { get; private set; }
        public string Value { get; private set; }

        public ExtraData(string key, string value)
        {
            this.Key = key;
            this.Value = value;
        }

        public static int GetFieldCount() => typeof(ExtraData).GetProperties().Count();
    }
}