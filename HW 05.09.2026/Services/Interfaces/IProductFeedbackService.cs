using HW_05._09._2026.Models;

namespace HW_05._09._2026.Services.Interfaces;

public interface IProductFeedbackService
{
    Task<ProductFeedback?> CreateAsync(int productId, int userId, string userEmail, string type, string message, int? rating);
    Task<List<ProductFeedback>> GetByProductIdAsync(int productId);
}
