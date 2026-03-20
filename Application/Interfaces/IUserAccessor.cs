namespace Application.Interfaces;

public interface IUserAccessor
{
    string? GetUserId();
    string? GetUserRole();
    bool IsInRole(string role);
}
