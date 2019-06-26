using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAdvert.SearchApi.Models;

namespace WebAdvert.SearchApi.Services
{
    public class SearchService : ISearchService
    {
        private readonly IElasticClient _elasticClient;

        public SearchService(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }
        public async Task<List<AdvertType>> GetSearch(string keyword)
        {
            var seachResponse = await _elasticClient.SearchAsync<AdvertType>(search =>
               search.Query(query => query.Term(field => field.Title, keyword.ToLower())));

            return seachResponse.Hits.Select(hit => hit.Source).ToList();
        }
    }
}
