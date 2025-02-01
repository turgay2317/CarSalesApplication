using CarSalesApplication.BLL.DTOs.Responses.User;

namespace CarSalesApplication.BLL.DTOs.Responses.Car;

public class CarDtoWithProfile : CarDto
{
    public UserDto User { get; set; }
}