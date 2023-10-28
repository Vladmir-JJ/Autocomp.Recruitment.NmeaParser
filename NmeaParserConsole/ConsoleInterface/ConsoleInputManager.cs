using Autocomp.Nmea.Common;
using NmeaParserConsole.Data;
using NmeaParserConsole.Data.SerializableData.HeaderDefinition;
using NmeaParserConsole.JsonUtilities;
using static NmeaParserConsole.ConsoleInterface.ConsoleMessageLibrary;

namespace NmeaParserConsole.ConsoleInterface
{
    public static class ConsoleInputManager
    {    
        public static void AwaitImput(string message)
        {
            Console.WriteLine(message);
            var response = Console.ReadKey(true);
            switch (response.KeyChar)
            {
                case CHOICE_1:
                    ConsoleOutputManager.PrintMessageFormats(ConsoleOutputManager.PrintHelpMessage);
                    break;
                case CHOICE_2:
                    ConsoleOutputManager.PrintMessageWithCallback(ENTER_VALID_NMEA, AwaitMessage);
                    break;
                case CHOICE_3:
                    ConsoleOutputManager.PrintMessageWithCallback(PROVIDE_HEADER, AwaitNewField);
                    break;
                default: ConsoleOutputManager.PrintMessageWithCallback(GENERIC_INVALID_INPUT,  ConsoleOutputManager.PrintHelpMessage);
                    break;
            }
        }

        private static void AwaitMessage()
        {           
            string? userInput = Console.ReadLine();
            
            if (userInput != null)
            {
                string header;
                string[]  parts = userInput.Split(NMEA_SEPARATOR);
                if (parts.Length >= 1)
                {
                    header = parts[0];
                    string[] fields = new string[parts.Length - 1];
                    for (int i = 1; i < parts.Length; i++)
                    {
                        fields[i - 1] = parts[i];
                    }
                    NmeaMessage userMessage = new(header, fields);
                    NmeaDataValidator validator = new NmeaDataValidator();
                    var data = validator.GetValidNmeaMessageData(userMessage);
                    data?.PrintMessage();
                }                
            }
            ConsoleOutputManager.PrintHelpMessage(); 
        }

        private static void AwaitNewField()
        {
            string? header = Console.ReadLine();
            if (header != null)
            {
                var data = SentenceFormatterImporter.GetAllDataOfType(ImportedData.HeaderDefinitions);
                if(data != null && data.Any(d => d.GetIdentifier() == header))
                {
                    Console.WriteLine(HEADER_EXISTS, header);
                    if (Console.ReadKey().KeyChar != YES)
                    {
                        ConsoleOutputManager.PrintHelpMessage();
                        return;
                    }
                }
                int i = 1;
                List<HeaderDefinitionFieldCharacteristics> fieldCharacteristics = new();
                while (true)
                {                    
                    Console.WriteLine(PROVIDE_FIELD, i);
                    string? response = Console.ReadLine();
                    if (response != null)
                    {
                        if (response == INPUT_EXIT)
                        {
                            ConsoleOutputManager.PrintHelpMessage();
                            return;
                        }
                        if(response == INPUT_FINISH)
                        {
                            break;
                        }
                        string[] fields = response.Split(HEADER_DEFINITION_SEPARATOR);
                        if(fields.Length != 4)
                        {
                            ConsoleErrorLogger.LogError(GENERIC_INVALID_INPUT);
                            ConsoleOutputManager.PrintHelpMessage();
                            return;
                        }
                        HeaderDefinitionFieldCharacteristics characteristic = new(fields[0], fields[1], fields[2], fields[3]);
                        fieldCharacteristics.Add(characteristic);
                        i++;
                    }
                }
                Console.WriteLine(PROVIDE_DESCRIPTION);
                string? description = Console.ReadLine();
                Console.WriteLine(PROVIDE_FORMAT_DESCRIPTION);
                for (int j = 0; j < fieldCharacteristics.Count; j++)
                {
                    Console.WriteLine($"{fieldCharacteristics[j].Description} {fieldCharacteristics[j].FieldType} {fieldCharacteristics[j].Format} {fieldCharacteristics[j].ExtraData}");
                }
                string? formatDetails = Console.ReadLine();
                if (description == null)
                    description = UNKNOWN_DESCRIPTION;
                if (formatDetails == null)
                    formatDetails = UNKNOWN_DETAILS;
                formatDetails = formatDetails.Replace("\\n", "\n");
                HeaderDefinitionData newData = new(header, description, formatDetails, fieldCharacteristics);
                SentenceFormatterExportUtility e = new();
                e.ExportHeaderDataToson(newData, true);
                ConsoleOutputManager.PrintHelpMessage();
            }
        }
    }
}
