using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CarSalesApplication.Core.Enums;

namespace CarSalesApplication.DAL.Entities;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    /// <summary>
    /// Kullanıcının adı
    /// </summary>
    [Required]
    public string Name { get; set; }
    /// <summary>
    /// Kullanıcının soyadı
    /// </summary>
    [Required]
    public string Surname { get; set; }
    /// <summary>
    /// Kullanıcının e-postası
    /// </summary>
    [Required]
    public string Email  { get; set; }
    /// <summary>
    /// Kullanıcının şifresi
    /// </summary>
    [Required]
    public string Password { get; set; }
    /// <summary>
    /// Kullanıcının doğum günü
    /// </summary>
    [Required]
    public DateTime Birthday { get; set; }
    /// <summary>
    /// Kullanıcının hesap oluşturma tarihi
    /// </summary>
    [Required]
    public DateTime Created { get; set; }
    /// <summary>
    /// Kullanıcının yetki tipi
    /// </summary>
    [Required]
    public UserStatus UserType { get; set; }
}