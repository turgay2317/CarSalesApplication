namespace CarSalesApplication.BLL.DTOs.Responses;

public class ModelDto
{
    public string Name { get; set; }
    public ICollection<CarDto>? Cars { get; set; }
}