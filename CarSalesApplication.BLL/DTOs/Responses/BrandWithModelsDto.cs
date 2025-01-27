namespace CarSalesApplication.BLL.DTOs.Responses;

public class BrandWithModelsDto
{
    public string Name { get; set; }
    public ICollection<ModelDto> Models { get; set; }
}