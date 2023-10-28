using Newtonsoft.Json;

namespace NmeaParserConsole
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
                
        public static IStorableData? GetDataFromFile(string id, ImportedData dataType)
        {           
            List<IStorableData>? datas = GetAllDataOfType(dataType);
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

        public static List<IStorableData>? GetAllDataOfType(ImportedData dataType)
        {
            string targetFile = Path.Combine(JSON_PATH, GetFileName(dataType));
            if(!File.Exists(targetFile))
            {
                //TODO: log error file doesnt exist
                return null;
            }
            
            string s = File.ReadAllText(targetFile);
      
            switch (dataType)
            {
                case ImportedData.HeaderDefinitions:
                    var loadable = JsonConvert.DeserializeObject<List<LoadableData>>(s);
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
         /*   switch (dataType)
            {
                case ImportedData.HeaderDefinitions:
                    var loadableData = JsonSerializer.Deserialize<List<LoadableData>>(s, options);
                    if(loadableData != null)
                        return new(loadableData);
                    break;
                case ImportedData.ExtraData:
                    var extraData = JsonSerializer.Deserialize<List<ExtraDataContainer>>(s, options);
                    if (extraData != null)
                        return new(extraData);
                    break;               
            }*/
            //TODO log error
            //return null;
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

        /// <summary>
        /// Returns data structure for supported NMEA message formatters by message header
        /// </summary>
        /*   public static LoadableData? GetNmeaFieldsDataFormat(string header)
           {
               string s = ReadFromFile(JSON_PATH, HEADER_DEF);

               var options = new JsonSerializerOptions()
               {
                   IncludeFields = true
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

           public static ExtraData? GetExtraData(string extraDataType)
           {
               string s = ReadFromFile(JSON_PATH, EXTRA_DATA);

               var options = new JsonSerializerOptions()
               {
                   IncludeFields = true
               };
               List<ExtraData> datas = JsonSerializer.Deserialize<List<ExtraData>>(s, options);

           }*/         
    }
}
