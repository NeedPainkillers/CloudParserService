using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CloudParserService.Lib.Providers
{
    public class MultipleFieldsProvider : IDataProvider
    {
        public string Get(string data, string regex)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetAll(string data, string regex)
        {
            var res = Regex.Matches(data, regex).Select(x => x.Groups[1].Value.Trim());
            return res;
        }
    }
}
