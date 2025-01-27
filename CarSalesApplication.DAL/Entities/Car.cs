using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CarSalesApplication.Core.Enums;
using Microsoft.EntityFrameworkCore;

namespace CarSalesApplication.DAL.Entities;

public class Car
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    public int BrandId { get; set; }
    
    [Required]
    public Brand _Brand { get; set; }
    
    [Required]
    public int ModelId { get; set; }
    
    [Required]
    public Model _Model { get; set; }
    
    [Required]
    public string Color { get; set; }
    
    [Required]
    public decimal Price { get; set; }
    
    [Range(1970, 2025)]
    public int Year { get; set; }
    [Required]
    public DateTime CreatedAt { get; set; }
    [Required]
    public PostType Status { get; set; }
    
    [Required]
    public int UserId { get; set; }
    /* Navigation props */
    public User User { get; set; }
    public ICollection<Photo> Photos { get; set; }
}