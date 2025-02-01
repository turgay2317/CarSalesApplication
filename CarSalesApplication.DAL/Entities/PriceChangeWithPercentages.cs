namespace CarSalesApplication.DAL.Entities;

public class PriceChangeWithPercentages
{
    public decimal WeeklyChangeRate { get; set; }
    public decimal MonthlyChangeRate { get; set; }
    public decimal ThreeMonthlyChangeRate { get; set; }
    public decimal YearlyChangeRate { get; set; }
    public ICollection<PriceChange> PriceChanges { get; set; }
}
