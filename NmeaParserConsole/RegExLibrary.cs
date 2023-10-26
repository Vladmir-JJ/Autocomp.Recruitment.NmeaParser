namespace NmeaParserConsole
{
    internal static class RegExLibrary
    {
        // public const string EXP_FLOAT = @"^-?[0-9]\d*.\d*$";
        // public const string EXP_INT = @"^-?[0-9]*$";   



        //TODO: check docu if lowercase message indicator is acceptable (ie NS, Status, etc)
        public const string FORMAT_NUMERIC = @"^\-?\d[0-9]*(\.[0-9]*)?$";
        public const string FORMAT_LONG_LAT = @"^\d{1,4}(\.\d{1,2})?$";
        public const string FORMAT_NS = @"^[n,N,s,S]$";
        public const string FORMAT_EW = @"^[e,E,w,W]$";
        public const string FORMAT_UTC = @"^[0-2]?\d[0-5]\d[0-5]\d(\.\d{1,2})?$";
        public const string FORMAT_STATUS = @"[a,A,v,V]";
        public const string FORMAT_MODE_INDICATOR = @"[a,A,d,D,e,E,m,M,s,S,n,N]";
        public const string FORMAT_ANGLE = @"^[0-3]?\d?\d(\.\d{1,2})?$";
        public const string FORMAT_RELATIVE = @"^[r,R,t,T]$";
        public const string FORMAT_SPEED_UNITS = @"[k,K,m,M,n,N,s,S]";
        //public readonly static string EXP_INT = @"^[0-9]*$";

    }
}