
namespace CarSalesApplication.BLL.DTOs.Responses;

public class BrandDto
{
    public string Name { get; set; }
    public ICollection<ModelDto>? Models { get; set; }
}