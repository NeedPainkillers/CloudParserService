using Microsoft.VisualStudio.TestTools.UnitTesting;
using CloudParserService.Lib;
using System.Collections.Generic;
using System.Linq;
using CloudParserService.Model;

namespace CloudParserService.Tests
{
    /* sample data
    <div data-marker=\"bx-recommendations-block-item\"
        class=\"styles-root-WJh3P photo-slider-slider-15LoY styles-item-h-Dpn js-item-2006957136\" itemscope=\"\"
        itemtype=\"http://schema.org/Product\">
        <header class=\"styles-root-1e90u\"><span class=\"tooltip-tooltip-box-2rApK\"><span
                    class=\"tooltip-target-wrapper-XcPdv\">
                    <div data-marker=\"favorite\" class=\"styles-root-3BW8A\" title=\"Добавить в избранное и в сравнение\"></div>
                </span></span><a class=\"styles-link-36uWZ\" itemprop=\"url\"
                href=\"/moskva/odezhda_obuv_aksessuary/belyy_sviter_s_lentoy_2006957136\" target=\"_blank\"
                title=\"Объявление «Белый свитер с лентой»\" rel=\"noopener\">
                <div class=\"photo-slider-root-2qXU9\">
                    <div class=\"photo-slider-photoSlider-ksL6h photo-slider-aspect-ratio-4-3-2ixHg\">
                        <ul class=\"photo-slider-list-3Zt1Z\">
                            <li class=\"photo-slider-list-item-1JrLt\"
                                data-marker=\"slider-image/image-https://03.img.avito.st/208x156/9399452203.jpg\">
                                <div class=\"photo-slider-item-2G-8p photo-slider-item_visible-3nYBH\"><img
                                        class=\"photo-slider-image-3kAVC\" itemprop=\"image\"
                                        elementtiming=\"bx.recommendations.first-item\" alt=\"Белый свитер с лентой\"
                                        src=\"https://03.img.avito.st/208x156/9399452203.jpg\"
                                        srcset=\"https://03.img.avito.st/image/1/9pRiGra_Wn0Uv6h7AjHa-5K5XHfceV6P0LlYe9q_WH3W_w 1.5x\">
                                </div>
                            </li>
                        </ul>
                    </div>
                </div>
            </a></header>
        <div class=\"styles-root-2Jty7\"><a href=\"/moskva/odezhda_obuv_aksessuary/belyy_sviter_s_lentoy_2006957136\"
                target=\"_blank\" rel=\"noopener\" title=\"Белый свитер с лентой в Москве\" itemprop=\"url\" data-marker=\"title\"
                class=\"link-link-39EVK link-design-default-2sPEv title-root-395AQ styles-title-2VgwA title-root_maxHeight-3obWc\"><span
                    itemprop=\"name\"
                    class=\"title-root-395AQ styles-title-2VgwA title-root_maxHeight-3obWc text-text-1PdBw text-size-s-1PUdo text-bold-3R9dt\">Белый
                    свитер с лентой</span></a><span class=\"price-root-1n2wM styles-price-1cOkp\"><span
                    class=\"price-price-32bra\" itemprop=\"offers\" itemscope=\"\" itemtype=\"http://schema.org/Offer\">
                    <meta itemprop=\"priceCurrency\" content=\"RUB\">
                    <meta itemprop=\"price\" content=\"500\">
                    <meta itemprop=\"availability\" content=\"https://schema.org/LimitedAvailability\"><span
                        class=\"price-text-1HrJ_ text-text-1PdBw text-size-s-1PUdo\">500
                        <!-- -->&nbsp;<span class=\"price-currency-LOpM3\">₽</span></span></span></span>
            <div class=\"geo-root-1pUZ8\"><span class=\"geo-address-9QndR text-text-1PdBw text-size-s-1PUdo\"><span>Москва,
                        Марьина Роща</span></span></div><span
                class=\"text-text-1PdBw text-size-s-1PUdo text-color-noaccent-bzEdI\">Сегодня 20:21</span>
        </div>
    </div>
    */
    [TestClass]
    public class AvitoEntryConverterTest
    {
        private string _data;
        private AvitoEntryConverter _converter;
        private Entry _res;
        

