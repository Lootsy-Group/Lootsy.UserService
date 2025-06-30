using Lootsy.UserService.Application.Models;

namespace Lootsy.UserService.Application.Interfaces;

public interface ISmsCodeService
{
    Task StoreAsync(string email, RegisterTempData data, string code, CancellationToken cancellationToken = default);
    Task<(RegisterTempData? Data, string? Code)> GetAsync(string email, CancellationToken cancellationToken = default);
    Task RemoveAsync(string email, CancellationToken cancellationToken = default);
}
