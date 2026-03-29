using Application.Core;
using Application.Interfaces;
using Application.LawyerAdmin.DTO;
using Domain.AppEntities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.LawyerAdmin.Commands;

public class CreateManagedStaff
{
    public class Command : IRequest<Result<int>>
    {
        public required CreateManagedStaffDto Staff { get; set; }
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

            var managesLawyer = await context.Lawyers
                .AsNoTracking()
                .AnyAsync(l => l.LawyerId == request.Staff.LawyerId && l.RegisteredByUserId == adminUserId, cancellationToken);

            if (!managesLawyer)
                return Result<int>.Failure("Invalid lawyer selection", 400);

            var created = await identityService.CreateUserWithRoleAsync(
                request.Staff.Email.Trim(),
                request.Staff.DisplayName.Trim(),
                request.Staff.Password,
                "Staff",
                cancellationToken);

            if (!created.IsSuccess)
                return Result<int>.Failure(created.Error ?? "Failed to create user", created.Code);

            var userId = created.Value!;

            try
            {
                var entity = new Staff
                {
                    UserId = userId,
                    LawyerId = request.Staff.LawyerId,
                    RegisteredByUserId = adminUserId,
                    EmailId = request.Staff.Email.Trim(),
                    FirstName = request.Staff.FirstName?.Trim(),
                    MiddleName = request.Staff.MiddleName?.Trim(),
                    LastName = request.Staff.LastName?.Trim(),
                    MobileNo = request.Staff.MobileNo?.Trim(),
                    IsActive = true,
                    CreatedBy = adminUserId,
                    CreatedDate = DateTime.Now
                };

                context.Staffs.Add(entity);
                var saved = await context.SaveChangesAsync(cancellationToken);
                if (saved == 0)
                    return Result<int>.Failure("Failed to create staff record", 400);

                return Result<int>.Success(entity.StaffId);
            }
            catch
            {
                await identityService.DeleteUserAsync(userId, cancellationToken);
                throw;
            }
        }
    }
}

