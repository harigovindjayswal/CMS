using Application.Core;
using Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Persistence.Identity;

namespace Infrastructure.Security;

public class IdentityService(
    UserManager<User> userManager,
    RoleManager<IdentityRole> roleManager) : IIdentityService
{
    public async Task<Result<string>> CreateUserWithRoleAsync(
        string email,
        string displayName,
        string password,
        string role,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(email))
            return Result<string>.Failure("Email is required", 400);

        if (string.IsNullOrWhiteSpace(displayName))
            return Result<string>.Failure("Display name is required", 400);

        if (string.IsNullOrWhiteSpace(password))
            return Result<string>.Failure("Password is required", 400);

        if (string.IsNullOrWhiteSpace(role))
            return Result<string>.Failure("Role is required", 400);

        var roleExists = await roleManager.RoleExistsAsync(role);
        if (!roleExists)
            return Result<string>.Failure("Invalid role selected", 400);

        var existing = await userManager.FindByEmailAsync(email);
        if (existing != null)
            return Result<string>.Failure("Email is already registered", 400);

        var user = new User
        {
            UserName = email,
            Email = email,
            DisplayName = displayName
        };

        var create = await userManager.CreateAsync(user, password);
        if (!create.Succeeded)
        {
            var message = create.Errors.FirstOrDefault()?.Description ?? "Failed to create user";
            return Result<string>.Failure(message, 400);
        }

        var addRole = await userManager.AddToRoleAsync(user, role);
        if (!addRole.Succeeded)
        {
            await userManager.DeleteAsync(user);
            var message = addRole.Errors.FirstOrDefault()?.Description ?? "Failed to assign role";
            return Result<string>.Failure(message, 400);
        }

        return Result<string>.Success(user.Id);
    }

    public async Task<Result<Unit>> DeleteUserAsync(string userId, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(userId))
            return Result<Unit>.Failure("User id is required", 400);

        var user = await userManager.FindByIdAsync(userId);
        if (user == null) return Result<Unit>.Success(Unit.Value);

        var result = await userManager.DeleteAsync(user);
        if (!result.Succeeded)
        {
            var message = result.Errors.FirstOrDefault()?.Description ?? "Failed to delete user";
            return Result<Unit>.Failure(message, 400);
        }

        return Result<Unit>.Success(Unit.Value);
    }
}
