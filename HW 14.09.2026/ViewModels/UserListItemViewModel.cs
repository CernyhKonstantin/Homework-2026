namespace HW_14._09._2026.ViewModels;

public sealed class UserListItemViewModel
{
    public int Id { get; init; }
    public string Email { get; init; } = string.Empty;
    public string Role { get; init; } = string.Empty;
    public DateTime CreatedAt { get; init; }
}
