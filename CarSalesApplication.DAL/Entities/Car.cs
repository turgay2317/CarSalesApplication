using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CarSalesApplication.Core.Enums;
using Microsoft.EntityFrameworkCore;

namespace CarSalesApplication.DAL.Entities;

public class Car
{
    /// <summary>
    /// Aracın ID'sidir.
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    /// <summary>
    /// Araç ilanının başlığıdır.
    /// </summary>
    [Required]
    public string Title { get; set; }
    
    [Required]
    public int BrandId { get; set; }
    
    /// <summary>
    /// Aracın ait olduğu markadır.
    /// </summary>
    [Required]
    public Brand _Brand { get; set; }
    
    [Required]
    public int ModelId { get; set; }
    
    /// <summary>
    /// Aracın ait olduğu modeldir.
    /// </summary>
    [Required]
    public Model _Model { get; set; }
    
    /// <summary>
    /// Aracın rengi
    /// </summary>
    [Required]
    public string Color { get; set; }
    
    /// <summary>
    /// Aracın satış fiyatı
    /// </summary>
    [Required]
    public decimal Price { get; set; }
    
    /// <summary>
    /// Aracın üretim yılı
    /// </summary>
    [Range(1970, 2025)]
    public int Year { get; set; }
    
    /// <summary>
    /// Aracın mevcut kilometresi
    /// </summary>
    [Range(0,1000000)]
    public int Kilometers { get; set; }
    
    /// <summary>
    /// İlan oluşturulma tarihi
    /// </summary>
    [Required]
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// İlan Durumu, yayında mı değil mi
    /// </summary>
    [Required]
    public PostStatus Status { get; set; }
    
    [Required]
    public int UserId { get; set; }
    
    /// <summary>
    /// İlanı yayınlayan kullanıcı
    /// </summary>
    public User User { get; set; }
    /// <summary>
    /// İlan fotoğrafları
    /// </summary>
    public ICollection<Photo> Photos { get; set; }
    /// <summary>
    /// Araç parçaları ve orijinallik durumları
    /// </summary>
    public ICollection<CarPart> CarParts { get; set; }
}