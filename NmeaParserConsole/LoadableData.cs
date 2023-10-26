using NmeaParserConsole.MessageData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NmeaParserConsole
{
    [Serializable]
    internal class LoadableData
    {
        [Serializable]
        public struct FieldCharacteristics
        {
            public  string Description;
            public  string FieldType;
            public  string Format;

            [JsonConstructor]
            public FieldCharacteristics(string Description, string FieldType, string Format)
            {
                this.Description = Description;
                this.FieldType = FieldType;
                this.Format = Format;
            }

        }

        public string Header;
        public List<FieldCharacteristics> RequiredFields;

        [JsonConstructor]
        public LoadableData(string Header, List<FieldCharacteristics> RequiredFields)
        {
            this.Header = Header;
            this.RequiredFields = RequiredFields;

        }
    }
}
