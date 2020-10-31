using System.Collections.Generic;

namespace CloudParserService.Lib.Providers
{
    public interface IDataProvider
    {
        public string Get(string data, string regex);

        public IEnumerable<string> GetAll(string data, string regex);
    }
}
