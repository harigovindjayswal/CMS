using Application.Core;
using Application.Interfaces;
using Application.LawyerAdmin.DTO;
using Domain.AppEntities;
using MediatR;
using Persistence.Context;

namespace Application.LawyerAdmin.Commands;

public class CreateManagedLawyer
{
    public class Command : IRequest<Result<int>>
    {
        public required CreateManagedLawyerDto Lawyer { get; set; }
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
                request.Lawyer.Email.Trim(),
                request.Lawyer.DisplayName.Trim(),
                request.Lawyer.Password,
                "Lawyer",
                cancellationToken);

            if (!created.IsSuccess)
                return Result<int>.Failure(created.Error ?? "Failed to create user", created.Code);

            var userId = created.Value!;

            try
            {
                var entity = new Lawyer
                {
                    UserId = userId,
                    RegisteredByUserId = adminUserId,
                    EmailId = request.Lawyer.Email.Trim(),
                    FirstName = request.Lawyer.FirstName?.Trim(),
                    MiddleName = request.Lawyer.MiddleName?.Trim(),
                    LastName = request.Lawyer.LastName?.Trim(),
                    MobileNo = request.Lawyer.MobileNo?.Trim(),
                    StateId = request.Lawyer.StateId,
                    CityId = request.Lawyer.CityId,
                    BarLicenseNumber = request.Lawyer.BarLicenseNumber?.Trim(),
                    IsActive = true,
                    CreatedBy = adminUserId,
                    CreatedDate = DateTime.Now
                };

                context.Lawyers.Add(entity);
                var saved = await context.SaveChangesAsync(cancellationToken);
                if (saved == 0)
                    return Result<int>.Failure("Failed to create lawyer record", 400);

                return Result<int>.Success(entity.LawyerId);
            }
            catch
            {
                await identityService.DeleteUserAsync(userId, cancellationToken);
                throw;
            }
        }
    }
}
