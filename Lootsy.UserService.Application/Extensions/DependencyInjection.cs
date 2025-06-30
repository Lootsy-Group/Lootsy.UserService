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
        RegisterRadis(services, configuration);

        return services;
    }

    public static IServiceCollection ImplementDI(this IServiceCollection services)
    {
        services.AddScoped<IJwtTokenService, JwtTokenService>();
        services.AddScoped<ISmsCodeService, SmsCodeService>();

        return services;
    }

    public static IServiceCollection RegisterRadis(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("RedisConnection");
        });

        return services;
    }
}
