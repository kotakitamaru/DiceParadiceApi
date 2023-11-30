using System.ComponentModel.DataAnnotations;

namespace DiceParadiceApi.Models.DataTransferObjects;

public class UserForRegistrationDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Role { get; set; }
}