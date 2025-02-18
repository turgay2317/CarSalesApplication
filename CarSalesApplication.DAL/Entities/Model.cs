using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarSalesApplication.DAL.Entities;

[Table("Models")]
public class Model
{
    /// <summary>
    /// Model IDsi
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id {get; set;}
    /// <summary>
    /// Model adı
    /// </summary>
    [Required]
    public string Name { get; set; }
    
    [Required]
    public int BrandId { get; set; }
    /// <summary>
    /// Model hangi markaya ait
    /// </summary>
    [Required]
    public Brand Brand { get; set; }
    
    /// <summary>
    /// Modele ait araçlar
    /// </summary>
    public ICollection<Car> Cars { get; set; }
}