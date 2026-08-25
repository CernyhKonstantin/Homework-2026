namespace HW_24._08._2026.Services.Interfaces;

public interface IUserQueuePublisher
{
    Task PublishUserRegisteredAsync(int userId, string email);
}
