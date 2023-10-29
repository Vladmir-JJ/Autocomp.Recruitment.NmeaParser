using NmeaParserConsole.ConsoleInterface;
using NmeaParserConsole.Data.DataFields;
using NmeaParserConsole.Data.SerializableData.ExtraData;
using NmeaParserConsole.Data.SerializableData.HeaderDefinition;
using NmeaParserConsole.JsonUtilities;
using System.Globalization;

namespace NmeaParserConsole.Data
{
    public class NmeaMessageDataFieldFactory
    {
        /// <summary>
        /// Parses field data to provided format previously checked by RegEx.
        /// </summary>
        public AbstractNmeaField? CreateField(HeaderDefinitionFieldCharacteristics fieldInfo, string fieldValue)
        {
            ExtraDataContainer? extraData = GetExtraData(fieldInfo);
            string fieldType = fieldInfo.FieldType;

            if (fieldType == typeof(float).Name)
            {
                if (float.TryParse(fieldValue, NumberStyles.Any, CultureInfo.InvariantCulture, out float value))
                    return new FieldTypeFloat(value, fieldInfo.Description, extraData);
                else
                    LogErrorViaLogger(fieldType, typeof(float).ToString());              
            }
            if (fieldType == typeof(char).Name)
            {
                var c = char.Parse(fieldValue);
                return new FieldTypeChar(c, fieldInfo.Description, extraData);
            }

            ConsoleErrorLogger.LogError(this, ConsoleMessageLibrary.ERROR_NO_VALID_FIELD, fieldInfo.FieldType);
            return null;
        }

        /// <summary>
        /// Returns extra data for describing NMEA acronyms.
        /// </summary>
        private ExtraDataContainer? GetExtraData(HeaderDefinitionFieldCharacteristics fieldInfo)
        {
            if (fieldInfo.ExtraData == ExtraDataType.None.ToString())
                return null;
            ExtraDataContainer? fieldsData = SentenceFormatterImporter.GetDataFromFile(fieldInfo.ExtraData, ImportedData.ExtraData) as ExtraDataContainer;
            if (fieldsData == null)
            {
                ConsoleErrorLogger.LogError(this, ConsoleMessageLibrary.ERROR_UNKNOWN_EXTRA_DATA, fieldInfo.ExtraData);
                return null;
            }
            return fieldsData;
        }

        /// <summary>
        /// Calls error logger to siglan parsing errors.
        /// </summary>
        private void LogErrorViaLogger(string fieldType, string parsingType)
        {
            ConsoleErrorLogger.LogError(this, ConsoleMessageLibrary.PARSE_ERROR, fieldType, parsingType);
        }
    }
}
