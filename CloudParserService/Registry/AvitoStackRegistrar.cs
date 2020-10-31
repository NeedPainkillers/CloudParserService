using CloudParserService.Lib;
using Microsoft.Extensions.DependencyInjection;

namespace CloudParserService.Registry
{
    public class AvitoStackRegistrar : IRegistrar
    {
        public void Register(IServiceCollection services)
        {
            services.AddSingleton<IConvert, AvitoEntryConverter>();
            services.AddTransient<IParser, AvitoParser>();
        }
    }
}
