using NmeaParserConsole.MessageData;
using System.Globalization;

namespace NmeaParserConsole
{
    public class NmeaFieldFactory
    {
        public AbstractNmeaField? CreateField(FieldCharacteristics fieldInfo, string fieldValue)
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



            Console.WriteLine($"[{this}] no valid field type found for type {fieldInfo.FieldType}");
            return null;
        }

        private ExtraDataContainer? GetExtraData(FieldCharacteristics fieldInfo)
        {
            if (fieldInfo.ExtraData == ExtraDataType.None.ToString())
                return null;
            ExtraDataContainer? fieldsData = SentenceFormatterImporter.GetDataFromFile(fieldInfo.ExtraData, ImportedData.ExtraData) as ExtraDataContainer;
            if (fieldsData == null)
            {
                //TODO: move to logger?
                Console.WriteLine($"[{this}] unknown extra data type: {fieldInfo.ExtraData}");
                return null;
            }
            return fieldsData;
        }

        private void LogError(string fieldValue, string fieldType)
        {
            Console.WriteLine($"[{this}] parsing of {fieldValue} to {fieldType} failed");
        }
    }
}
