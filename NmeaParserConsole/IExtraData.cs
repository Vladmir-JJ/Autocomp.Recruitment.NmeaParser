namespace NmeaParserConsole
{
    public interface IExtraData
    {
        ExtraDataContainer ExtraData { get; set; }

        public string GetExtraData();
    }
}
