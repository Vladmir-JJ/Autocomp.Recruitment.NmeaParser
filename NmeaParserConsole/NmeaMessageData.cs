using Autocomp.Nmea.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NmeaParserConsole.MessageData
{

    internal class NmeaMessageData
    {
        private string RawMessage => _message.ToString();

        private NmeaMessage _message;
        private List<IPrintableData> _printableData;
        // protected PropertyInfo[] _properties;

        public NmeaMessageData(NmeaMessage message, LoadableData data)
        {
            _message = message;
            _printableData = BuildFields(data);
            //_properties = GetProperties();
        }

        private List<IPrintableData> BuildFields(LoadableData data)
        {
            List<IPrintableData> ret = new ();
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
            Console.WriteLine($"Succesfully printing message with header {_message.Header}\nRaw message:\n{RawMessage}\n");
            for (int i = 0; i < _printableData.Count; i++)
            {
                Console.WriteLine(_printableData[i].GetPrintData());
            }
            Console.WriteLine("\n");
        }
    }
}
