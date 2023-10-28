using Newtonsoft.Json;
using NmeaParserConsole.ConsoleInterface;
using NmeaParserConsole.Data.SerializableData.ExtraData;
using NmeaParserConsole.Data.SerializableData.HeaderDefinition;
using NmeaParserConsole.Interfaces;
using static NmeaParserConsole.ConsoleInterface.ConsoleMessageLibrary;


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
                ConsoleErrorLogger.LogError(typeof(SentenceFormatterImporter), ERROR_FAILED_TO_DESERIALIZE, Path.Combine(JSON_PATH, GetFileName(dataType))); ;
                return null;
            }
            var data = datas.FirstOrDefault(d => d.GetIdentifier() == id);
            if (data == default)
            {
                ConsoleErrorLogger.LogError(typeof(SentenceFormatterImporter), ERROR_UNSUPPORTED_DATA_ID, id);
                return null;
            }
            return data;
        }

        public static List<ISerializableData>? GetAllDataOfType(ImportedData dataType)
        {
            string targetFile = Path.Combine(JSON_PATH, GetFileName(dataType));
            if (!File.Exists(targetFile))
            {
                ConsoleErrorLogger.LogError(typeof(SentenceFormatterImporter), ERROR_FILE_DOESNT_EXIST, targetFile);
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
            ConsoleErrorLogger.LogError(typeof(SentenceFormatterImporter), ERROR_DATATYPE_INVALID, dataType.ToString());
            return "";
        }
    }
}
