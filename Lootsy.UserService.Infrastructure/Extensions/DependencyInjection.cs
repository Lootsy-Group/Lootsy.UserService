using Lootsy.UserService.Domain.Aggregates;
using Lootsy.UserService.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using Lootsy.UserService.Application.Configurations;
using System.Net;
using Lootsy.UserService.Application.Interfaces;
using Lootsy.UserService.Infrastructure.Email;

namespace Lootsy.UserService.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDBContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });

        AddIdentity(services);
        AddEmail(services, configuration);

        return services;
    }

    private static void AddIdentity(IServiceCollection services)
    {
        services.AddIdentity<User, IdentityRole<Guid>>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 8;

            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            options.User.RequireUniqueEmail = false;
            options.SignIn.RequireConfirmedAccount = false;
        })
        .AddEntityFrameworkStores<ApplicationDBContext>()
        .AddDefaultTokenProviders();

        services.Configure<DataProtectionTokenProviderOptions>(options =>
        {
            options.TokenLifespan = TimeSpan.FromHours(12);
        });
    }

    private static void AddEmail(IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetSection(EmailOptions.SectionName);
        var emailOptions = section.Get<EmailOptions>();

        if (emailOptions is null)
        {
            throw new InvalidOperationException("Email configuration settings did not load correctly.");
        }

        services.AddFluentEmail(emailOptions.FromEmail, emailOptions.FromName)
            .AddRazorRenderer()
            .AddSmtpSender(new SmtpClient
            {
                Port = emailOptions.Port,
                Credentials= new NetworkCredential(emailOptions.FromEmail, emailOptions.Password),
                EnableSsl = emailOptions.EnableSsl,
            });

        services.AddScoped<IEmailService, EmailService>();
    }
}
