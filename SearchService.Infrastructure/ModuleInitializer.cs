using CommomInterface;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Nest;
using SearchService.Domain;

namespace SearchService.Infrastructure
{
    public class ModuleInitializer : IModuleInitializer
    {
        public void Initialize(IServiceCollection services)
        {
            services.AddScoped<IElasticClient>(sp =>
            {
                var ElasticSearchUrl = Environment.GetEnvironmentVariable("ElasticSearchUrl");
                //var option = sp.GetRequiredService<IOptions<ElasticSearchOptions>>();
                var settings = new ConnectionSettings(new Uri(ElasticSearchUrl));
                return new ElasticClient(settings);
            });
            services.AddScoped<ISearchRepository, SearchRepository>();
        }
    }
}
