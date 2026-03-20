using Application.Interfaces;
using Application.LawyerRequests.DTO;
using Domain.AppEntities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.LawyerRequests.Queries;

public class GetIncomingLawyerRequests
{
    public class Query : IRequest<Result<List<LawyerRequestDto>>> { }

    public class Handler(CmsContext context, IUserAccessor userAccessor) : IRequestHandler<Query, Result<List<LawyerRequestDto>>>
    {
        public async Task<Result<List<LawyerRequestDto>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var userId = userAccessor.GetUserId();
            if (string.IsNullOrWhiteSpace(userId))
                return Result<List<LawyerRequestDto>>.Failure("Unauthorised", 401);

            var lawyer = await context.Lawyers
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserId == userId && x.IsActive, cancellationToken);

            if (lawyer == null)
                return Result<List<LawyerRequestDto>>.Failure("Lawyer profile not found", 400);

            var items = await context.LawyerRequests
                .AsNoTracking()
                .Where(x => x.LawyerId == lawyer.LawyerId)
                .OrderByDescending(x => x.CreatedDate)
                .Select(x => new LawyerRequestDto
                {
                    LawyerRequestId = x.LawyerRequestId,
                    ClientId = x.ClientId,
                    ClientName = context.Clients.Where(c => c.ClientId == x.ClientId).Select(c => ((c.FirstName ?? "") + " " + (c.LastName ?? "")).Trim()).FirstOrDefault(),
                    LawyerId = x.LawyerId,
                    LawyerName = ((lawyer.FirstName ?? "") + " " + (lawyer.LastName ?? "")).Trim(),
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

            // Mark newly seen requests as Read (without affecting Accepted/Rejected)
            await context.LawyerRequests
                .Where(x => x.LawyerId == lawyer.LawyerId && x.Status == LawyerRequestStatus.Pending)
                .ExecuteUpdateAsync(setters => setters
                        .SetProperty(x => x.Status, LawyerRequestStatus.Read)
                        .SetProperty(x => x.UpdatedBy, userId)
                        .SetProperty(x => x.UpdatedDate, DateTime.Now),
                    cancellationToken);

            return Result<List<LawyerRequestDto>>.Success(items);
        }
    }
}
