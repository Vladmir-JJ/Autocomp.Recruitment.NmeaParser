using Newtonsoft.Json;

namespace NmeaParserConsole
{
    internal class SentenceFormatterExportUtility
    {
        public void ExportDataToJsonDEV(bool overrideSave = false)
        {
            List<FieldCharacteristics> GLLdefinition = new()
            {
                new ("Latitude : ", typeof(float).Name, RegExLibrary.FORMAT_LONG_LAT),
                new("North - South : ", typeof(char).Name, RegExLibrary.FORMAT_NS),
                new("Longitude : ", typeof(float).Name, RegExLibrary.FORMAT_LONG_LAT),
                new("East - West : ", typeof(char).Name, RegExLibrary.FORMAT_EW),
                new ("UTC of position : ", typeof(float).Name, RegExLibrary.FORMAT_UTC),
                new("Status : ", typeof(char).Name, RegExLibrary.FORMAT_STATUS, ExtraDataType.Status.ToString()),
                new("Mode indicator : ", typeof(char).Name, RegExLibrary.FORMAT_MODE_INDICATOR, ExtraDataType.ModeIndicator.ToString())
            };

            LoadableData GLLdataStructure = new("GLL", GLLdefinition);

            List<FieldCharacteristics> MWVdefinition = new()
            {
                new ("Wind angle : ", typeof(float).Name, RegExLibrary.FORMAT_ANGLE),
                new("Reference : ", typeof(char).Name, RegExLibrary.FORMAT_RELATIVE, ExtraDataType.Reference.ToString()),
                new("Wind speed : ", typeof(float).Name, RegExLibrary.FORMAT_NUMERIC),
                new("Wind speed units : ", typeof(char).Name, RegExLibrary.FORMAT_SPEED_UNITS),
                new ("Status : ", typeof(char).Name, RegExLibrary.FORMAT_STATUS, ExtraDataType.Status.ToString()),
            };

            LoadableData MWVdataStructure = new("MWV", MWVdefinition);

            List<IStorableData> toSave = new() { MWVdataStructure, GLLdataStructure };

            string path = Path.Combine(SentenceFormatterImporter.JSON_PATH, SentenceFormatterImporter.HEADER_DEF);
            SaveData(toSave, path, ImportedData.HeaderDefinitions, overrideSave);
        }

        public void ExportExtraDataDEV(bool overrideSave = false)
        {
            List<ExtraData> extraInfoMWVref = new ()
            {
                new( "R", "relative"),
                new( "T", "total" )
            };

            List<ExtraData> extraInfoStatus = new ()
            {
                new( "A", "data valid" ),
                new("V", "data invalid" )
            };
            List<ExtraData> extraInfoGLLModeInd = new ()
            {
                new( "A", "Autonomous mode" ),
                new( "D", "Differential mode" ),
                new( "E", "Estimated(dead reckoning) mode" ),
                new( "M", "Manual input mode" ),
                new( "S", "Simulator mode" ),
                new("N", "Data not valid" )
            };

            List<IStorableData> toSave = new()
            {
            new ExtraDataContainer(ExtraDataType.Reference.ToString(), extraInfoMWVref),
            new ExtraDataContainer(ExtraDataType.Status.ToString(), extraInfoStatus),
            new ExtraDataContainer(ExtraDataType.ModeIndicator.ToString(), extraInfoGLLModeInd)
            };

            string path = Path.Combine(SentenceFormatterImporter.JSON_PATH, SentenceFormatterImporter.EXTRA_DATA);
            SaveData(toSave, path, ImportedData.ExtraData, overrideSave);
        }

        private void SaveData<T>(List<T> toSave, string path, ImportedData dataType, bool overrideSave = false) where T :IStorableData
        {
            List<IStorableData>? fromFile = SentenceFormatterImporter.GetAllDataOfType(dataType);
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