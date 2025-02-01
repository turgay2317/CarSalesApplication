namespace CarSalesApplication.DAL.Entities;

public class PriceChange
{
    public int CarId { get; set; }
    public decimal OldPrice { get; set; }
    public decimal NewPrice { get; set; }
    public DateTime ChangeDate { get; set; }
}
