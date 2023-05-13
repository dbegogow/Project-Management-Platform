using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

using static TeamService.Infrastructure.Validations.Validations;

namespace TeamService.Models.Data;

public class Team
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public string Id { get; init; }

    [BsonRequired]
    [StringLength(TeamNameMaxLength, MinimumLength = TeamNameMinLength)]
    public string Name { get; init; }

    [BsonRequired]
    [StringLength(TeamGoalsMaxLenght, MinimumLength = TeamGoalsMinLenght)]
    public string Goals { get; init; }

    [Required]
    public IEnumerable<string> Members { get; init; }
}
