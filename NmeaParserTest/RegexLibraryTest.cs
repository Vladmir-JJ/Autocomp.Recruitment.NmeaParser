using NmeaParserConsole;
using System.Text.RegularExpressions;

namespace NmeaParserTest
{
    public class RegexLibraryTest
    {
        //Comments contain documentation notes.

        [Theory]
        //Variable numbers x.x
        //Optional leading and trailing zeros.
        //The decimal point and associated decimal-fraction are optional if full resolution is not required.
        [InlineData("-1", RegExLibrary.FORMAT_NUMERIC)]
        [InlineData("-1.0", RegExLibrary.FORMAT_NUMERIC)]
        [InlineData("-1.00", RegExLibrary.FORMAT_NUMERIC)]
        [InlineData("00001", RegExLibrary.FORMAT_NUMERIC)]
        [InlineData("-00001.00", RegExLibrary.FORMAT_NUMERIC)]
        [InlineData("00001.", RegExLibrary.FORMAT_NUMERIC)]
        [InlineData("-00001.000000", RegExLibrary.FORMAT_NUMERIC)]


        //Longitude - latitude xxxxx.xx
        //3 fixed digits of degrees, 2 fixed digits of minutes and a variable number of digits for decimal-fraction of minutes.
        //Leading zeros always included for degrees and minutes to maintain fixed length.
        //The decimal point and associated decimal - fraction are optional if full resolution is not required.
        [InlineData("12345", RegExLibrary.FORMAT_LONG_LAT)]
        [InlineData("12345.1", RegExLibrary.FORMAT_LONG_LAT)]
        [InlineData("12345.12", RegExLibrary.FORMAT_LONG_LAT)]
        [InlineData("92345", RegExLibrary.FORMAT_LONG_LAT)]
        [InlineData("12345.0", RegExLibrary.FORMAT_LONG_LAT)]
        [InlineData("00005.", RegExLibrary.FORMAT_LONG_LAT)]
        [InlineData("00025.1", RegExLibrary.FORMAT_LONG_LAT)]
        [InlineData("00025.12", RegExLibrary.FORMAT_LONG_LAT)]

        // 2 fixed digits of hours, 2 fixed digits of minutes, 2 fixed digits of seconds and a variable number of digits for decimal - fraction of seconds.
        //Leading zeros always included for hours, minutes and seconds to maintain fixed length.
        //The decimal point and associated decimal - fraction are optional if full resolution is not required.    
        [InlineData("000000", RegExLibrary.FORMAT_UTC)]
        [InlineData("051205", RegExLibrary.FORMAT_UTC)]
        [InlineData("102418", RegExLibrary.FORMAT_UTC)]
        [InlineData("163535.231", RegExLibrary.FORMAT_UTC)]
        [InlineData("204751.1232222", RegExLibrary.FORMAT_UTC)]
        [InlineData("235842.2", RegExLibrary.FORMAT_UTC)]
        [InlineData("234941.00", RegExLibrary.FORMAT_UTC)]
        [InlineData("235927.", RegExLibrary.FORMAT_UTC)]

        //MWV Wind angle, 0 to 359 degrees x.x
        [InlineData("000", RegExLibrary.FORMAT_ANGLE)]
        [InlineData("000.", RegExLibrary.FORMAT_ANGLE)]
        [InlineData("000.0", RegExLibrary.FORMAT_ANGLE)]
        [InlineData("000.00", RegExLibrary.FORMAT_ANGLE)]
        [InlineData("000.0000000", RegExLibrary.FORMAT_ANGLE)]
        [InlineData("160.00", RegExLibrary.FORMAT_ANGLE)]
        [InlineData("300.0000000", RegExLibrary.FORMAT_ANGLE)]
        [InlineData("222.", RegExLibrary.FORMAT_ANGLE)]
        [InlineData("00", RegExLibrary.FORMAT_ANGLE)]
        [InlineData("1", RegExLibrary.FORMAT_ANGLE)]
        [InlineData("21.2", RegExLibrary.FORMAT_ANGLE)]
        [InlineData("1.200", RegExLibrary.FORMAT_ANGLE)]
        [InlineData("1.", RegExLibrary.FORMAT_ANGLE)]

        //Wind speed units, K/M/N/S
        [InlineData("K", RegExLibrary.FORMAT_SPEED_UNITS)]
        [InlineData("M", RegExLibrary.FORMAT_SPEED_UNITS)]
        [InlineData("N", RegExLibrary.FORMAT_SPEED_UNITS)]
        [InlineData("S", RegExLibrary.FORMAT_SPEED_UNITS)]

        //Directions NS
        [InlineData("N", RegExLibrary.FORMAT_NS)]
        [InlineData("S", RegExLibrary.FORMAT_NS)]

        //Directions EW
        [InlineData("E", RegExLibrary.FORMAT_EW)]
        [InlineData("W", RegExLibrary.FORMAT_EW)]

        //Status
        [InlineData("A", RegExLibrary.FORMAT_STATUS)]
        [InlineData("V", RegExLibrary.FORMAT_STATUS)]

        //Wind direction relativeness [MWV]
        [InlineData("R", RegExLibrary.FORMAT_RELATIVE)]
        [InlineData("T", RegExLibrary.FORMAT_RELATIVE)]

