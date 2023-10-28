namespace NmeaParserConsole
{
    public static class RegExLibrary
    {       
        //Numeric:
        public const string FORMAT_NUMERIC = @"^\-?\d[0-9]*(\.[0-9]*)?$";
        public const string FORMAT_LONG_LAT = @"^\d{5}\.?(\d{1,2})?$";
        public const string FORMAT_UTC = @"^([0-1][0-9]|2[0-3])[0-5]\d[0-5]\d(\.\d*)?$";
        public const string FORMAT_ANGLE = @"^[0-3]?\d?\d(\.\d*)?$";

        //Char type:
        public const string FORMAT_NS = @"^[N,S]$";
        public const string FORMAT_EW = @"^[E,W]$";
        public const string FORMAT_STATUS = @"^[A,V]$";
        public const string FORMAT_MODE_INDICATOR = @"^[A,D,E,M,S,N]$";
        public const string FORMAT_RELATIVE = @"^[R,T]$";
        public const string FORMAT_SPEED_UNITS = @"^[K,M,N,S]$"; 
    }
}