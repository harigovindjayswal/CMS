using Application.Interfaces;
using Domain.AppEntities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.Cases.Commands;

public class AddCaseDocument
{
    public class Command : IRequest<Result<int>>
    {
        public required int CaseId { get; set; }
        public string? Title { get; set; }
        public string? Category { get; set; }
        public required string FilePath { get; set; }
    }

    public class Handler(CmsContext context, IUserAccessor userAccessor) : IRequestHandler<Command, Result<int>>
    {
        public async Task<Result<int>> Handle(Command request, CancellationToken cancellationToken)
        {
            var userId = userAccessor.GetUserId();
            if (string.IsNullOrWhiteSpace(userId))
                return Result<int>.Failure("Unauthorised", 401);

            var caseEntity = await context.Cases
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.CaseId == request.CaseId, cancellationToken);

            if (caseEntity == null) return Result<int>.Failure("Case not found", 404);

            var isAssignedLawyer = string.Equals(caseEntity.AssignedTo, userId, StringComparison.OrdinalIgnoreCase);
            var isStaffForAssignedLawyer = false;
            if (!isAssignedLawyer && userAccessor.IsInRole("Staff"))
            {
                var assignedLawyerId = await context.Lawyers
                    .AsNoTracking()
                    .Where(l => l.UserId == caseEntity.AssignedTo)
                    .Select(l => l.LawyerId)
                    .FirstOrDefaultAsync(cancellationToken);

                if (assignedLawyerId != 0)
                {
                    isStaffForAssignedLawyer = await context.Staffs
                        .AsNoTracking()
                        .AnyAsync(s => s.UserId == userId && s.LawyerId == assignedLawyerId && s.IsActive, cancellationToken);
                }
            }

            if (!isAssignedLawyer && !isStaffForAssignedLawyer)
                return Result<int>.Failure("Forbidden", 403);

            var entity = new Document
            {
                CaseId = request.CaseId,
                Title = request.Title?.Trim(),
                Category = request.Category?.Trim(),
                FilePath = request.FilePath,
                UploadedBy = userId,
                UploadedAt = DateTime.Now,
                Version = 1
            };

            context.Documents.Add(entity);
            var saved = await context.SaveChangesAsync(cancellationToken);
            if (saved == 0) return Result<int>.Failure("Failed to save document", 400);

            return Result<int>.Success(entity.DocumentId);
        }
    }
}
