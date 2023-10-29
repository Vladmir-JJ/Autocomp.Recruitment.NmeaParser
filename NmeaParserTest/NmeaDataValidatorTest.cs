using Autocomp.Nmea.Common;
using NmeaParserConsole.Data;

namespace NmeaParserTest
{
    public class NmeaDataValidatorTest
    {               
        [Theory]
        [InlineData("023", "R", "00123.22", "N", "A" )]
        [InlineData("123.00", "R", "232123", "K", "V")]
        [InlineData("223", "R", "023.2222", "M", "V")]
        [InlineData("333.2", "R", "123.22", "S", "A")]
        [InlineData("123", "R", "12233.20", "N", "A")]
        public void ProperNmeaMessage_MWV_ReturnsNmeaData(string arg1, string arg2, string arg3, string arg4, string arg5)
        {
            string[] fields = new string[] { arg1, arg2, arg3, arg4, arg5 };
            NmeaMessage message = new("MWV", fields);
            NmeaDataValidator validator = new();

            var data = validator.GetValidNmeaMessageData(message);

            Assert.NotNull(data);            
        }

        [Theory]
        [InlineData("11111.11", "N", "00022", "E", "121123.100", "A", "A")]
        [InlineData("13411.11", "S", "22122", "W", "121123.100", "V", "D")]
        [InlineData("11641.10", "N", "22222.12", "E", "121123.100", "V", "E")]
        [InlineData("00111.11", "S", "00022.00", "W", "121123.100", "V", "M")]
        [InlineData("00011.10", "N", "22222.21", "E", "121123.100", "A", "S")]
        public void ProperNmeaMessage_GLL_ReturnsNmeaData(string arg1, string arg2, string arg3, string arg4, string arg5, string arg6, string arg7)
        {
            string[] fields = new string[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7 };
            NmeaMessage message = new("GLL", fields);
            NmeaDataValidator validator = new();

            var data = validator.GetValidNmeaMessageData(message);

            Assert.NotNull(data);
        }

        [Theory]
        [InlineData("GLL", "11111.11", "N", "00022", "E", "121123.100", "A", "A", "XD")]
        [InlineData("GLL", "13411.11", "S", "22122", "W", "121123.100", "V", "D", "LMAO")]
        [InlineData("MWV", "111.10", "R", "22222.123", "K", "A", "V", "E", "ZSRR")]
        [InlineData("MWV", "011.11", "T", "00022.00", "M", "V", "V", "M", "ROTFL")]
        [InlineData("GLL", "00011.10", "N", "22222.21", "E", "121123.100", "A", "S", "OMG")]
        public void TooLargeFieldCount_ReturnsNull(string header, string arg1, string arg2, string arg3, string arg4, string arg5, string arg6, string arg7, string arg8)
        {
            string[] fields = new string[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8 };
            NmeaMessage message = new(header, fields);
            NmeaDataValidator validator = new();

            var data = validator.GetValidNmeaMessageData(message);

            Assert.Null(data);
        }

        [Theory]
        [InlineData("GLL", "11111.11", "N", "00022", "E")]
        [InlineData("GLL", "13411.11", "S", "22122", "W")]
        [InlineData("MWV", "116411.10", "N", "22222.123", "E")]
        [InlineData("MWV", "00111.11", "S", "00022.00", "W")]
        [InlineData("GLL", "00011.10", "N", "22222.21", "E")]
        public void TooSmallFieldCount_ReturnsNull(string header, string arg1, string arg2, string arg3, string arg4)
        {
            string[] fields = new string[] { arg1, arg2, arg3, arg4};
            NmeaMessage message = new(header, fields);
            NmeaDataValidator validator = new();

            var data = validator.GetValidNmeaMessageData(message);

            Assert.Null(data);
        }

        [Theory]
        [InlineData("GLL", "a", "N", "00022", "E", "121123.100", "A", "A")]
        [InlineData("GLL", "blabla", "S", "22122", "W", "121123.100", "V", "D")]
        [InlineData("GLL", "116411.10", "123", "22222.123", "E", "121123.100", "V", "E")]
        [InlineData("GLL", "00111.11", "S", "bla", "W", "121123.100", "123", "M")]
        [InlineData("GLL", "00011.10", "N", "22222.21", "E", "121123.100", "A", "aaaaa")]
        public void ImproperFieldType_ReturnsNull(string header, string arg1, string arg2, string arg3, string arg4, string arg5, string arg6, string arg7)
        {
            string[] fields = new string[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7 };
            NmeaMessage message = new(header, fields);
            NmeaDataValidator validator = new();

            var data = validator.GetValidNmeaMessageData(message);

            Assert.Null(data);
        }

        [Theory]
        [InlineData("423", "R", "00123.22", "N", "A")]
        [InlineData("123.00", "G", "232123", "K", "V")]
        [InlineData("223", "R", "023.2s22", "u", "V")]
        [InlineData("333.2", "R", "123.22", "S", "a")]
        [InlineData("1235", "R", "12233.20", "N", "A")]
        public void ImproperInputFormatForProperFieldType_ReturnsNull(string arg1, string arg2, string arg3, string arg4, string arg5)
        {
            string[] fields = new string[] { arg1, arg2, arg3, arg4, arg5 };
            NmeaMessage message = new("MWV", fields);
            NmeaDataValidator validator = new();

            var data = validator.GetValidNmeaMessageData(message);

            Assert.Null(data);
        }
    }
}
