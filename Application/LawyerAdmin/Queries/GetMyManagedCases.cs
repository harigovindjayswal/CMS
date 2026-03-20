using Application.Core;
using Application.Interfaces;
using Application.LawyerAdmin.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.LawyerAdmin.Queries;

public class GetMyManagedCases
{
    public class Query : IRequest<Result<List<ManagedCaseDto>>> { }

    public class Handler(CmsContext context, IUserAccessor userAccessor) : IRequestHandler<Query, Result<List<ManagedCaseDto>>>
    {
        public async Task<Result<List<ManagedCaseDto>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var adminUserId = userAccessor.GetUserId();
            if (string.IsNullOrWhiteSpace(adminUserId))
                return Result<List<ManagedCaseDto>>.Failure("Unauthorised", 401);

            if (!userAccessor.IsInRole("LawyerAdmin"))
                return Result<List<ManagedCaseDto>>.Failure("Forbidden", 403);

            var items = await context.Cases
                .AsNoTracking()
                .Where(c => context.Lawyers.Any(l => l.UserId == c.AssignedTo && l.RegisteredByUserId == adminUserId))
                .OrderByDescending(c => c.CreatedAt)
                .Select(c => new ManagedCaseDto
                {
                    CaseId = c.CaseId,
                    ClientId = c.ClientId,
                    ClientName = context.Clients.Where(cl => cl.ClientId == c.ClientId)
                        .Select(cl => ((cl.FirstName ?? "") + " " + (cl.LastName ?? "")).Trim())
                        .FirstOrDefault(),
                    AssignedLawyerUserId = c.AssignedTo,
                    AssignedLawyerName = context.Lawyers.Where(l => l.UserId == c.AssignedTo)
                        .Select(l => ((l.FirstName ?? "") + " " + (l.LastName ?? "")).Trim())
                        .FirstOrDefault(),
                    Title = c.Title,
                    CaseType = c.CaseType,
                    CourtName = c.CourtName,
                    CaseNumber = c.CaseNumber,
                    Status = c.Status,
                    Stage = c.Stage,
                    CreatedAt = c.CreatedAt,
                    UpdatedAt = c.UpdatedAt
                })
                .ToListAsync(cancellationToken);

            return Result<List<ManagedCaseDto>>.Success(items);
        }
    }
}

