using Lootsy.UserService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Lootsy.UserService.Api.Extensions;

public static class DatabaseExtensions
{ 
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();
        using ApplicationDBContext dbContext =
            scope.ServiceProvider.GetRequiredService<ApplicationDBContext>();

        dbContext.Database.Migrate();
    }
}
