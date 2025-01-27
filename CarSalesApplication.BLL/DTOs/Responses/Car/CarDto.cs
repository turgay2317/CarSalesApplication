using CarSalesApplication.BLL.DTOs.Shared;

namespace CarSalesApplication.BLL.DTOs.Responses;

public class CarDto
{
    public BrandDto brand { get; set; }
    public ModelDto model { get; set; }
    public string color { get; set; }
    public decimal price { get; set; }
    public int year { get; set; }
    public ICollection<PhotoDto> photos { get; set; }
}