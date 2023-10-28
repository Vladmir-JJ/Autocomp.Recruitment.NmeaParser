using NmeaParserConsole.Data.SerializableData.HeaderDefinition;
using NmeaParserConsole.JsonUtilities;

namespace NmeaParserConsole.ConsoleInterface
{
    public class ConsoleOutputManager
    {
        public static void PrintWelcomeMessage()
        {
            Console.WriteLine("NMEA 0183\r\nStandard For Interfacing\r\nMarine Electronic Devices\r\n\nNMEA message parser\r\nCreated 28.10.2023 by Jędrzej Wysocki for Autocomp Managment\n");
            PrintHelpMessage();
        }

        public static void PrintMessageFormats(Action onComplete)
        {
            var data = SentenceFormatterImporter.GetAllDataOfType(ImportedData.HeaderDefinitions);
            if (data != null && data.Count > 0)
            {
                for (int i = 0; i < data.Count; i++)
                {
                    var headerDefinition = data[i] as HeaderDefinitionData;
                    if (headerDefinition != null)
                        Console.WriteLine(headerDefinition.MessageFormat+"\n");
                }
            }
            else
            {
                //TODO: log error Console.WriteLine($"No valid data file found in ");
            }
            onComplete?.Invoke();
        }

        public static void PrintHelpMessage()
        {
            Console.WriteLine("\nPrint currently supported headers: [1]\nEnter NMEA message to parse: [2]\nEnter new NMEA message definition [3]");
            ConsoleInputManager.AwaitImput("Awaiting user input...\n");
        }

        public static void PrintMessageWithCallback(string message, Action onComplete)
        {
            Console.WriteLine(message);
            onComplete?.Invoke();
        }

    }
}
