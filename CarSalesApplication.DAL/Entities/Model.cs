using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarSalesApplication.DAL.Entities;

[Table("Models")]
public class Model
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id {get; set;}
    [Required]
    public string Name { get; set; }
    // Many-to-one
    [Required]
    public int BrandId { get; set; }
    [Required]
    public Brand Brand { get; set; }
    
    public ICollection<Car> Cars { get; set; }
}