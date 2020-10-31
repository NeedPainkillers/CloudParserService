using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CloudParserService.Lib.Providers
{
    public class SingleGroupProvider : IDataProvider
    {
        public string Get(string data, string regex)
        {
            var res = Regex.Match(data, regex).Groups[1].Value.Trim();
            return res;
        }

        public IEnumerable<string> GetAll(string data, string regex)
        {
            throw new NotImplementedException();
        }
    }
}
