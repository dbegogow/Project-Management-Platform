﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace TeamService.Models.Data;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public string Id { get; init; }

    public string Username { get; init; }
}
