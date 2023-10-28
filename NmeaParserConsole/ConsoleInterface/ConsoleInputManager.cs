using Autocomp.Nmea.Common;
using NmeaParserConsole.Data;
using NmeaParserConsole.Data.SerializableData.HeaderDefinition;
using NmeaParserConsole.JsonUtilities;
using System.Reflection;

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
                case '1':
                    ConsoleOutputManager.PrintMessageFormats(ConsoleOutputManager.PrintHelpMessage);
                    break;
                case '2':
                    ConsoleOutputManager.PrintMessageWithCallback("Enter valid NMEA message with supported header:", AwaitMessage);
                    break;
                case '3':
                    ConsoleOutputManager.PrintMessageWithCallback("Provide new NMEA header:", AwaitNewField);
                    break;
                default: ConsoleOutputManager.PrintMessageWithCallback("Invalid input",  ConsoleOutputManager.PrintHelpMessage);
                    break;
            }
        }

        private static void AwaitMessage()
        {           
            string? userInput = Console.ReadLine();
            
            if (userInput != null)
            {
                string header;
                string[]  parts = userInput.Split(',');
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
                    Console.WriteLine($"Header {header} is already defined in json. Do you want to override it?\nPress [y] to confirm, press any key to exit");
                    if (Console.ReadKey().KeyChar != 'y')
                    {
                        ConsoleOutputManager.PrintHelpMessage();
                        return;
                    }
                }
                int i = 1;
                List<HeaderDefinitionFieldCharacteristics> fieldCharacteristics = new();
                while (true)
                {                    
                    Console.WriteLine($"Provide field number {i}:\nDescription=FieldType=RegexFormat=ExtraDataType\nExample:\nStatus: =Char=^[A,V]$=Status\nexit => quit without saving\nfinish => proceed");
                    string? response = Console.ReadLine();
                    if (response != null)
                    {
                        if (response == "exit")
                        {
                            ConsoleOutputManager.PrintHelpMessage();
                            return;
                        }
                        if(response == "finish")
                        {
                            break;
                        }
                        string[] fields = response.Split('=');
                        if(fields.Length != 4)
                        {
                            Console.WriteLine("Invalid definition");
                            ConsoleOutputManager.PrintHelpMessage();
                            return;
                        }
                        HeaderDefinitionFieldCharacteristics characteristic = new(fields[0], fields[1], fields[2], fields[3]);
                        fieldCharacteristics.Add(characteristic);
                        i++;
                    }
                }
                Console.WriteLine("Provide header description:");
                string? description = Console.ReadLine();
                Console.WriteLine("Provide message format details:\nExample:\nMWV,x.x,a,y.y,b,c\n add description for values if needed\nFields entered:");
                for (int j = 0; j < fieldCharacteristics.Count; j++)
                {
                    Console.WriteLine($"{fieldCharacteristics[j].Description} {fieldCharacteristics[j].FieldType} {fieldCharacteristics[j].Format} {fieldCharacteristics[j].ExtraData}");
                }
                string? formatDetails = Console.ReadLine();
                if (description == null)
                    description = "Unknown description";
                if (formatDetails == null)
                    formatDetails = "Unknown format details";
                formatDetails = formatDetails.Replace("\\n", "\n");
                HeaderDefinitionData newData = new(header, description, formatDetails, fieldCharacteristics);
                SentenceFormatterExportUtility e = new();
                e.ExportHeaderDataToson(newData, true);
                ConsoleOutputManager.PrintHelpMessage();
            }
        }
    }
}
