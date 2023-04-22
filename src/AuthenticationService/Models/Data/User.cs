using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace AuthenticationService.Models.Data;

[CollectionName("Users")]
public class User : MongoIdentityUser<Guid>
{
}
