using CarSalesApplication.BLL.DTOs.Responses.Brand;
using CarSalesApplication.BLL.DTOs.Responses.Model;
using CarSalesApplication.BLL.DTOs.Responses.User;
using CarSalesApplication.BLL.DTOs.Shared;

namespace CarSalesApplication.BLL.DTOs.Responses.Car;

public class CarDtoWithDetails
{
    // Car
    public int Id { get; set; }
    public string Title { get; set; }
    public BrandDto Brand { get; set; }
    public ModelDto Model { get; set; }
    public string Color { get; set; }
    public decimal Price { get; set; }
    public int Year { get; set; }
    public int Kilometers { get; set; }
    public ICollection<PhotoDto> Photos { get; set; }
    // Owner
    public UserDto User { get; set; }
    // Parts
    public ICollection<CarPartDto> Parts { get; set; }
}