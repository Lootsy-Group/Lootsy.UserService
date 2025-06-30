using Lootsy.UserService.Application.Extensions;
using MediatR;

namespace Lootsy.UserService.Application.Features.Commands;

public record ConfirmEmailCodeCommand(
    string Email,
    string Code
    ) : IRequest<Result>;
