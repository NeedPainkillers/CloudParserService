using Microsoft.Extensions.DependencyInjection;

namespace CloudParserService.Registry
{
    interface IRegistrar
    {
        public void Register(IServiceCollection services);
    }
}
