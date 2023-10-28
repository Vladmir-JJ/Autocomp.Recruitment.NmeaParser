using Autocomp.Nmea.Common;
using NmeaParserConsole.Data.SerializableData.HeaderDefinition;
using NmeaParserConsole.JsonUtilities;
using System.Text.RegularExpressions;

namespace NmeaParserConsole.Data
{
    public class NmeaDataValidator
    {
        public NmeaMessageData? GetValidNmeaMessageData(NmeaMessage message)
        {
            HeaderDefinitionData? fieldsData = SentenceFormatterImporter.GetDataFromFile(message.Header, ImportedData.HeaderDefinitions) as HeaderDefinitionData;
            if (fieldsData == null)
            {
                Console.WriteLine($"[{this}] unsupported header: {message.Header}");
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
                Console.WriteLine($"[{this}] invalid field count: [{fields.Length}], expected: [{expectedFields.Count}]");
                return false;
            }
            for (int i = 0; i < fields.Length; i++)
            {
                if (!ValidateField(fields[i], expectedFields[i].Format))
                {
                    //TODO: error
                    Console.WriteLine($"[{this}] invalid format {fields[i]} at field of index [{i}]");
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