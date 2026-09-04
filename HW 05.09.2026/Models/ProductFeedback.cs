using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HW_05._09._2026.Models;

public class ProductFeedback
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

    [BsonElement("productId")]
    public int ProductId { get; set; }

    [BsonElement("userId")]
    public int UserId { get; set; }

    [BsonElement("userEmail")]
    public string UserEmail { get; set; } = string.Empty;

    [BsonElement("type")]
    public string Type { get; set; } = string.Empty;

    [BsonElement("message")]
    public string Message { get; set; } = string.Empty;

    [BsonElement("rating")]
    public int? Rating { get; set; }

    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
