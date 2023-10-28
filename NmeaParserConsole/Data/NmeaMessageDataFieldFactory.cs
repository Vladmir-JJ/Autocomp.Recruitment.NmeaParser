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
        public AbstractNmeaField? CreateField(HeaderDefinitionFieldCharacteristics fieldInfo, string fieldValue)
        {
            ExtraDataContainer? extraData = GetExtraData(fieldInfo);
            string fieldType = fieldInfo.FieldType;

            if (fieldType == typeof(float).Name)
            {                
                var value = float.Parse(fieldValue, CultureInfo.InvariantCulture);
                return new FieldTypeFloat(value, fieldInfo.Description, extraData);
            }
            if (fieldType == typeof(char).Name)
            {
                var c = char.Parse(fieldValue);
                return new FieldTypeChar(c, fieldInfo.Description, extraData);
            }

            ConsoleErrorLogger.LogError(this, ConsoleMessageLibrary.ERROR_NO_VALID_FIELD, fieldInfo.FieldType);
            return null;
        }

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
    }
}
