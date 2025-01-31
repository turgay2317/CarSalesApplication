
using System.ComponentModel;
using System.Text.Json.Serialization;
using CarSalesApplication.BLL.DTOs.Responses.Model;

namespace CarSalesApplication.BLL.DTOs.Responses.Brand;

public class BrandDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}