namespace Lootsy.UserService.Application.Models;

public sealed record EmailMessage(
    string To,
    string UserName,
    string Subject,
    string? FallbackUrl
    );
