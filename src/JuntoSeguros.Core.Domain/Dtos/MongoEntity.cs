using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace JuntoSeguros.Domain.Dtos;

public class MongoEntity
{
    [BsonId]
    public Guid Id { get; set; }
}
