using Application.Interfaces;
using Application.Profile.Staff.DTO;
using Domain.AppEntities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.Profile.Staff.Commands;

public class UpsertMyStaffProfile
{
    public class Command : IRequest<Result<Unit>>
    {
        public required StaffProfileDto Profile { get; set; }
    }

    public class Handler(CmsContext context, IUserAccessor userAccessor) : IRequestHandler<Command, Result<Unit>>
    {
        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var userId = userAccessor.GetUserId();
            if (string.IsNullOrWhiteSpace(userId))
                return Result<Unit>.Failure("Unauthorised", 401);

            if (!userAccessor.IsInRole("Staff"))
                return Result<Unit>.Failure("Forbidden", 403);

            var entity = await context.Staffs
                .FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);

            if (entity == null)
            {
                return Result<Unit>.Failure("Staff profile not found", 404);
            }

            entity.FirstName = request.Profile.FirstName?.Trim();
            entity.MiddleName = request.Profile.MiddleName?.Trim();
            entity.LastName = request.Profile.LastName?.Trim();
            entity.DateOfBirth = request.Profile.DateOfBirth;
            entity.EmailId = request.Profile.EmailId?.Trim();
            entity.MobileNo = request.Profile.MobileNo?.Trim();
            entity.Address = request.Profile.Address?.Trim();

            // LawyerId is assigned by LawyerAdmin; staff cannot change it.
            entity.UpdatedBy = userId;
            entity.UpdatedDate = DateTime.Now;

            var saved = await context.SaveChangesAsync(cancellationToken);
            if (saved == 0) return Result<Unit>.Failure("Failed to save profile", 400);

            return Result<Unit>.Success(Unit.Value);
        }
    }
}

