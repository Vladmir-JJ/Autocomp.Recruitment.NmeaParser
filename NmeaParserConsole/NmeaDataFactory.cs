using Autocomp.Nmea.Common;
using NmeaParserConsole.MessageData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NmeaParserConsole
{   
    public enum SupportetHeader
    {
        MWV,
        GLL
    }

    internal class NmeaDataFactory
    {      
        public AbstractMessageData CreateMessageData(NmeaMessage message)
        {          
            string header = message.Header;
            if (header == SupportetHeader.MWV.ToString())
                return new MWVMessageData(message);
            else if (header == SupportetHeader.GLL.ToString())
                return new GLLMessageData(message);

            //Add new Message header variants here

            ErrorLogger.LogFactoryError(message, this);
            return null;
        }
    }
}
