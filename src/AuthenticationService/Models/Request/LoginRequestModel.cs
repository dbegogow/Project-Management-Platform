using System.ComponentModel.DataAnnotations;

namespace AuthenticationService.Models.Request;

public class LoginRequestModel
{
    [Required]
    [EmailAddress]
    public string Email { get; init; }

    [Required]
    public string Password { get; set; }
}
