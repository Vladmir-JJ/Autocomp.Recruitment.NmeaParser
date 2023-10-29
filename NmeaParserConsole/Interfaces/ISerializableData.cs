namespace NmeaParserConsole.Interfaces
{
    public interface ISerializableData
    {
        /// <summary>
        /// Returns NMEA header for NMEA message data or enum type for extra data
        /// </summary>
        public string GetIdentifier();
    }
}