        [TestInitialize]
        public void Init()
        {
            _data = "<div data-marker=\"bx-recommendations-block-item\"    class=\"styles-root-WJh3P photo-slider-slider-15LoY styles-item-h-Dpn js-item-2006957136\" itemscope=\"\"    itemtype=\"http://schema.org/Product\">    <header class=\"styles-root-1e90u\"><span class=\"tooltip-tooltip-box-2rApK\"><span                class=\"tooltip-target-wrapper-XcPdv\">                <div data-marker=\"favorite\" class=\"styles-root-3BW8A\" title=\"Добавить в избранное и в сравнение\"></div>            </span></span><a class=\"styles-link-36uWZ\" itemprop=\"url\"            href=\"/moskva/odezhda_obuv_aksessuary/belyy_sviter_s_lentoy_2006957136\" target=\"_blank\"            title=\"Объявление «Белый свитер с лентой»\" rel=\"noopener\">            <div class=\"photo-slider-root-2qXU9\">                <div class=\"photo-slider-photoSlider-ksL6h photo-slider-aspect-ratio-4-3-2ixHg\">                    <ul class=\"photo-slider-list-3Zt1Z\">                        <li class=\"photo-slider-list-item-1JrLt\"                            data-marker=\"slider-image/image-https://03.img.avito.st/208x156/9399452203.jpg\">                            <div class=\"photo-slider-item-2G-8p photo-slider-item_visible-3nYBH\"><img                                    class=\"photo-slider-image-3kAVC\" itemprop=\"image\"                                    elementtiming=\"bx.recommendations.first-item\" alt=\"Белый свитер с лентой\"                                    src=\"https://03.img.avito.st/208x156/9399452203.jpg\"                                    srcset=\"https://03.img.avito.st/image/1/9pRiGra_Wn0Uv6h7AjHa-5K5XHfceV6P0LlYe9q_WH3W_w 1.5x\">                            </div>                        </li>                    </ul>                </div>            </div>        </a></header>    <div class=\"styles-root-2Jty7\"><a href=\"/moskva/odezhda_obuv_aksessuary/belyy_sviter_s_lentoy_2006957136\"            target=\"_blank\" rel=\"noopener\" title=\"Белый свитер с лентой в Москве\" itemprop=\"url\" data-marker=\"title\"            class=\"link-link-39EVK link-design-default-2sPEv title-root-395AQ styles-title-2VgwA title-root_maxHeight-3obWc\"><span                itemprop=\"name\"                class=\"title-root-395AQ styles-title-2VgwA title-root_maxHeight-3obWc text-text-1PdBw text-size-s-1PUdo text-bold-3R9dt\">Белый                свитер с лентой</span></a><span class=\"price-root-1n2wM styles-price-1cOkp\"><span                class=\"price-price-32bra\" itemprop=\"offers\" itemscope=\"\" itemtype=\"http://schema.org/Offer\">                <meta itemprop=\"priceCurrency\" content=\"RUB\">                <meta itemprop=\"price\" content=\"500\">                <meta itemprop=\"availability\" content=\"https://schema.org/LimitedAvailability\"><span                    class=\"price-text-1HrJ_ text-text-1PdBw text-size-s-1PUdo\">500                    <!-- -->&nbsp;<span class=\"price-currency-LOpM3\">₽</span></span></span></span>        <div class=\"geo-root-1pUZ8\"><span class=\"geo-address-9QndR text-text-1PdBw text-size-s-1PUdo\"><span>Москва, Марьина Роща</span></span></div><span class=\"text-text-1PdBw text-size-s-1PUdo text-color-noaccent-bzEdI\">Сегодня 20:21</span>    </div></div>";
            _converter = new AvitoEntryConverter();
            _res = _converter.Convert(_data);
        }

        [TestMethod]
        public void ConvertImagesTest()
        {
            var _images = new List<string>() { "https://03.img.avito.st/image/1/9pRiGra_Wn0Uv6h7AjHa-5K5XHfceV6P0LlYe9q_WH3W_w 1.5x" };
            foreach (var t in _images)
            {
                Assert.IsTrue(_res.Images.Contains(t));
            }
        }

        [TestMethod]
        public void ConvertTitleTest()
        {
            Assert.AreEqual(_res.Title, "Объявление «Белый свитер с лентой»");
        }

        [TestMethod]
        public void ConvertLocationTest()
        {
            Assert.AreEqual(_res.Location, "Москва, Марьина Роща");
        }

        [TestMethod]
        public void ConvertPriceTest()
        {
            Assert.AreEqual(_res.Price, "500");
        }

        [TestMethod]
        public void ConvertPriceCurrencyTest()
        {
            Assert.AreEqual(_res.PriceCurrency, "RUB");
        }

        [TestMethod]
        public void ConvertTimeTest()
        {
            Assert.AreEqual(_res.Time, "Сегодня 20:21");
        }
    }
}
