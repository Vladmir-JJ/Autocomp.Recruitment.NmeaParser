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
    public struct DataStruct<T>
    {
        public DataStruct(T value, string suportedFormat)
        {
            Value = value;
            SupportedFormat = suportedFormat;
        }

        public T Value;
        public string SupportedFormat;
    }

    internal abstract class AbstractMessageData
    {
        protected string RawMessage => _message.ToString();

        protected NmeaMessage _message;
        protected PropertyInfo[] _properties;
        protected Dictionary<string, string> _supportedFormats = new Dictionary<string, string>();

        public AbstractMessageData(NmeaMessage message)
        {
            _message = message;
            _properties = GetProperties();
        }

        public virtual void PrintMessage()
        {
            Console.WriteLine($"Raw message recieved:\n{RawMessage}");
            //PropertyInfo[] properties = GetProperties();
            foreach (PropertyInfo property in _properties)
            {
               var value = property.GetValue(this);
                Console.WriteLine(property.Name + " " + value?.ToString());
            }
            Console.WriteLine();
        }

        protected PropertyInfo[] GetProperties() => GetType().GetProperties();

        protected void PopulateFields()
        {
            if (_message.Fields.Length != _properties.Length)
            {
                ErrorLogger.LogError(ErrorType.WrongFieldCount, _message, _properties, this);
            }

            for (int i = 0; i < _message.Fields.Length && i < _properties.Length; i++)
            {
                if (FieldTypeManager.IsFieldTypeValid(_message.Fields[i], _properties[i].PropertyType))
                {
                    FieldTypeManager.ConvertAndAssignField(_message.Fields[i], _properties[i].PropertyType, _properties[i], this);
                }
                else
                {
                    ErrorLogger.LogError(ErrorType.InvalidFieldInput, _message, _properties, this);
                }
            }
        }

        /*protected void ParseField((string value, Type type) field)
        {
            if (field.type != typeof(string))
            {
                var xD = Convert.ChangeType(field.value, field.type, CultureInfo.InvariantCulture);
                Console.WriteLine($"{field} is numerical {xD}");
                //var ret = field as type;
            }
        }*/
        //TODO: error logger?
    }
}
