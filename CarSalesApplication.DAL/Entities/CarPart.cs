using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CarSalesApplication.Core.Enums;
using Microsoft.EntityFrameworkCore;

namespace CarSalesApplication.DAL.Entities;

[Index(nameof(CarId), nameof(PartId), IsUnique = true)]
public class CarPart
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    /// <summary>
    /// Parçanın durumu
    /// </summary>
    [Required]
    public PartStatus Status { get; set; }
    
    public Part Part { get; set; }
    /// <summary>
    /// Parça hangi arabaya ait
    /// </summary>
    public Car Car { get; set; }
    public int CarId { get; set; }
    /// <summary>
    /// Parça detayı
    /// </summary>
    public int PartId { get; set; }
}