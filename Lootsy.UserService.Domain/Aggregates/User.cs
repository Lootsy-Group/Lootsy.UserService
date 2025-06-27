using Microsoft.AspNetCore.Identity;

namespace Lootsy.UserService.Domain.Aggregates;

public class User : IdentityUser<Guid>
{
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }
}
