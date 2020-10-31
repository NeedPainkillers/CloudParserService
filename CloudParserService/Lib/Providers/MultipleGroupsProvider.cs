using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CloudParserService.Lib.Providers
{
    public class MultipleGroupsProvider : IDataProvider
    {
        public string Get(string data, string regex)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetAll(string data, string regex)
        {
            var res = Regex.Match(data, regex).Groups.Cast<Group>().Select(x => x.Value.Trim());
            return res;
        }
    }
}
