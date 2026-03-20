using Application.Core;
using Application.Cases.DTO;
using Application.Interfaces;
using Domain.AppEntities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.LawyerAdmin.Commands;

public class CreateCaseForRequest
{
    public class Command : IRequest<Result<int>>
    {
        public required CreateCaseDto Case { get; set; }
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

            var lawyerRequest = await context.LawyerRequests
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.LawyerRequestId == request.Case.LawyerRequestId, cancellationToken);

            if (lawyerRequest == null)
                return Result<int>.Failure("Lawyer request not found", 404);

            if (lawyerRequest.Status != LawyerRequestStatus.Accepted)
                return Result<int>.Failure("Case can only be created after request acceptance", 400);

            var lawyer = await context.Lawyers
                .AsNoTracking()
                .FirstOrDefaultAsync(x =>
                    x.LawyerId == lawyerRequest.LawyerId &&
                    x.RegisteredByUserId == adminUserId &&
                    x.IsActive, cancellationToken);

            if (lawyer == null)
                return Result<int>.Failure("Forbidden", 403);

            var alreadyCreated = await context.Cases
                .AsNoTracking()
                .AnyAsync(x => x.LawyerRequestId == lawyerRequest.LawyerRequestId, cancellationToken);

            if (alreadyCreated)
                return Result<int>.Failure("Case already created for this request", 400);

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
                LawyerRequestId = lawyerRequest.LawyerRequestId,
                ClientId = lawyerRequest.ClientId,
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

