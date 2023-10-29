using Autocomp.Nmea.Common;
using NmeaParserConsole.Data;
using NmeaParserConsole.Data.SerializableData.ExtraData;
using NmeaParserConsole.Data.SerializableData.HeaderDefinition;
using NmeaParserConsole.JsonUtilities;
using static NmeaParserConsole.ConsoleInterface.ConsoleMessageLibrary;

namespace NmeaParserConsole.ConsoleInterface
{
    public static class ConsoleInputManager
    {    
        /// <summary>
        /// Print provided message and show input choices for user
        /// </summary>
        public static void AwaitInput(string message)
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
                    ConsoleOutputManager.PrintMessageWithCallback(PROVIDE_HEADER, () => AwaitNewIdentifier(ImportedData.HeaderDefinitions));
                    break;
                case CHOICE_4:
                    ConsoleOutputManager.PrintMessageWithCallback(PROVIDE_EXTRA_DATA_ENUM, () => AwaitNewIdentifier(ImportedData.ExtraData));
                    break;
                default: ConsoleOutputManager.PrintMessageWithCallback(GENERIC_INVALID_INPUT,  ConsoleOutputManager.PrintHelpMessage);
                    break;
            }
        }

        /// <summary>
        /// Reads NMEA message provided by user
        /// </summary>
        private static void AwaitMessage()
        {           
            string? userInput = ReadLineHandled();

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

        /// <summary>
        /// Reads Header for new Header definition (i.e. MWV) or enum name for extra data (WindSpeedUnits)
        /// </summary>
        private static void AwaitNewIdentifier(ImportedData dataType)
        {
            string? identifier = ReadLineHandled();
            if (identifier != null)
            {
                var data = SentenceFormatterImporter.GetAllDataOfType(dataType);
                if(data != null && data.Any(d => d.GetIdentifier() == identifier))
                {
                    Console.WriteLine(IDENTIFIER_EXISTS, identifier);
                    if (Console.ReadKey().KeyChar != YES)
                    {
                        ConsoleOutputManager.PrintMessageWithCallback(ABORTED_BY_USER, ConsoleOutputManager.PrintHelpMessage);
                        return;
                    }                    
                }
                switch (dataType)
                {
                    case ImportedData.HeaderDefinitions:
                        AwaitFields(identifier);
                        break;
                    case ImportedData.ExtraData:
                        AwaitNewExtraData(identifier);
                        break;
                    default:
                        break;
                }                
            }
        }

        /// <summary>
        /// While user is creating new NMEA message definition this method reads single field definition
        /// </summary>
        private static void AwaitFields(string header)
        {
            int i = 1;
            List<HeaderDefinitionFieldCharacteristics> fieldCharacteristics = new();
            while (true)
            {
                string? response = ReadLineHandled(PROVIDE_FIELD, i);
                if (response != null)
                {
                    if (response == INPUT_EXIT)
                    {
                        ConsoleOutputManager.PrintMessageWithCallback(ABORTED_BY_USER, ConsoleOutputManager.PrintHelpMessage);
                        return;
                    }
                    if (response == INPUT_FINISH)
                    {
                        break;
                    }
                    string[] fields = response.Split(CUSTOM_DEFINITOIN_SEPARATOIR);
                    if (fields.Length != HeaderDefinitionFieldCharacteristics.GetFieldCount())
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

            AwaitExtraData(header, fieldCharacteristics);            
        }

        /// <summary>
        /// Allows user to provide extra data like description of message header acronym and message format preview
        /// </summary>
        private static void AwaitExtraData(string header, List<HeaderDefinitionFieldCharacteristics> fieldCharacteristics)
        {
            string? description = ReadLineHandled(PROVIDE_DESCRIPTION);            
            for (int j = 0; j < fieldCharacteristics.Count; j++)
            {
                Console.WriteLine($"{fieldCharacteristics[j].Description} {fieldCharacteristics[j].FieldType} {fieldCharacteristics[j].Format} {fieldCharacteristics[j].ExtraData}");
            }
            string? formatDetails = ReadLineHandled(PROVIDE_FORMAT_DESCRIPTION);
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

        /// <summary>
        /// While user is creating new extra data type this method reads single description for possible message input (i.e. 'A' [Status Active])
        /// </summary>
        private static void AwaitNewExtraData(string enumName)
        {
            int i = 1;
            List<ExtraData> fieldCharacteristics = new();
            while (true)
            {
                string? response = ReadLineHandled(PROVIDE_EXTRA_DATA);
                if (response != null)
                {
                    if (response == INPUT_EXIT)
                    {
                        ConsoleOutputManager.PrintMessageWithCallback(ABORTED_BY_USER, ConsoleOutputManager.PrintHelpMessage);
                        return;
                    }
                    if (response == INPUT_FINISH)
                    {
                        break;
                    }
                    string[] fields = response.Split(CUSTOM_DEFINITOIN_SEPARATOIR);
                    if (fields.Length != ExtraData.GetFieldCount())
                    {
                        ConsoleErrorLogger.LogError(GENERIC_INVALID_INPUT);
                        ConsoleOutputManager.PrintHelpMessage();
                        return;
                    }
                    ExtraData characteristic = new(fields[0], fields[1]);
                    fieldCharacteristics.Add(characteristic);
                    i++;
                }
            }
            ExtraDataContainer extraDataContainer = new(enumName, fieldCharacteristics);
            SentenceFormatterExportUtility e = new();
            e.ExportExtraDataToson(extraDataContainer, true);
            ConsoleOutputManager.PrintHelpMessage();
        }

        /// <summary>
        /// Reads line and handles exceptions. Optional message and numerical parameter are provided.
        /// </summary>
        private static string? ReadLineHandled(string? message = null, int i = -1)
        {
            string? response = null;
            if(message != null)
            {
                if (i < 0)
                    Console.WriteLine(message);
                else
                    Console.WriteLine(message, 1);
            }                
            try
            {
                response = Console.ReadLine();
            }
            catch (IOException ex)
            {
                ConsoleErrorLogger.LogError(EXCEPTION_IO, ex);
            }
            catch (OutOfMemoryException ex)
            {
                ConsoleErrorLogger.LogError(EXCEPTION_OUT_OF_MEMORY, ex);
            }

            return response;
        }
    }  
}
