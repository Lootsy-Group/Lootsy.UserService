﻿namespace Lootsy.UserService.Application.Models;

public class RegisterTempData
{
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string FullName { get; set; } = default!;
}
