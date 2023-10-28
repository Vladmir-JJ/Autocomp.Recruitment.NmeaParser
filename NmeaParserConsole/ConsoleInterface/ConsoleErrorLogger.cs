namespace NmeaParserConsole.ConsoleInterface
{
    public static class ConsoleErrorLogger
    {        
        public static void LogError(string reason)
        {
            Console.WriteLine(reason);
        }

        public static void LogError(object from, string reason, string arg)
        {
            Console.WriteLine($"[{from}] : {reason} {arg}");
        }
        public static void LogError(object from, string reason, string arg1, string arg2)
        {
            Console.WriteLine($"[{from}] : {reason}", arg1, arg2);
        }
    }
}
