using CloudParserService.Lib.Providers;
using CloudParserService.Model;
using System.Linq;

namespace CloudParserService.Lib
{
    public class AvitoEntryConverter : IConvert
    {
        private readonly string _imageRegex = "<img.*?srcset=\"(.*?)\"";
        private readonly string _titleRegex = "<a.*? href=\"(.*?)\".*?title=\"(.*?)\"";
        private readonly string _priceRegex = "<meta\\s*?itemprop=\"price\"\\s*?content=\"(.*?)\">";
        private readonly string _priceCurrencyRegex = "<meta\\s*?itemprop=\"priceCurrency\"\\s*?content=\"(.*?)\">";
        private readonly string _locationRegex = "geo-address.*?<span>(.*?)</span>";
        private readonly string _timeRegex = "([a-zA-ZА-Яа-я]*\\s*?[0-9]{2}:[0-9]{2})</span>";

        private readonly IDataProvider _dataSGProvider = new SingleGroupProvider();
        private readonly IDataProvider _dataMGProvider = new MultipleGroupsProvider();

        private readonly IDataProvider _dataMFProvider = new MultipleFieldsProvider();
        public Entry Convert(string data)
        {
            var price = _dataSGProvider.Get(data, _priceRegex);
            var priceCurrency = _dataSGProvider.Get(data, _priceCurrencyRegex);
            var location = _dataSGProvider.Get(data, _locationRegex);

            var time = _dataSGProvider.Get(data, _timeRegex);

            var urlTitle = _dataMGProvider.GetAll(data, _titleRegex);

            var images = _dataMFProvider.GetAll(data, _imageRegex);

            var urlTitleList = urlTitle.ToList();

            var entry = new Entry()
            {
                Images = images,
                Location = location,
                Price = price,
                PriceCurrency = priceCurrency,
                Time = time,
                Title = urlTitleList.ElementAt(2),
                Url = urlTitleList.ElementAt(1)
            };
            return entry;
        }
    }
}
