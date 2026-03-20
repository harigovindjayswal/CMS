using Application.Interfaces;
using Application.LawyerRequests.DTO;
using Domain.AppEntities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.LawyerRequests.Queries;

public class GetMyLawyerRequests
{
    public class Query : IRequest<Result<List<LawyerRequestDto>>> { }

    public class Handler(CmsContext context, IUserAccessor userAccessor) : IRequestHandler<Query, Result<List<LawyerRequestDto>>>
    {
        public async Task<Result<List<LawyerRequestDto>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var userId = userAccessor.GetUserId();
            if (string.IsNullOrWhiteSpace(userId))
                return Result<List<LawyerRequestDto>>.Failure("Unauthorised", 401);

            var client = await context.Clients
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);

            if (client == null)
                return Result<List<LawyerRequestDto>>.Failure("Client profile not found", 400);

            var items = await context.LawyerRequests
                .AsNoTracking()
                .Where(x => x.ClientId == client.ClientId)
                .OrderByDescending(x => x.CreatedDate)
                .Select(x => new LawyerRequestDto
                {
                    LawyerRequestId = x.LawyerRequestId,
                    ClientId = x.ClientId,
                    ClientName = (client.FirstName ?? "") + " " + (client.LastName ?? ""),
                    LawyerId = x.LawyerId,
                    LawyerName = context.Lawyers.Where(l => l.LawyerId == x.LawyerId).Select(l => ((l.FirstName ?? "") + " " + (l.LastName ?? "")).Trim()).FirstOrDefault(),
                    CaseTypeId = x.CaseTypeId,
                    CaseTypeName = context.CaseTypes.Where(ct => ct.CaseTypeId == x.CaseTypeId).Select(ct => ct.TypeName).FirstOrDefault(),
                    StateId = x.StateId,
                    DistrictId = x.DistrictId,
                    CityId = x.CityId,
                    CaseDescription = x.CaseDescription,
                    Status = x.Status,
                    LawyerRemark = x.LawyerRemark
                })
                .ToListAsync(cancellationToken);

            // Normalise whitespace for display name
            foreach (var item in items)
                item.ClientName = (item.ClientName ?? "").Trim();

            return Result<List<LawyerRequestDto>>.Success(items);
        }
    }
}

