using System;
using System.ComponentModel.DataAnnotations;

namespace CMSAPI.DTOs;

public class RegisterDto
{
    [Required]
    public string DisplayName { get; set; } = "";

    [Required]
    [EmailAddress]
    public string Email { get; set; } = "";

    public string Password { get; set; } = "";
    [Required]
    public string UserType { get; set; } = "";
}
