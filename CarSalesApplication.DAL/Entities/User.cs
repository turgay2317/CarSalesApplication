using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CarSalesApplication.Core.Enums;

namespace CarSalesApplication.DAL.Entities;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Surname { get; set; }
    [Required]
    public string Email  { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public DateTime Birthday { get; set; }
    [Required]
    public DateTime Created { get; set; }
    [Required]
    public UserType UserType { get; set; }
}