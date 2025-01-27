using System.ComponentModel.DataAnnotations;
using CarSalesApplication.BLL.DTOs.Shared;

namespace CarSalesApplication.BLL.DTOs.Requests.Car;

public class NewCarRequestDto
{
    [Required]
    public int BrandId { get; set; }
    [Required]
    public int ModelId { get; set; }
    [Required]
    public string Color { get; set; }
    [Required]
    public decimal Price { get; set; }
    [Range(1970, 2025)]
    public int Year { get; set; }
    public ICollection<PhotoDto> Photos { get; set; }
}