namespace HW_31._08._2026.Helpers;

public sealed class MongoDbSettings
{
    public string ConnectionString { get; set; } = string.Empty;
    public string DatabaseName { get; set; } = string.Empty;
    public string FeedbackCollectionName { get; set; } = "ProductFeedback";
}
