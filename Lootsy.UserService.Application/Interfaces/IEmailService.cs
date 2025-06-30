using Lootsy.UserService.Application.Models;

namespace Lootsy.UserService.Application.Interfaces;

public interface IEmailService
{
    void SendEmailConfirmation(EmailMessage emailMessage);
    void SendPasswordReset(EmailMessage emailMessage);
}
