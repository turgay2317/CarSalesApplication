using CarSalesApplication.Core.Enums;

namespace CarSalesApplication.BLL.DTOs.Shared;

public class CarPartDto
{
    public string Name { get; set; }
    public PartStatus Status { get; set; }
}