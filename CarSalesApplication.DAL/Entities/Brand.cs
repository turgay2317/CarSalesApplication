using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarSalesApplication.DAL.Entities;

public class Brand
{
    /// <summary>
    /// Marka IDsi
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id {get; set;}
    
    /// <summary>
    /// Marka adı
    /// </summary>
    [Required]
    public string Name { get; set; }
    
    /// <summary>
    /// Markaya ait modeller
    /// </summary>
    public ICollection<Model> Models { get; set; }
}