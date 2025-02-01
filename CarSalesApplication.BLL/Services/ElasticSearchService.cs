using CarSalesApplication.DAL.Entities;
using Elastic.Clients.Elasticsearch;

namespace CarSalesApplication.BLL.Services;

public class ElasticSearchService
{
    private readonly ElasticsearchClient _client;

    public ElasticSearchService()
    {
        var settings = new ElasticsearchClientSettings(new Uri("http://localhost:9200"));
        _client = new ElasticsearchClient(settings);
    }

    public async Task IndexPriceChangeAsync(PriceChange priceChange)
    {
        Console.WriteLine("Price change request has come");
        Console.WriteLine($"Price: {priceChange}");
        try
        {
            // UniqueID = CarId + ChangeDate
            var uniqueId = $"{priceChange.CarId}-{priceChange.ChangeDate:yyyyMMddHHmmss}";

            var indexResponse = await _client.IndexAsync(priceChange, idx => idx
                    .Index("price-changes")
                    .Id(uniqueId)  
            );

            if (!indexResponse.IsValidResponse)
            {
                throw new Exception("Error indexing price change: " + (indexResponse.DebugInformation ?? "Unknown error"));
            }
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while indexing the price change: " + ex.Message);
        }
    }



   public async Task<PriceChangeWithPercentages> GetPriceChangesWithPercentagesAsync(int carId)
    {
        var now = DateTime.UtcNow;
        var weekStart = now.AddDays(-7);
        var monthStart = now.AddMonths(-1);
        var threeMonthsStart = now.AddMonths(-3);
        var yearStart = now.AddYears(-1);

        var searchResponse = await _client.SearchAsync<PriceChange>(s => s
            .Index("price-changes")
            .Query(q => q
                .Bool(b => b
                    .Must(m => m.Match(mm => mm.Field(f => f.CarId).Query(carId.ToString())))
                    .Filter(f => f.Range(r => r.DateRange(dr => dr.Field(f => f.ChangeDate)
                        .Gte(yearStart)
                    )))
                )
            )
            .Sort(descriptor => descriptor.Field(pc => pc.ChangeDate))
        );

        if (!searchResponse.IsValidResponse)
        {
            throw new Exception("Error searching price changes: " + searchResponse.DebugInformation);
        }

        var priceChanges = searchResponse.Documents.ToList();

        var weeklyChangeRate = CalculatePercentageChange(priceChanges, weekStart);
        var monthlyChangeRate = CalculatePercentageChange(priceChanges, monthStart);
        var threeMonthlyChangeRate = CalculatePercentageChange(priceChanges, threeMonthsStart);
        var yearlyChangeRate = CalculatePercentageChange(priceChanges, yearStart);

        var result = new PriceChangeWithPercentages
        {
            WeeklyChangeRate = weeklyChangeRate,
            MonthlyChangeRate = monthlyChangeRate,
            ThreeMonthlyChangeRate = threeMonthlyChangeRate,
            YearlyChangeRate = yearlyChangeRate,
            PriceChanges = priceChanges
        };

        return result;
    }

    private decimal CalculatePercentageChange(List<PriceChange> priceChanges, DateTime startDate)
    {
        var relevantChanges = priceChanges
            .Where(pc => pc.ChangeDate >= startDate)
            .OrderBy(pc => pc.ChangeDate)
            .ToList();

        if (relevantChanges.Count < 2) return 0; 

        var firstChange = relevantChanges.First();
        var lastChange = relevantChanges.Last();

        var percentageChange = (lastChange.NewPrice / firstChange.OldPrice) * 100;

        return Math.Round(percentageChange, 2); 
    }

}