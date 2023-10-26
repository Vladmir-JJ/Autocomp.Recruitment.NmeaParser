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
    internal abstract class AbstractMessageData
    {        
        protected string _rawMessage = "";

        public AbstractMessageData(NmeaMessage message)
        {
            _rawMessage = message.ToString();
        }

        public virtual void PrintMessage()
        {
            PropertyInfo[] properties = this.GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                Console.WriteLine(property.Name + " " + property.GetValue(this).ToString());
            }
        }

        protected void ParseField((string value, Type type) field)
        {
            if (field.type != typeof(string))
            {
                var xD = Convert.ChangeType(field.value, field.type, CultureInfo.InvariantCulture);
                Console.WriteLine($"{field} is numerical {xD}");
                //var ret = field as type;
            }
        }       
        //TODO: error logger?
    }
}
