using System.ComponentModel.DataAnnotations;

namespace CarSalesApplication.BLL.DTOs.Requests;

public class RegisterRequestDto
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Surname is required")]
    public string Surname { get; set; }
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email  { get; set; }
    [Required(ErrorMessage = "Password is required")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters")]
    public string Password { get; set; }
    [Required(ErrorMessage = "Birthday is required")]
    [DataType(DataType.Date)]
    public DateTime Birthday { get; set; }
}