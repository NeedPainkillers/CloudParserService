using System.Collections.Generic;
using System.Threading.Tasks;

namespace CloudParserService.Lib
{
    public interface IParser
    {
        public Task<IEnumerable<string>> GetData(string url);
    }
}
