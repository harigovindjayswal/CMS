using System;
using Microsoft.AspNetCore.Identity;

namespace Persistence.Identity;

public class User : IdentityUser
{
    public string? DisplayName { get; set; }
    public string? Bio { get; set; }
    public string? ImageUrl { get; set; }
    public DateTime CreatedDate { get; set; }
}
