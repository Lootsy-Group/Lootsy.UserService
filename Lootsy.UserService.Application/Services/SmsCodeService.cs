using Lootsy.UserService.Application.Interfaces;
using Lootsy.UserService.Application.Models;
using Microsoft.Extensions.Caching.Distributed;

namespace Lootsy.UserService.Application.Services;

internal sealed class SmsCodeService : ISmsCodeService
{
    private readonly IDistributedCache _distributedCache;
    private const int ExpirationMinutes = 5;

    public SmsCodeService(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache ?? throw new ArgumentNullException(nameof(distributedCache));
    }

    public async Task StoreAsync(string email, RegisterTempData data, string code, CancellationToken cancellationToken = default)
    {
        var combined = new RegisterTempWrapper
        {
            Data = data,
            Code = code
        };

        var json = System.Text.Json.JsonSerializer.Serialize(combined);

        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(ExpirationMinutes)
        };

        await _distributedCache.SetStringAsync(GetKey(email), json, options, cancellationToken).ConfigureAwait(false);
    }

    public async Task<(RegisterTempData? Data, string? Code)> GetAsync(string email, CancellationToken cancellationToken = default)
    {
        var json = await _distributedCache.GetStringAsync(GetKey(email), cancellationToken);

        if (string.IsNullOrEmpty(json))
            return (null, null);

        var obj = System.Text.Json.JsonSerializer.Deserialize<RegisterTempWrapper>(json);

        return (obj.Data, obj.Code);
    }

    public async Task RemoveAsync(string email, CancellationToken cancellationToken = default)
    {
        await _distributedCache.RemoveAsync(GetKey(email), cancellationToken);
    }

    private static string GetKey(string email) => $"auth:register:{email}";

    private class RegisterTempWrapper
    {
        public RegisterTempData Data { get; set; } = default!;
        public string Code { get; set; } = default!;
    }
}
