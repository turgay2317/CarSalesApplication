using System.Text.Json.Serialization;
using CarSalesApplication.BLL.DTOs.Responses.Brand;
using CarSalesApplication.BLL.DTOs.Responses.Model;
using CarSalesApplication.BLL.DTOs.Shared;

namespace CarSalesApplication.BLL.DTOs.Responses.Car;

public class CarDto
{
    public int Id { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public BrandDto brand { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ModelDto model { get; set; }
    public string color { get; set; }
    public decimal price { get; set; }
    public int year { get; set; }
    public ICollection<PhotoDto> photos { get; set; }
}