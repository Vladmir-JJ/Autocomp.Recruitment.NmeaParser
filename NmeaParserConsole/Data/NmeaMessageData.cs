using Autocomp.Nmea.Common;
using NmeaParserConsole.ConsoleInterface;
using NmeaParserConsole.Data.DataFields;
using NmeaParserConsole.Data.SerializableData.HeaderDefinition;
using static NmeaParserConsole.ConsoleInterface.ConsoleMessageLibrary;

namespace NmeaParserConsole.Data
{
    public class NmeaMessageData
    {      
        private NmeaMessage _message;
        private List<AbstractNmeaField> _printableData;
        private string _description;

        public NmeaMessageData(NmeaMessage message, HeaderDefinitionData data)
        {
            _message = message;
            _printableData = BuildFields(data);
            _description = data.MessageDescription;
        }

        /// <summary>
        /// Populates _printableData with objects containing desired data types.
        /// </summary>
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

        /// <summary>
        /// Calls console output manager to print data.
        /// </summary>
        public virtual void PrintMessage()
        {
            ConsoleOutputManager.PrintNmeaMessage(_printableData, _message.Header, _description, _message.ToString());         
        }
    }
}
