using Lootsy.UserService.Application.Extensions;
using Lootsy.UserService.Application.Features.Commands;
using Lootsy.UserService.Application.Interfaces;
using Lootsy.UserService.Domain.Aggregates;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Lootsy.UserService.Application.Features.Handlers;

internal sealed class ConfirmEmailCodeHandler : IRequestHandler<ConfirmEmailCodeCommand, Result>
{
    private readonly ISmsCodeService _smsCodeService;
    private readonly UserManager<User> _userManager;

    public ConfirmEmailCodeHandler(
        ISmsCodeService smsCodeService,
        UserManager<User> userManager)
    {
        _smsCodeService = smsCodeService ?? throw new ArgumentNullException(nameof(smsCodeService));
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
    }

    public async Task<Result> Handle(ConfirmEmailCodeCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var (data, code) = await _smsCodeService.GetAsync(request.Email, cancellationToken);

        if (data == null || code == null)
            throw new ArgumentNullException("Invalid or expired code");

        var user = new User
        {
            Email = data.Email,
            UserName = data.Email,
            PhoneNumber = data.PhoneNumber,
            FullName = data.FullName,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(user, data.Password);
        if (!result.Succeeded)
            throw new Exception("Registration failed");

        await _smsCodeService.RemoveAsync(request.Email, cancellationToken);

        return Result.Success();
    }
}
