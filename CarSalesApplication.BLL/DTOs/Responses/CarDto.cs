namespace CarSalesApplication.BLL.DTOs.Responses;

public class CarDto
{
    public BrandDto _brand { get; set; }
    public string model { get; set; }
    public string color { get; set; }
    public decimal price { get; set; }
    public int year { get; set; }
}