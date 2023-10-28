using Autocomp.Nmea.Common;

namespace NmeaParserConsole.MessageData
{

    public class NmeaMessageData
    {
        private string RawMessage => _message.ToString();

        private NmeaMessage _message;
        private List<AbstractNmeaField> _printableData;
        private string _description;

        public NmeaMessageData(NmeaMessage message, LoadableData data)
        {
            _message = message;
            _printableData = BuildFields(data);
            _description = data.MessageDescription;
        }

        private List<AbstractNmeaField> BuildFields(LoadableData data)
        {
            List<AbstractNmeaField> ret = new ();
            NmeaFieldFactory fieldFactory = new NmeaFieldFactory();

            for (int i = 0; i < data.RequiredFields.Count; i++)
            {
                var field = fieldFactory.CreateField(data.RequiredFields[i], _message.Fields[i]);
                if (field != null)
                    ret.Add(field);
            }

            return ret;
        }

        public virtual void PrintMessage()
        {
            Console.WriteLine($"========  {_message.Header}  ========");
            Console.WriteLine($"{_description}\n");

            Console.WriteLine($"Raw message:{RawMessage}");
            for (int i = 0; i < _printableData.Count; i++)
            {
                Console.WriteLine(_printableData[i].GetPrintData());
            }
            Console.WriteLine($"\n========  /{_message.Header}  ========\n");
        }
    }
}
