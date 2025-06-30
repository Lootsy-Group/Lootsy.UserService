using Lootsy.UserService.Application.Extensions;
using Lootsy.UserService.Application.Features.Commands;
using Lootsy.UserService.Application.Interfaces;
using Lootsy.UserService.Application.Models;
using MediatR;

namespace Lootsy.UserService.Application.Features.Handlers;

internal sealed class SendEmailCodeHandler : IRequestHandler<RegisterCommand, Result>
{
    private readonly ISmsCodeService _smsCodeService;
    private readonly IEmailService _emailService;

    public SendEmailCodeHandler(ISmsCodeService smsCodeService, IEmailService emailService)
    {
        _smsCodeService = smsCodeService ?? throw new ArgumentNullException(nameof(smsCodeService));
        _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
    }

    public async Task<Result> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var data = new RegisterTempData
        {
            Email = request.Email,
            Password = request.Password,
            PhoneNumber = request.PhoneNumber,
            FullName = request.FullName
        };

        var smsCode = new Random().Next(1000, 9999).ToString();

        await _smsCodeService.StoreAsync(request.Email, data, smsCode, cancellationToken);
        _emailService.SendEmailConfirmation(new EmailMessage(request.Email, request.FullName, $"Confirm your email{smsCode}", null));

        return Result.Success();
    }
}
