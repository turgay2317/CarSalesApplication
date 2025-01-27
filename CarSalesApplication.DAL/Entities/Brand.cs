using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarSalesApplication.DAL.Entities;

public class Brand
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id {get; set;}
    [Required]
    public string Name { get; set; }
    // Nav
    public ICollection<Model> Models { get; set; }
}