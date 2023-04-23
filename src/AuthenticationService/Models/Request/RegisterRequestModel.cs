using System.ComponentModel.DataAnnotations;

namespace AuthenticationService.Models.Request;

public class RegisterRequestModel
{
    [Required]
    public string Username { get; init; }

    [Required]
    [EmailAddress]
    public string Email { get; init; }

    [Required]
    public string Password { get; init; }

    [Required]
    public string Role { get; init; }
}
