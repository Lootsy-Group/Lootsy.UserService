using FluentEmail.Core;
using Lootsy.UserService.Application.Interfaces;
using Lootsy.UserService.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lootsy.UserService.Infrastructure.Email;

internal sealed class EmailService : IEmailService
{
    private readonly IFluentEmail _fluentEmail;

    public EmailService(IFluentEmail fluentEmail)
    {
        _fluentEmail = fluentEmail ?? throw new ArgumentNullException(nameof(fluentEmail));
    }

    public void SendEmailConfirmation(EmailMessage emailMessage)
    {
        _fluentEmail.To(emailMessage.To)
            .Subject(emailMessage.Subject)
            .Send();
    }

    public void SendPasswordReset(EmailMessage emailMessage)
    {
        throw new NotImplementedException();
    }
}
