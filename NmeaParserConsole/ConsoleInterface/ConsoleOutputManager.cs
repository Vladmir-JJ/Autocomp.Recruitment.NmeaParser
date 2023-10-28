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

        public static void PrintHelpMessage()
        {
            Console.WriteLine(CHOOSE_ACTION);
            ConsoleInputManager.AwaitImput(AWAITING_INPUT);
        }

        public static void PrintMessageWithCallback(string message, Action onComplete)
        {
            Console.WriteLine(message);
            onComplete?.Invoke();
        }
    }
}
