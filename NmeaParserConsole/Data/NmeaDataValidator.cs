using Autocomp.Nmea.Common;
using NmeaParserConsole.ConsoleInterface;
using NmeaParserConsole.Data.SerializableData.HeaderDefinition;
using NmeaParserConsole.JsonUtilities;
using System.Text.RegularExpressions;
using static NmeaParserConsole.ConsoleInterface.ConsoleMessageLibrary;


namespace NmeaParserConsole.Data
{
    public class NmeaDataValidator
    {
        public NmeaMessageData? GetValidNmeaMessageData(NmeaMessage message)
        {
            HeaderDefinitionData? fieldsData = SentenceFormatterImporter.GetDataFromFile(message.Header, ImportedData.HeaderDefinitions) as HeaderDefinitionData;
            if (fieldsData == null)
            {
                ConsoleErrorLogger.LogError(this, ERROR_UNSUPPORTED_HEADER, message.Header);
                return null;
            }
            if (!ValidateFields(message.Fields, fieldsData.RequiredFields))
                return null;

            return new NmeaMessageData(message, fieldsData);
        }

        private bool ValidateFields(string[] fields, List<HeaderDefinitionFieldCharacteristics> expectedFields)
        {
            if (fields.Length != expectedFields.Count)
            {
                ConsoleErrorLogger.LogError(this, ERROR_INVALID_FIELD_COUNT, fields.Length.ToString(), expectedFields.Count.ToString());
                return false;
            }
            for (int i = 0; i < fields.Length; i++)
            {
                if (!ValidateField(fields[i], expectedFields[i].Format))
                {
                    ConsoleErrorLogger.LogError(this, ERROR_INVALID_FORMAT, fields[i], i.ToString());
                    return false;
                }
            }
            return true;
        }

        private bool ValidateField(string fieldValue, string expectedFormat)
        {
            if (Regex.Match(fieldValue, expectedFormat).Success)
            {
                return true;
            }
            return false;
        }
    }
}