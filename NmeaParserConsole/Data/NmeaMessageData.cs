﻿using Autocomp.Nmea.Common;
using NmeaParserConsole.Data.DataFields;
using NmeaParserConsole.Data.SerializableData.HeaderDefinition;
using static NmeaParserConsole.ConsoleInterface.ConsoleMessageLibrary;

namespace NmeaParserConsole.Data
{
    public class NmeaMessageData
    {
        private string RawMessage => _message.ToString();

        private NmeaMessage _message;
        private List<AbstractNmeaField> _printableData;
        private string _description;

        public NmeaMessageData(NmeaMessage message, HeaderDefinitionData data)
        {
            _message = message;
            _printableData = BuildFields(data);
            _description = data.MessageDescription;
        }

        private List<AbstractNmeaField> BuildFields(HeaderDefinitionData data)
        {
            List<AbstractNmeaField> ret = new();
            NmeaMessageDataFieldFactory fieldFactory = new NmeaMessageDataFieldFactory();

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
            Console.Write(PRINTED_MESSAGE_HEADER, _message.Header, _description, RawMessage);
            for (int i = 0; i < _printableData.Count; i++)
            {
                Console.WriteLine(_printableData[i].GetPrintData());
            }
            Console.WriteLine(PRINTED_MESSAGE_END, _message.Header);
        }
    }
}
