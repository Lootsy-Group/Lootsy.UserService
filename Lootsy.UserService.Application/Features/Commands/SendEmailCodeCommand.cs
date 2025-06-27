using Lootsy.UserService.Application.Extensions;
using MediatR;

namespace Lootsy.UserService.Application.Features.Commands;

public record SendEmailCodeCommand(
    string Email
    ) : IRequest<Result>;
