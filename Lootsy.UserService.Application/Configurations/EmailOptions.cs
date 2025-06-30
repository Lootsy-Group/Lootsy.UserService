using System.ComponentModel.DataAnnotations;

namespace Lootsy.UserService.Application.Configurations;

public class EmailOptions
{
    public const string SectionName = nameof(EmailOptions);

    [Required(ErrorMessage = "From Email is required")]
    public required string FromEmail { get; set; } = default!;
    [Required(ErrorMessage = "From Name is required")]
    public required string FromName { get; set; } = default!;
    [Required(ErrorMessage = "Password is required")]
    public required string Password { get; set; } = default!;
    [Required(ErrorMessage = "Host is required")]
    public int Port { get; set; }
    [Required(ErrorMessage = "Host is required")]
    public bool EnableSsl { get; set; } = true;
}
