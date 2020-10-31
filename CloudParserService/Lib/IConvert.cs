using CloudParserService.Model;

namespace CloudParserService.Lib
{
    public interface IConvert
    {
        public Entry Convert(string data);
    }
}
