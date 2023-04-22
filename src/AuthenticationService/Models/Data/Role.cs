using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace AuthenticationService.Models.Data;

[CollectionName("Roles")]
public class Role : MongoIdentityRole<Guid>
{
}
