namespace NmeaParserConsole.Data.SerializableData.ExtraData
{
    /// <summary>
    /// Defines types of extra data displayed to user for NMEA char acronyms (i.e. Status contains description for letters A and V)
    /// </summary>
    public enum ExtraDataType
    {
        None = 0,
        Status,
        ModeIndicator,
        Reference,
        CardinalDirections,
        WindSpeedUnits,
        TestEnum,
        TestDev
    }
}