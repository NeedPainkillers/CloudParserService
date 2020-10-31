using System;

namespace CloudParserService.Lib
{
    public class Settings
    {
        public string uri;
        public string connstr;
        public string conndb;

        public Settings()
        {
            uri = "https://www.avito.ru/moskva";
            connstr = Environment.GetEnvironmentVariable("MongoDB_connection_string");
            conndb = "CloudParser";
        }
    }
}