using Application.Cases.DTO;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.Cases.Queries;

public class GetAssignedCases
{
    public class Query : IRequest<Result<List<CaseDto>>> { }

    public class Handler(CmsContext context, IUserAccessor userAccessor) : IRequestHandler<Query, Result<List<CaseDto>>>
    {
        public async Task<Result<List<CaseDto>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var userId = userAccessor.GetUserId();
            if (string.IsNullOrWhiteSpace(userId))
                return Result<List<CaseDto>>.Failure("Unauthorised", 401);

            var items = await context.Cases
                .AsNoTracking()
                .Where(x => x.AssignedTo == userId)
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => new CaseDto
                {
                    CaseId = x.CaseId,
                    ClientId = x.ClientId,
                    ClientName = context.Clients.Where(c => c.ClientId == x.ClientId).Select(c => ((c.FirstName ?? "") + " " + (c.LastName ?? "")).Trim()).FirstOrDefault(),
                    LawyerRequestId = x.LawyerRequestId,
                    Title = x.Title,
                    Description = x.Description,
                    CaseType = x.CaseType,
                    CourtName = x.CourtName,
                    CaseNumber = x.CaseNumber,
                    Purpose = x.Purpose,
                    FilingDate = x.FilingDate,
                    Status = x.Status,
                    Stage = x.Stage,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt
                })
                .ToListAsync(cancellationToken);

            return Result<List<CaseDto>>.Success(items);
        }
    }
}

