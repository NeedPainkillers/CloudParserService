using CloudParserService.Lib.Providers;
using CloudParserService.Model;
using System.Linq;

namespace CloudParserService.Lib
{
    public class AvitoEntryConverter : IConvert
    {
        readonly private string _imageRegex = "<img.*?srcset=\"(.*?)\"";
        readonly private string _titleRegex = "<a.*? href=\"(.*?)\".*?title=\"(.*?)\"";
        readonly private string _priceRegex = "<meta\\s*?itemprop=\"price\"\\s*?content=\"(.*?)\">";
        readonly private string _priceCurrencyRegex = "<meta\\s*?itemprop=\"priceCurrency\"\\s*?content=\"(.*?)\">";
        readonly private string _locationRegex = "geo-address.*?<span>(.*?)</span>";
        readonly private string _timeRegex = "([a-zA-ZА-Яа-я]*\\s*?[0-9]{2}:[0-9]{2})</span>";

        readonly private IDataProvider _dataSGProvider = new SingleGroupProvider();
        readonly private IDataProvider _dataMGProvider = new MultipleGroupsProvider();

        readonly private IDataProvider _dataMFProvider = new MultipleFieldsProvider();
        public Entry Convert(string data)
        {
            var price = _dataSGProvider.Get(data, _priceRegex);
            var priceCurrency = _dataSGProvider.Get(data, _priceCurrencyRegex);
            var location = _dataSGProvider.Get(data, _locationRegex);

            var time = _dataSGProvider.Get(data, _timeRegex);

            var urlTitle = _dataMGProvider.GetAll(data, _titleRegex);

            var images = _dataMFProvider.GetAll(data, _imageRegex);

            Entry entry = new Entry()
            {
                Images = images,
                Location = location,
                Price = price,
                PriceCurrency = priceCurrency,
                Time = time,
                Title = urlTitle.ElementAt(2),
                Url = urlTitle.ElementAt(1)
            };
            return entry;
        }
    }
}
