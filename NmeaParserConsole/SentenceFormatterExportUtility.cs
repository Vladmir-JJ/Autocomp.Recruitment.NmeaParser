using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NmeaParserConsole
{
    internal class SentenceFormatterExportUtility
    {
        public void ExportDataToJson()
        {
             List<LoadableData.FieldCharacteristics> GLLdefinition = new()
            {
                new ("Latitude : ", typeof(float).Name, RegExLibrary.FORMAT_LONG_LAT),
                new("North - South : ", typeof(char).Name, RegExLibrary.FORMAT_NS),
                new("Longitude : ", typeof(float).Name, RegExLibrary.FORMAT_LONG_LAT),
                new("East - West : ", typeof(char).Name, RegExLibrary.FORMAT_EW),
                new ("UTC of position : ", typeof(float).Name, RegExLibrary.FORMAT_UTC),
                new("Status : ", typeof(char).Name, RegExLibrary.FORMAT_STATUS),
                new("Mode indicator : ", typeof(char).Name, RegExLibrary.FORMAT_MODE_INDICATOR)
            };

            LoadableData GLLdataStructure = new("GLL", GLLdefinition);

            List<LoadableData.FieldCharacteristics> MWVdefinition = new()
            {
                new ("Wind angle : ", typeof(float).Name, RegExLibrary.FORMAT_ANGLE),
                new("Reference : ", typeof(char).Name, RegExLibrary.FORMAT_RELATIVE),
                new("Wind speed : ", typeof(float).Name, RegExLibrary.FORMAT_NUMERIC),
                new("Wind speed units : ", typeof(char).Name, RegExLibrary.FORMAT_SPEED_UNITS),
                new ("Status : ", typeof(char).Name, RegExLibrary.FORMAT_STATUS),
            };

            LoadableData MWVdataStructure = new("MWV", MWVdefinition);

            //Console.Write(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase));

            List<LoadableData> toSave = new() { MWVdataStructure, GLLdataStructure };
            var options = new JsonSerializerOptions()
            {
                IncludeFields = true,
            };
            
            string json = JsonSerializer.Serialize(toSave, options);

        //READ FROM FILE, APPEND json!

            string path = Path.Combine(@"Json\", "HeaderDefinitions.json");
            
            File.WriteAllText(path, json);
        }
    }
}
