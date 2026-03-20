using Application.Core;
using Application.Interfaces;
using Application.LawyerAdmin.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.LawyerAdmin.Queries;

public class GetMyManagedClients
{
    public class Query : IRequest<Result<List<ManagedClientDto>>> { }

    public class Handler(CmsContext context, IUserAccessor userAccessor) : IRequestHandler<Query, Result<List<ManagedClientDto>>>
    {
        public async Task<Result<List<ManagedClientDto>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var adminUserId = userAccessor.GetUserId();
            if (string.IsNullOrWhiteSpace(adminUserId))
                return Result<List<ManagedClientDto>>.Failure("Unauthorised", 401);

            if (!userAccessor.IsInRole("LawyerAdmin"))
                return Result<List<ManagedClientDto>>.Failure("Forbidden", 403);

            var items = await context.Clients
                .AsNoTracking()
                .Where(x => x.RegisteredByUserId == adminUserId)
                .OrderByDescending(x => x.CreatedDate)
                .Select(x => new ManagedClientDto
                {
                    ClientId = x.ClientId,
                    UserId = x.UserId,
                    EmailId = x.EmailId,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    MobileNo = x.MobileNo,
                    IsActive = x.IsActive,
                    CreatedDate = x.CreatedDate
                })
                .ToListAsync(cancellationToken);

            return Result<List<ManagedClientDto>>.Success(items);
        }
    }
}

