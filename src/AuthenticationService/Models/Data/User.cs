using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace AuthenticationService.Models.Data;

[CollectionName("Users")]
public class User : MongoIdentityUser<Guid>
{
    public string Username { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }
}
