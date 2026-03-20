using Application.Core;
using Application.Interfaces;
using Application.LawyerAdmin.DTO;
using Domain.AppEntities;
using MediatR;
using Persistence.Context;

namespace Application.LawyerAdmin.Commands;

public class CreateManagedClient
{
    public class Command : IRequest<Result<int>>
    {
        public required CreateManagedClientDto Client { get; set; }
    }

    public class Handler(
        CmsContext context,
        IUserAccessor userAccessor,
        IIdentityService identityService) : IRequestHandler<Command, Result<int>>
    {
        public async Task<Result<int>> Handle(Command request, CancellationToken cancellationToken)
        {
            var adminUserId = userAccessor.GetUserId();
            if (string.IsNullOrWhiteSpace(adminUserId))
                return Result<int>.Failure("Unauthorised", 401);

            if (!userAccessor.IsInRole("LawyerAdmin"))
                return Result<int>.Failure("Forbidden", 403);

            var created = await identityService.CreateUserWithRoleAsync(
                request.Client.Email.Trim(),
                request.Client.DisplayName.Trim(),
                request.Client.Password,
                "Client",
                cancellationToken);

            if (!created.IsSuccess)
                return Result<int>.Failure(created.Error ?? "Failed to create user", created.Code);

            var userId = created.Value!;

            try
            {
                var entity = new Client
                {
                    UserId = userId,
                    RegisteredByUserId = adminUserId,
                    EmailId = request.Client.Email.Trim(),
                    FirstName = request.Client.FirstName?.Trim(),
                    MiddleName = request.Client.MiddleName?.Trim(),
                    LastName = request.Client.LastName?.Trim(),
                    MobileNo = request.Client.MobileNo?.Trim(),
                    IsActive = true,
                    CreatedBy = adminUserId,
                    CreatedDate = DateTime.Now
                };

                context.Clients.Add(entity);
                var saved = await context.SaveChangesAsync(cancellationToken);
                if (saved == 0)
                    return Result<int>.Failure("Failed to create client record", 400);

                return Result<int>.Success(entity.ClientId);
            }
            catch
            {
                await identityService.DeleteUserAsync(userId, cancellationToken);
                throw;
            }
        }
    }
}
