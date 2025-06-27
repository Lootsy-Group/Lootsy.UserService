using Lootsy.UserService.Application.Extensions;
using Lootsy.UserService.Application.Features.Commands;
using Lootsy.UserService.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Pipelines.Sockets.Unofficial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lootsy.UserService.Application.Features.Handlers;

internal sealed class SendEmailCodeHandler : IRequestHandler<SendEmailCodeCommand, Result>
{
    private readonly ISmsCodeService _smsCodeService;

    public SendEmailCodeHandler(ISmsCodeService smsCodeService)
    {
        _smsCodeService = smsCodeService ?? throw new ArgumentNullException(nameof(smsCodeService));
    }

    public async Task<Result> Handle(SendEmailCodeCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var smsCode = new Random().Next(1000, 9999).ToString();



        _smsCodeService.SaveCode(request.Email, smsCode);

        throw new NotImplementedException();
    }
}
