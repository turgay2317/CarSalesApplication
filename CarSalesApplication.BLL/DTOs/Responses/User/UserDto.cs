using System.Text.Json.Serialization;

namespace CarSalesApplication.BLL.DTOs.Responses.User;

public class UserDto
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email  { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public DateTime Birthday { get; set; }
    public int Age => DateTime.Now.Year - Birthday.Year;
}