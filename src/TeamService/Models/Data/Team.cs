using MongoDB.Bson.Serialization.Attributes;

namespace TeamService.Models.Data;

public class Team
{
    [BsonId]
    public string Id { get; init; }

    public string Name { get; init; }

    public string Goals { get; init; }

    public IEnumerable<string> Members { get; init; }
}
