using CarSalesApplication.BLL.DTOs.Responses.Car;
using CarSalesApplication.BLL.Interfaces;
using Elastic.Clients.Elasticsearch;


namespace CarSalesApplication.BLL.Services
{
    public class ElasticSearchService : IElasticSearchService
    {
        private readonly ElasticsearchClient _elasticClient;
        private string _defaultIndex = "cars";

        public ElasticSearchService(ElasticsearchClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public async Task<bool> AddOrUpdate(CarDto car)
        {
            var response = await _elasticClient.IndexAsync(car, idx =>
                idx.Index(this._defaultIndex)
                    .OpType(OpType.Index));
            return response.IsValidResponse;
        }
        
        public async Task<bool> AddOrUpdateBulk(List<CarDto> cars, string indexName)
        {
            var response = await _elasticClient.BulkAsync(b => b.Index(_defaultIndex)
                .UpdateMany(cars, (ud, u) => ud.Doc(u).DocAsUpsert(true)));
            return response.IsValidResponse;
        }

        public async Task<List<CarDto>> GetAll(string keyword)
        {
            var response = await _elasticClient.SearchAsync<CarDto>(s => s
                .Index(_defaultIndex)
                .Query(q => q
                    .Bool(b => b
                            .Should(
                                sh => sh.Match(m => m
                                    .Field(f => f.Title)
                                    .Query(keyword)
                                    .Fuzziness(new Fuzziness(2))
                                ),
                                sh => sh.Match(m => m
                                    .Field(f => f.Brand) 
                                    .Query(keyword)
                                    .Fuzziness(new Fuzziness(2))
                                ),
                                sh => sh.Match(m => m
                                    .Field(f => f.Model)
                                    .Query(keyword)
                                    .Fuzziness(new Fuzziness(2))
                                )
                            )
                            .MinimumShouldMatch(1) 
                    )
                )
            );

            return response.Documents.ToList();
        }
    }
}