        //Mode indicator [GLL]
        [InlineData("A", RegExLibrary.FORMAT_MODE_INDICATOR)]
        [InlineData("D", RegExLibrary.FORMAT_MODE_INDICATOR)]
        [InlineData("E", RegExLibrary.FORMAT_MODE_INDICATOR)]
        [InlineData("M", RegExLibrary.FORMAT_MODE_INDICATOR)]
        [InlineData("S", RegExLibrary.FORMAT_MODE_INDICATOR)]
        [InlineData("N", RegExLibrary.FORMAT_MODE_INDICATOR)]
        public void RegEx_ForProperInput_ValidatesTrue(string fieldValue, string expetedFormat)
        {
            Assert.True(Regex.Match(fieldValue, expetedFormat).Success);
        }

        [Theory]
        [InlineData("aaaa", RegExLibrary.FORMAT_NUMERIC)]
        [InlineData("213a22", RegExLibrary.FORMAT_NUMERIC)]
        [InlineData("12234.a2", RegExLibrary.FORMAT_NUMERIC)]
        [InlineData("12234.a2", RegExLibrary.FORMAT_LONG_LAT)]
        [InlineData("12234.ac", RegExLibrary.FORMAT_LONG_LAT)]
        [InlineData("12s34", RegExLibrary.FORMAT_LONG_LAT)]
        [InlineData("xxxxx.xx", RegExLibrary.FORMAT_LONG_LAT)]
        [InlineData("2b2", RegExLibrary.FORMAT_ANGLE)]
        [InlineData("222.c2", RegExLibrary.FORMAT_ANGLE)]
        [InlineData("2?2.dd", RegExLibrary.FORMAT_ANGLE)]
        [InlineData("hh2137", RegExLibrary.FORMAT_UTC)]
        [InlineData("hhmm37.123", RegExLibrary.FORMAT_UTC)]
        [InlineData("162069.1s3", RegExLibrary.FORMAT_UTC)]

        public void RegEx_ForNumericFormats_InputWithChars_ValidatesFalse(string fieldValue, string expetedFormat)
        {
            Assert.False(Regex.Match(fieldValue, expetedFormat).Success);
        }

        [Theory]
        [InlineData("s", RegExLibrary.FORMAT_RELATIVE)]
        [InlineData("2", RegExLibrary.FORMAT_RELATIVE)]
        [InlineData("E", RegExLibrary.FORMAT_NS)]
        [InlineData("1", RegExLibrary.FORMAT_NS)]
        [InlineData("s", RegExLibrary.FORMAT_EW)]
        [InlineData("h", RegExLibrary.FORMAT_EW)]
        [InlineData("!", RegExLibrary.FORMAT_STATUS)]
        [InlineData("x", RegExLibrary.FORMAT_STATUS)]
        [InlineData("Y", RegExLibrary.FORMAT_MODE_INDICATOR)]
        [InlineData("q", RegExLibrary.FORMAT_MODE_INDICATOR)]
        [InlineData("A", RegExLibrary.FORMAT_SPEED_UNITS)]
        [InlineData("k", RegExLibrary.FORMAT_SPEED_UNITS)]
        public void RegEx_ForNonNumericInputs_InputUnsupportedChar_ValidatesFalse(string fieldValue, string expetedFormat)
        {
            Assert.False(Regex.Match(fieldValue, expetedFormat).Success);
        }

        [Theory]
        [InlineData("RT", RegExLibrary.FORMAT_RELATIVE)]
        [InlineData("2T", RegExLibrary.FORMAT_RELATIVE)]
        [InlineData("Esda", RegExLibrary.FORMAT_NS)]
        [InlineData("N222", RegExLibrary.FORMAT_NS)]
        [InlineData("WWW", RegExLibrary.FORMAT_EW)]
        [InlineData("E a", RegExLibrary.FORMAT_EW)]
        [InlineData("VRA", RegExLibrary.FORMAT_STATUS)]
        [InlineData("SS", RegExLibrary.FORMAT_STATUS)]
        [InlineData("ASD", RegExLibrary.FORMAT_MODE_INDICATOR)]
        [InlineData("qKMN", RegExLibrary.FORMAT_MODE_INDICATOR)]
        [InlineData("KMNS", RegExLibrary.FORMAT_SPEED_UNITS)]
        [InlineData("kS", RegExLibrary.FORMAT_SPEED_UNITS)]
        public void RegEx_ForCharInputs_InputLongerThanOneChar_ValidatesFalse(string fieldValue, string expetedFormat)
        {
            Assert.False(Regex.Match(fieldValue, expetedFormat).Success);
        }

        [Theory]
        [InlineData("", RegExLibrary.FORMAT_RELATIVE)]
        [InlineData(" \n ", RegExLibrary.FORMAT_UTC)]
        [InlineData("   ", RegExLibrary.FORMAT_NUMERIC)]
        [InlineData("\n", RegExLibrary.FORMAT_EW)]
        [InlineData("", RegExLibrary.FORMAT_ANGLE)]
        [InlineData("", RegExLibrary.FORMAT_LONG_LAT)]
        public void RegEx_ForAllInputs_EmptyString_ValidatesFalse(string fieldValue, string expectedFormat)
        {
            Assert.False(Regex.Match(fieldValue, expectedFormat).Success);
        }
    }
}