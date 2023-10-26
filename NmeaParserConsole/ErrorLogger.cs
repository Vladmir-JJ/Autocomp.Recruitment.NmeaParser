using Autocomp.Nmea.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace NmeaParserConsole
{
    public enum ErrorType
    {
        WrongFieldCount,
        InvalidFieldInput,
        CastFailed
    }

    internal static class ErrorLogger
    {
        public static void LogError(ErrorType errorType, NmeaMessage message, PropertyInfo[] properties, object from)
        {
            switch (errorType)
            {
                case ErrorType.WrongFieldCount:
                    Console.WriteLine($"[{from}] - [{errorType}]: [{message.Fields.Length}], for header [{message.Header}] expected [{properties.Length}]]");
                    break;
                case ErrorType.InvalidFieldInput:
                    break;
                case ErrorType.CastFailed:
                    break;
                default:
                    break;
            }
            
        }

        public static void LogFactoryError(NmeaMessage message, object from)
        {
            Console.WriteLine($"[{from}]: Header [{message.Header} is currently unsupported]");
            Console.WriteLine($"Message provided: {message.ToString()}");
        }
    }
}
