using System.Text.Json.Serialization;
using CarSalesApplication.BLL.DTOs.Responses.Brand;
using CarSalesApplication.BLL.DTOs.Responses.Model;
using CarSalesApplication.BLL.DTOs.Shared;

namespace CarSalesApplication.BLL.DTOs.Responses.Car;

public class CarDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public BrandDto Brand { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ModelDto Model { get; set; }
    public string Color { get; set; }
    public decimal Price { get; set; }
    public int Year { get; set; }
    public ICollection<PhotoDto> Photos { get; set; }
}