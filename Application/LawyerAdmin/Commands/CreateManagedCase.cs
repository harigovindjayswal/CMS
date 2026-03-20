using Application.Core;
using Application.Interfaces;
using Application.LawyerAdmin.DTO;
using Domain.AppEntities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.LawyerAdmin.Commands;

public class CreateManagedCase
{
    public class Command : IRequest<Result<int>>
    {
        public required CreateManagedCaseDto Case { get; set; }
    }

    public class Handler(CmsContext context, IUserAccessor userAccessor) : IRequestHandler<Command, Result<int>>
    {
        public async Task<Result<int>> Handle(Command request, CancellationToken cancellationToken)
        {
            var adminUserId = userAccessor.GetUserId();
            if (string.IsNullOrWhiteSpace(adminUserId))
                return Result<int>.Failure("Unauthorised", 401);

            if (!userAccessor.IsInRole("LawyerAdmin"))
                return Result<int>.Failure("Forbidden", 403);

            var client = await context.Clients
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.ClientId == request.Case.ClientId && x.RegisteredByUserId == adminUserId, cancellationToken);

            if (client == null)
                return Result<int>.Failure("Client not found", 404);

            var lawyer = await context.Lawyers
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.LawyerId == request.Case.LawyerId && x.RegisteredByUserId == adminUserId && x.IsActive, cancellationToken);

            if (lawyer == null)
                return Result<int>.Failure("Lawyer not found", 404);

            var caseType = await context.CaseTypes
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.CaseTypeId == request.Case.CaseTypeId && x.IsActive, cancellationToken);

            if (caseType == null)
                return Result<int>.Failure("Invalid case type selected", 400);

            string courtName;
            if (request.Case.CourtId.HasValue && request.Case.CourtId.Value > 0)
            {
                var court = await context.Courts
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.CourtId == request.Case.CourtId.Value && x.IsActive, cancellationToken);

                if (court == null)
                    return Result<int>.Failure("Invalid court selected", 400);

                courtName = court.Name;
            }
            else
            {
                courtName = request.Case.CourtName?.Trim() ?? "";
                if (string.IsNullOrWhiteSpace(courtName))
                    return Result<int>.Failure("Court name is required", 400);
            }

            var entity = new Case
            {
                LawyerRequestId = null,
                ClientId = client.ClientId,
                Title = request.Case.Title.Trim(),
                Description = request.Case.Description?.Trim(),
                CaseType = caseType.TypeName,
                CourtName = courtName,
                CaseNumber = request.Case.CaseNumber?.Trim(),
                Purpose = request.Case.Purpose?.Trim(),
                FilingDate = request.Case.FilingDate,
                AssignedTo = lawyer.UserId,
                Status = "Open",
                Stage = "Filed",
                CreatedAt = DateTime.Now
            };

            context.Cases.Add(entity);
            var saved = await context.SaveChangesAsync(cancellationToken);
            if (saved == 0) return Result<int>.Failure("Failed to create case", 400);

            return Result<int>.Success(entity.CaseId);
        }
    }
}

