using CarSalesApplication.BLL.DTOs.Responses.Brand;
using CarSalesApplication.BLL.DTOs.Responses.Model;
using CarSalesApplication.BLL.DTOs.Responses.User;
using CarSalesApplication.BLL.DTOs.Shared;

namespace CarSalesApplication.BLL.DTOs.Responses.Car;

public class CarDtoWithDetails : CarDto
{
    public int Kilometers { get; set; }
    public UserDto User { get; set; }
    public ICollection<CarPartDto> Parts { get; set; }
}