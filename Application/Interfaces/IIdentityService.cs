using Application.Core;
using MediatR;

namespace Application.Interfaces;

public interface IIdentityService
{
    Task<Result<string>> CreateUserWithRoleAsync(
        string email,
        string displayName,
        string password,
        string role,
        CancellationToken cancellationToken);

    Task<Result<Unit>> DeleteUserAsync(string userId, CancellationToken cancellationToken);
}
