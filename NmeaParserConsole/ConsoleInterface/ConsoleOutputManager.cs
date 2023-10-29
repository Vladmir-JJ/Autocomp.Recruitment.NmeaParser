using NmeaParserConsole.Data.DataFields;
using NmeaParserConsole.Data.SerializableData.HeaderDefinition;
using NmeaParserConsole.JsonUtilities;
using static NmeaParserConsole.ConsoleInterface.ConsoleMessageLibrary;

namespace NmeaParserConsole.ConsoleInterface
{
    public static class ConsoleOutputManager
    {
        public static void PrintWelcomeMessage()
        {
            Console.WriteLine(HELLO_MESSAGE);
            PrintHelpMessage();
        }

        /// <summary>
        /// Returns currently supported NMEA message formats by headers defined in HeaderDefinitions.json
        /// </summary>
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
            onComplete?.Invoke();
        }

        /// <summary>
        /// Prints menu for choosing console options.
        /// </summary>
        public static void PrintHelpMessage()
        {
            Console.WriteLine(CHOOSE_ACTION);
            ConsoleInputManager.AwaitInput(AWAITING_INPUT);
        }

        /// <summary>
        /// Prints message and invokes callback on message printed.
        /// </summary>
        public static void PrintMessageWithCallback(string message, Action onComplete)
        {
            Console.WriteLine(message);
            onComplete?.Invoke();
        }

        /// <summary>
        /// Prints NMEA message with extra data description.
        /// </summary>
        public static void PrintNmeaMessage(List<AbstractNmeaField> providedData, string header, string description, string rawMessage)
        {
            Console.Write(PRINTED_MESSAGE_HEADER, header, description, rawMessage);
            for (int i = 0; i < providedData.Count; i++)
            {
                Console.WriteLine(providedData[i].GetPrintData());
            }
            Console.WriteLine(PRINTED_MESSAGE_END, header);
        }
    }
}
