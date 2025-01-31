using CarSalesApplication.BLL.DTOs.Responses.Model;

namespace CarSalesApplication.BLL.DTOs.Responses.Brand;

public class BrandDtoWithModels : BrandDto
{
    public ICollection<ModelDto> Models { get; set; }
}