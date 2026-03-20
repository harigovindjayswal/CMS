using System.Security.Claims;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Security;

public class UserAccessor(IHttpContextAccessor httpContextAccessor) : IUserAccessor
{
    public string? GetUserId()
    {
        return httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
    }

    public string? GetUserRole()
    {
        // We treat "UserType" as the primary role for UI routing; token contains standard role claims.
        return httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Role);
    }

    public bool IsInRole(string role)
    {
        return httpContextAccessor.HttpContext?.User?.IsInRole(role) == true;
    }
}
