using Autocomp.Nmea.Common;
using NmeaParserConsole.MessageData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NmeaParserConsole
{
    internal class MWVMessageData : AbstractMessageData
    {
        public float WindAngle { get; private set; } = 0;
        public string Reference { get; private set; } = "";
        public float WindSpeed { get; private set; } = 0;
        public string WindSpeedUnits { get; private set; } = "";
        public string Status { get; private set; } = "";

        public MWVMessageData(NmeaMessage message) : base(message)
        {
            var myType = this.GetType();
            PropertyInfo[] properties = myType.GetProperties();

            if(message.Fields.Length != properties.Length)
            {
                Console.WriteLine($"Invalid field count [{message.Fields.Length}] for message header [{message.Header}] Expected: [{properties.Length}]");
            }

            for (int i = 0; i < message.Fields.Length; i++)
            {
                if (FieldTypeManager.IsFieldTypeValid(message.Fields[i], properties[i].PropertyType))
                {
                    FieldTypeManager.ConvertAndAssignField(message.Fields[i], properties[i].PropertyType, properties[i], this);
                }
                else
                {

                }
                /*Console.WriteLine(message.Fields[i]);
                Console.WriteLine(properties[i].);*/
            }
        }       
    }
}
