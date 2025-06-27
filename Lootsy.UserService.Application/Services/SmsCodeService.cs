using Lootsy.UserService.Application.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace Lootsy.UserService.Application.Services;

internal sealed class SmsCodeService : ISmsCodeService
{
    private readonly IMemoryCache _memoryCache;
    private readonly TimeSpan _expirationTime = TimeSpan.FromMinutes(5);

    public SmsCodeService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public void SaveCode(string email, string smsCode)
    {
        _memoryCache.Set(email, smsCode, _expirationTime);
    }

    public string GetCode(string email)
    {
        _memoryCache.TryGetValue(email, out string smsCode);
        return smsCode;
    }

    public bool ValidateCode(string email, string enteredCode)
    {
        var storedCode = GetCode(email);
        return storedCode != null && storedCode == enteredCode;
    }
}
