using Newtonsoft.Json;
using NmeaParserConsole.Data.SerializableData.HeaderDefinition;
using NmeaParserConsole.Interfaces;

namespace NmeaParserConsole.JsonUtilities
{
    public class SentenceFormatterExportUtility
    {
        public void ExportHeaderDataToson(HeaderDefinitionData data, bool overrideSave = false)
        {
            string path = Path.Combine(SentenceFormatterImporter.JSON_PATH, SentenceFormatterImporter.HEADER_DEF);
            List<HeaderDefinitionData> toSave = new List<HeaderDefinitionData>
            {
                data
            };
            SaveData(toSave, path, ImportedData.HeaderDefinitions, overrideSave);
        }      

        private void SaveData<T>(List<T> toSave, string path, ImportedData dataType, bool overrideSave = false) where T : ISerializableData
        {
            List<ISerializableData>? fromFile = SentenceFormatterImporter.GetAllDataOfType(dataType);
            if (fromFile != null)
            {
                for (int i = 0; i < toSave.Count; i++)
                {
                    if (!fromFile.Any(d => d.GetIdentifier() == toSave[i].GetIdentifier()))
                    {
                        fromFile.Add(toSave[i]);
                    }
                    else if (overrideSave)
                    {
                        var toReplace = fromFile.First(d => d.GetIdentifier() == toSave[i].GetIdentifier());
                        fromFile.Remove(toReplace);
                        fromFile.Add(toSave[i]);
                    }
                }
                toSave = new();
                foreach (var item in fromFile)
                {
                    toSave.Add((T)item);
                }
            }

            string json = JsonConvert.SerializeObject(toSave);
            File.WriteAllText(path, json);
        }
    }
}