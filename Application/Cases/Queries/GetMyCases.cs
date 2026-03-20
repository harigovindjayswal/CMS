using Application.Cases.DTO;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.Cases.Queries;

public class GetMyCases
{
    public class Query : IRequest<Result<List<CaseDto>>> { }

    public class Handler(CmsContext context, IUserAccessor userAccessor) : IRequestHandler<Query, Result<List<CaseDto>>>
    {
        public async Task<Result<List<CaseDto>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var userId = userAccessor.GetUserId();
            if (string.IsNullOrWhiteSpace(userId))
                return Result<List<CaseDto>>.Failure("Unauthorised", 401);

            var client = await context.Clients
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);

            if (client == null)
                return Result<List<CaseDto>>.Failure("Client profile not found", 400);

            var items = await context.Cases
                .AsNoTracking()
                .Where(x => x.ClientId == client.ClientId)
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => new CaseDto
                {
                    CaseId = x.CaseId,
                    ClientId = x.ClientId,
                    ClientName = ((client.FirstName ?? "") + " " + (client.LastName ?? "")).Trim(),
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

