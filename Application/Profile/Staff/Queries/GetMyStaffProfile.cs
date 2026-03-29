using Application.Interfaces;
using Application.Profile.Staff.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.Profile.Staff.Queries;

public class GetMyStaffProfile
{
    public class Query : IRequest<Result<StaffProfileDto>> { }

    public class Handler(CmsContext context, IUserAccessor userAccessor) : IRequestHandler<Query, Result<StaffProfileDto>>
    {
        public async Task<Result<StaffProfileDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var userId = userAccessor.GetUserId();
            if (string.IsNullOrWhiteSpace(userId))
                return Result<StaffProfileDto>.Failure("Unauthorised", 401);

            if (!userAccessor.IsInRole("Staff"))
                return Result<StaffProfileDto>.Failure("Forbidden", 403);

            var entity = await context.Staffs
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserId == userId && x.IsActive, cancellationToken);

            if (entity == null)
                return Result<StaffProfileDto>.Failure("Staff profile not found", 404);

            return Result<StaffProfileDto>.Success(new StaffProfileDto
            {
                FirstName = entity.FirstName,
                MiddleName = entity.MiddleName,
                LastName = entity.LastName,
                DateOfBirth = entity.DateOfBirth,
                EmailId = entity.EmailId,
                MobileNo = entity.MobileNo,
                Address = entity.Address,
                LawyerId = entity.LawyerId
            });
        }
    }
}

