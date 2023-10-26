using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NmeaParserConsole
{
    internal static class FieldTypeManager
    {      
        public static bool IsFieldTypeValid(string field, Type expectedType)
        {
            Type type = field.GetType();

            string expression = RegExLibrary.EXP_NUMERIC;

            if (Regex.Match(field, expression).Success)
            {
                //TODO improve parsing method
                type = field.Contains('.') ? typeof(float) : typeof(int);
                //Console.WriteLine($"{field} is numerical {float.Parse(field, CultureInfo.InvariantCulture)}");
            }
            else
            {
                //Better work OR ELSE! :D
                //Console.WriteLine($"{field} is not a float - it is {type}");
            }

            return type == expectedType;
        }

        public static void ConvertAndAssignField(string inputField, Type type, PropertyInfo property, object target)
        {
            if (type == typeof(string))
                property.SetValue(target, inputField);
            else if (type == typeof(float))
            {
                if (float.TryParse(inputField, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out float value))
                {
                    property.SetValue(target, value);
                }
                else
                {
                    //TODO log error
                }
            }
        }
    }
}
