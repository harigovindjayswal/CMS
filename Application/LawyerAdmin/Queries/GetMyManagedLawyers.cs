using Application.Core;
using Application.Interfaces;
using Application.LawyerAdmin.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.LawyerAdmin.Queries;

public class GetMyManagedLawyers
{
    public class Query : IRequest<Result<List<ManagedLawyerDto>>> { }

    public class Handler(CmsContext context, IUserAccessor userAccessor) : IRequestHandler<Query, Result<List<ManagedLawyerDto>>>
    {
        public async Task<Result<List<ManagedLawyerDto>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var adminUserId = userAccessor.GetUserId();
            if (string.IsNullOrWhiteSpace(adminUserId))
                return Result<List<ManagedLawyerDto>>.Failure("Unauthorised", 401);

            if (!userAccessor.IsInRole("LawyerAdmin"))
                return Result<List<ManagedLawyerDto>>.Failure("Forbidden", 403);

            var items = await context.Lawyers
                .AsNoTracking()
                .Where(x => x.RegisteredByUserId == adminUserId)
                .OrderByDescending(x => x.CreatedDate)
                .Select(x => new ManagedLawyerDto
                {
                    LawyerId = x.LawyerId,
                    UserId = x.UserId,
                    EmailId = x.EmailId,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    MobileNo = x.MobileNo,
                    StateId = x.StateId,
                    CityId = x.CityId,
                    IsActive = x.IsActive,
                    CreatedDate = x.CreatedDate
                })
                .ToListAsync(cancellationToken);

            return Result<List<ManagedLawyerDto>>.Success(items);
        }
    }
}

