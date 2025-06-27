using FluentValidation;
using Lootsy.UserService.Application.Interfaces;
using Lootsy.UserService.Application.Services;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Lootsy.UserService.Application.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection RegisterApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());

        services.AddValidatorsFromAssembly(typeof(MediatR.ServiceCollectionExtensions).Assembly);

        //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        ImplementDI(services);

        return services;
    }

    public static IServiceCollection ImplementDI(this IServiceCollection services)
    {
        // services.AddScoped<ICurrentUserService, CurrentUserService>();
        // services.AddScoped<IJwtTokenService, JwtTokenService>();

        services.AddScoped<ISmsCodeService, SmsCodeService>();

        return services;
    }
}
