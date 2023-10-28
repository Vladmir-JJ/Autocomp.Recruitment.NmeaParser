using Newtonsoft.Json;
using NmeaParserConsole.Data.SerializableData.ExtraData;
using NmeaParserConsole.Data.SerializableData.HeaderDefinition;
using NmeaParserConsole.Interfaces;

namespace NmeaParserConsole.JsonUtilities
{
    public enum ImportedData
    {
        HeaderDefinitions,
        ExtraData
    }

    internal static class SentenceFormatterImporter
    {
        public const string JSON_PATH = @"Json\";
        public const string HEADER_DEF = "HeaderDefinitions.json";
        public const string EXTRA_DATA = "ExtraData.json";

        public static ISerializableData? GetDataFromFile(string id, ImportedData dataType)
        {
            List<ISerializableData>? datas = GetAllDataOfType(dataType);
            if (datas == null)
            {
                //TODO log error
                Console.WriteLine($"ERROR - failed to deserialize file from {Path.Combine(JSON_PATH, GetFileName(dataType))}");
                return null;
            }
            var data = datas.FirstOrDefault(d => d.GetIdentifier() == id);
            if (data == default)
            {
                //TODO: log error
                Console.WriteLine($"Error - unsupported data id: {id}");
                return null;
            }
            return data;
        }

        public static List<ISerializableData>? GetAllDataOfType(ImportedData dataType)
        {
            string targetFile = Path.Combine(JSON_PATH, GetFileName(dataType));
            if (!File.Exists(targetFile))
            {
                //TODO: log error file doesnt exist
                return null;
            }

            string s = File.ReadAllText(targetFile);

            switch (dataType)
            {
                case ImportedData.HeaderDefinitions:
                    var loadable = JsonConvert.DeserializeObject<List<HeaderDefinitionData>>(s);
                    if (loadable != null)
                        return new(loadable);
                    break;
                case ImportedData.ExtraData:
                    var extraData = JsonConvert.DeserializeObject<List<ExtraDataContainer>>(s);
                    if (extraData != null)
                        return new(extraData);
                    break;
            }

            return null;   
        }

        private static string GetFileName(ImportedData dataType)
        {
            switch (dataType)
            {
                case ImportedData.HeaderDefinitions:
                    return HEADER_DEF;
                case ImportedData.ExtraData:
                    return EXTRA_DATA;
            }
            //TODO: Log Error
            return "";
        }
    }
}
