using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAdvert.SearchApi.Models;

namespace WebAdvert.SearchApi.Extensions
{
    public static class ElasticSearchLoggerExtension
    {

        public static void AddElasticSearchInstance(this IServiceCollection services, IConfiguration configuration)
        {
            var url = configuration.GetSection("ES").GetValue<string>("url");

            var connectionSettings = new ConnectionSettings(new Uri(url))
                                    .DefaultIndex("adverts")
                                    .DefaultTypeName("advert")
                                    .DefaultMappingFor<AdvertType>(m => m.IdProperty(x => x.Id));

            var client = new ElasticClient(connectionSettings);
            services.AddSingleton<IElasticClient>(client);
        }
    }
}
