using Lootsy.UserService.Domain.Aggregates;

namespace Lootsy.UserService.Application.Interfaces;

public interface IJwtTokenService
{
    string GenerateToken(User user);
}
