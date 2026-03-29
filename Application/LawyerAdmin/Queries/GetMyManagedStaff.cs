using Application.Core;
using Application.Interfaces;
using Application.LawyerAdmin.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.LawyerAdmin.Queries;

public class GetMyManagedStaff
{
    public class Query : IRequest<Result<List<ManagedStaffDto>>> { }

    public class Handler(CmsContext context, IUserAccessor userAccessor) : IRequestHandler<Query, Result<List<ManagedStaffDto>>>
    {
        public async Task<Result<List<ManagedStaffDto>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var adminUserId = userAccessor.GetUserId();
            if (string.IsNullOrWhiteSpace(adminUserId))
                return Result<List<ManagedStaffDto>>.Failure("Unauthorised", 401);

            if (!userAccessor.IsInRole("LawyerAdmin"))
                return Result<List<ManagedStaffDto>>.Failure("Forbidden", 403);

            var items = await context.Staffs
                .AsNoTracking()
                .Where(x => x.RegisteredByUserId == adminUserId)
                .OrderByDescending(x => x.CreatedDate)
                .Select(x => new ManagedStaffDto
                {
                    StaffId = x.StaffId,
                    UserId = x.UserId,
                    EmailId = x.EmailId,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    MobileNo = x.MobileNo,
                    LawyerId = x.LawyerId,
                    IsActive = x.IsActive,
                    CreatedDate = x.CreatedDate
                })
                .ToListAsync(cancellationToken);

            return Result<List<ManagedStaffDto>>.Success(items);
        }
    }
}

