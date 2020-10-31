using CloudParserService.Lib;
using Microsoft.Extensions.DependencyInjection;

namespace CloudParserService.Registry
{
    public class MongoDBRegistrar : IRegistrar
    {
        public void Register(IServiceCollection services)
        {
            services.AddTransient<IDBWriter, MongoDBWriter>();
        }
    }
}
