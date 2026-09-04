using HW_31._08._2026.Helpers;
using HW_31._08._2026.Models;
using HW_31._08._2026.Services.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace HW_31._08._2026.Services;

public class ProductFeedbackService : IProductFeedbackService
{
    private readonly IMongoCollection<ProductFeedback> _collection;

    public ProductFeedbackService(IOptions<MongoDbSettings> options)
    {
        var settings = options.Value;

        if (string.IsNullOrWhiteSpace(settings.ConnectionString))
            throw new InvalidOperationException("MongoDb:ConnectionString is missing.");

        if (string.IsNullOrWhiteSpace(settings.DatabaseName))
            throw new InvalidOperationException("MongoDb:DatabaseName is missing.");

        var client = new MongoClient(settings.ConnectionString);
        var database = client.GetDatabase(settings.DatabaseName);
        _collection = database.GetCollection<ProductFeedback>(settings.FeedbackCollectionName);
    }

    public async Task<ProductFeedback?> CreateAsync(
        int productId,
        int userId,
        string userEmail,
        string type,
        string message,
        int? rating)
    {
        var feedback = new ProductFeedback
        {
            ProductId = productId,
            UserId = userId,
            UserEmail = userEmail,
            Type = type.Trim().ToLowerInvariant(),
            Message = message.Trim(),
            Rating = rating,
            CreatedAt = DateTime.UtcNow
        };

        await _collection.InsertOneAsync(feedback);
        return feedback;
    }

    public Task<List<ProductFeedback>> GetByProductIdAsync(int productId)
    {
        return _collection
            .Find(x => x.ProductId == productId)
            .SortByDescending(x => x.CreatedAt)
            .ToListAsync();
    }
}
