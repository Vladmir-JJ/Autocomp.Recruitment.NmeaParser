using Autocomp.Nmea.Common;
using NmeaParserConsole.MessageData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NmeaParserConsole
{
    internal class GLLMessageData : AbstractMessageData
    {
        public float Latitude { get; private set; } = 0;
        public string NorthSouth { get; private set; } = "";
        public float Longitude { get; private set; } = 0;
        public string EastOrWest { get; private set; } = "";
        public float UTCOfPosition { get; private set; } = 0;
        public string Status { get; private set; } = "";
        public string ModeIndicator { get; private set; } = "";

        public GLLMessageData(NmeaMessage message) : base(message)
        {
            PopulateFields();
        }
    }
}
