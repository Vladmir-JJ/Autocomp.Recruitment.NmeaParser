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
    internal sealed class MWVMessageData : AbstractMessageData
    {
        public float WindAngle { get; private set; } = 0;
        public string Reference { get; private set; } = "";
        public float WindSpeed { get; private set; } = 0;
        public string WindSpeedUnits { get; private set; } = "";
        public string Status { get; private set; } = "";

        public MWVMessageData(NmeaMessage message) : base(message)
        {
            PopulateFields();
        }       
    }
}
