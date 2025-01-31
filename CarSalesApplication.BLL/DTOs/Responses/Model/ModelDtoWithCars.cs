using CarSalesApplication.BLL.DTOs.Responses.Car;

namespace CarSalesApplication.BLL.DTOs.Responses.Model;

public class ModelDtoWithCars : ModelDto
{
    public ICollection<CarDto> Cars { get; set; }
}