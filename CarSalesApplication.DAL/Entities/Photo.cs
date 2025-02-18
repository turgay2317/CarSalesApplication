using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarSalesApplication.DAL.Entities;

public class Photo
{
    [Key]
    public int Id { get; set; }
    /// <summary>
    /// Blob tipindeki fotoğraf datası
    /// </summary>
    public byte[] Data { get; set; }
    public int CarId { get; set; }
    /// <summary>
    /// Fotoğrafın ait olduğu araç
    /// </summary>
    public Car Car { get; set; }
    /// <summary>
    /// Fotoğrafın yüklenme tarihi
    /// </summary>
    public DateTime CreatedAt { get; set; }
}