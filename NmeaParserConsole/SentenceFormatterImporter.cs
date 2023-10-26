using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NmeaParserConsole
{
    internal static class SentenceFormatterImporter
    {
        const string PATH = @"Json\HeaderDefinitions.json";

        /// <summary>
        /// Returns data structure for supported NMEA message formatters by message header
        /// </summary>
        public static LoadableData? GetNmeaFieldsDataFormat(string header)
        {
             string s = File.ReadAllText(PATH);
           
            var options = new JsonSerializerOptions()
            {
                IncludeFields = true,
            };
            List<LoadableData>? datas = JsonSerializer.Deserialize<List<LoadableData>>(s, options);
            if (datas == null)
            {
                //TODO log error
                Console.WriteLine($"ERROR - failed to deserialize file from {PATH}");
                return null;
            }
            var data = datas.FirstOrDefault(d => d.Header == header);
            if (data == default)
            {
                //TODO: log error
                Console.WriteLine($"Error - unsupported header: {header}");
                return null;
            }
            return data;
        }
    }
}
