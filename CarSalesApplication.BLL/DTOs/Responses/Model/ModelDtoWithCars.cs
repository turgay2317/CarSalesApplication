using CarSalesApplication.BLL.DTOs.Responses.Car;

namespace CarSalesApplication.BLL.DTOs.Responses.Model;

public class ModelDtoWithCars
{
    public ModelDto Model { get; set; }
    public ICollection<CarDto> Cars { get; set; }
}