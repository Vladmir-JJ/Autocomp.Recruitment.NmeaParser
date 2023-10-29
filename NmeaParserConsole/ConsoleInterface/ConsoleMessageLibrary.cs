namespace NmeaParserConsole.ConsoleInterface
{
    public static class ConsoleMessageLibrary
    {
        public const string GENERIC_INVALID_INPUT = "Invalid input";

        public const string ERROR_UNSUPPORTED_HEADER = "unsupported header: {0}";
        public const string ERROR_INVALID_FIELD_COUNT = "invalid field count: [{0}], expected: [{1}]";
        public const string ERROR_INVALID_FORMAT = "invalid format {0} at field of index[{1}]";
        public const string ERROR_UNKNOWN_EXTRA_DATA = "unknown extra data type: {0}";
        public const string ERROR_NO_VALID_FIELD = "no valid field type found for type {0}";
        public const string ERROR_FAILED_TO_DESERIALIZE = "failed to deserialize file from {0}";
        public const string ERROR_UNSUPPORTED_DATA_ID = "Error - unsupported data id: {0}";
        public const string ERROR_FILE_DOESNT_EXIST = "Error - file does not exist: {0}";
        public const string ERROR_DATATYPE_INVALID = "Error - provided data type {0} does not exist";
        public const string PARSE_ERROR = "Parsing error - could not parse from {0} to {1}";
        public const string EXCEPTION_IO = "An IO exception has occured: ";
        public const string EXCEPTION_OUT_OF_MEMORY = "Out of memory exception: ";

        public const string HELLO_MESSAGE = "NMEA 0183\r\nStandard For Interfacing\r\nMarine Electronic Devices\r\n\nNMEA message parser\r\nCreated 28.10.2023 by Jędrzej Wysocki for Autocomp Managment\n";
        public const string ENTER_VALID_NMEA = "Enter valid NMEA message with supported header:";
        public const string PROVIDE_HEADER = "Provide new NMEA header:";
        public const string PROVIDE_EXTRA_DATA_ENUM = "NOTE: NEW ENUM MUST ALSO BE ADDED TO [ExtraDataType.cs]!\nProvide extra data enum:";
        public const string IDENTIFIER_EXISTS = "Identifier {0} is already defined in json. Do you want to override it?\nPress [y] to confirm, press any key to exit";
        public const string PROVIDE_FIELD = "Provide field number {0}:\nDescription=FieldType=RegexFormat=ExtraDataType\nExample:\nStatus: =Char=^[A,V]$=Status\nexit => quit without saving\nfinish => proceed";
        public const string PROVIDE_EXTRA_DATA = "Provide extra data number {0}:\nCodedField= Description\nExample:\nX= Status drowned\nexit => quit without saving\nfinish => proceed";
        public const string PROVIDE_DESCRIPTION = "Provide header description:";
        public const string PROVIDE_FORMAT_DESCRIPTION = "Provide message format details:\nExample:\nMWV,x.x,a,y.y,b,c\n add description for values if needed\nFields entered:";
        public const string UNKNOWN_DESCRIPTION = "Unknown description";
        public const string UNKNOWN_DETAILS = "Unknown format details";
        public const string CHOOSE_ACTION = "\nPrint currently supported headers: [1]\nEnter NMEA message to parse: [2]\nEnter new NMEA message definition [3]\nEnter new extra data definition [4]";
        public const string AWAITING_INPUT = "Awaiting user input...\n";
        public const string PRINTED_MESSAGE_HEADER = "\n========  {0}  ========\n{1}\nRaw message:{2}";
        public const string PRINTED_MESSAGE_END = "\n========  /{0}  ========";
        public const string ABORTED_BY_USER = "Adding new message was aborted by user";

        public const char YES = 'y';
        public const char CHOICE_1 = '1';
        public const char CHOICE_2 = '2';
        public const char CHOICE_3 = '3';
        public const char CHOICE_4 = '4';

        public const char NMEA_SEPARATOR = ',';
        public const char CUSTOM_DEFINITOIN_SEPARATOIR = '=';

        public const string INPUT_EXIT = "exit";
        public const string INPUT_FINISH = "finish";
    }
}
