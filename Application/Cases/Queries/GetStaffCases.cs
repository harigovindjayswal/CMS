using Application.Cases.DTO;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.Cases.Queries;

public class GetStaffCases
{
    public class Query : IRequest<Result<List<CaseDto>>> { }

    public class Handler(CmsContext context, IUserAccessor userAccessor) : IRequestHandler<Query, Result<List<CaseDto>>>
    {
        public async Task<Result<List<CaseDto>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var userId = userAccessor.GetUserId();
            if (string.IsNullOrWhiteSpace(userId))
                return Result<List<CaseDto>>.Failure("Unauthorised", 401);

            if (!userAccessor.IsInRole("Staff"))
                return Result<List<CaseDto>>.Failure("Forbidden", 403);

            var staff = await context.Staffs
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.UserId == userId && s.IsActive, cancellationToken);

            if (staff == null)
                return Result<List<CaseDto>>.Failure("Staff profile not found", 404);

            var lawyerUserId = await context.Lawyers
                .AsNoTracking()
                .Where(l => l.LawyerId == staff.LawyerId && l.IsActive)
                .Select(l => l.UserId)
                .FirstOrDefaultAsync(cancellationToken);

            if (string.IsNullOrWhiteSpace(lawyerUserId))
                return Result<List<CaseDto>>.Failure("Assigned lawyer not found", 400);

            var items = await context.Cases
                .AsNoTracking()
                .Where(c => c.AssignedTo == lawyerUserId)
                .OrderByDescending(c => c.CreatedAt)
                .Select(c => new CaseDto
                {
                    CaseId = c.CaseId,
                    ClientId = c.ClientId,
                    ClientName = context.Clients
                        .Where(cl => cl.ClientId == c.ClientId)
                        .Select(cl => ((cl.FirstName ?? "") + " " + (cl.LastName ?? "")).Trim())
                        .FirstOrDefault(),
                    LawyerRequestId = c.LawyerRequestId,
                    Title = c.Title,
                    Description = c.Description,
                    CaseType = c.CaseType,
                    CourtName = c.CourtName,
                    CaseNumber = c.CaseNumber,
                    Purpose = c.Purpose,
                    FilingDate = c.FilingDate,
                    Status = c.Status,
                    Stage = c.Stage,
                    CreatedAt = c.CreatedAt,
                    UpdatedAt = c.UpdatedAt
                })
                .ToListAsync(cancellationToken);

            return Result<List<CaseDto>>.Success(items);
        }
    }
}

