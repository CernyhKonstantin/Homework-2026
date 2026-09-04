namespace HW_05._09._2026.Services.Interfaces;

public interface IUserQueuePublisher
{
    Task PublishUserRegisteredAsync(int userId, string email);
}
