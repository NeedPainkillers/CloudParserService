using AngleSharp;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudParserService.Lib
{
    class AvitoParser : IParser
    {
        public async Task<IEnumerable<string>> GetData(string url)
        {
            var config = Configuration.Default.WithDefaultLoader();
            var address = url;
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(address);
            var elements = document.All.Where(m => m.LocalName == "div" && m.ClassList.Contains("styles-root-WJh3P") && m.HasAttribute("data-marker"));


            var res = elements.Select(e => e.OuterHtml);

            return res;
        }
    }
}
