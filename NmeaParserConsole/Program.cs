using Autocomp.Nmea.Common;

namespace NmeaParserConsole
{
    internal class Program
    {
        
        static void Main(string[] args)
        {           
            NmeaMessage message = new("MWVa", new string[] {"-13.4sd", "R", "21.37", "K", "A"}, NmeaFormat.Default);

            (string, Type)[] fieldTypes = new (string, Type)[message.Fields.Length];

            MWVMessageData data = new(message);

            data.PrintMessage();
           /* for (int i = 0; i < fieldTypes.Length; i++)
            {
                string field = FieldTypeManager.ParseField(field);
            }
            foreach (string field in message.Fields)
            {
                FieldTypeManager.ParseField(field);
            }*/
            Console.WriteLine(message.ToString());

            Console.WriteLine();
        }


        private void ParseField(IParsable field)
        {

        }
    }
}