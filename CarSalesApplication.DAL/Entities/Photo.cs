using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarSalesApplication.DAL.Entities;

public class Photo
{
    [Key]
    public int Id { get; set; }
    public byte[] Data { get; set; }
    // FK
    public int CarId { get; set; }
    public Car Car { get; set; }
    public DateTime CreatedAt { get; set; }
}