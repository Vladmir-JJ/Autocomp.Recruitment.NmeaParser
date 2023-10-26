using NmeaParserConsole.MessageData;
using System.Globalization;

namespace NmeaParserConsole
{
    internal class NmeaFieldFactory
    {
        public IPrintableData? CreateField(LoadableData.FieldCharacteristics fieldInfo, string fieldValue)
        {
            string fieldType = fieldInfo.FieldType;
            if (fieldType == typeof(float).Name)
            {
                var value = float.Parse(fieldValue, CultureInfo.InvariantCulture);
                return new FieldTypeFloat(value, fieldInfo.Description);
            }
            if (fieldType == typeof(char).Name)
            {
                var c = char.Parse(fieldValue);
                return new FieldTypeChar(c, fieldInfo.Description);
            }

            Console.WriteLine($"[{this}] no valid field type found for type {fieldInfo.FieldType}");
            return null;
        }

        private void LogError(string fieldValue, string fieldType)
        {
            Console.WriteLine($"[{this}] parsing of {fieldValue} to {fieldType} failed");
        }
    }
}
