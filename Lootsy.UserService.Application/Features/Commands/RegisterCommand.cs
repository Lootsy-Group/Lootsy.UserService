using Lootsy.UserService.Application.Extensions;
using MediatR;

namespace Lootsy.UserService.Application.Features.Commands;

public record RegisterCommand(
    string Email,
    string Password,
    string PhoneNumber,
    string FullName
    ) : IRequest<Result>;
