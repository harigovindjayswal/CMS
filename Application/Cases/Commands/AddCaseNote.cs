using Application.Cases.DTO;
using Application.Interfaces;
using Domain.AppEntities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.Cases.Commands;

public class AddCaseNote
{
    public class Command : IRequest<Result<int>>
    {
        public required AddNoteDto Note { get; set; }
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
                .FirstOrDefaultAsync(x => x.CaseId == request.Note.CaseId, cancellationToken);

            if (caseEntity == null) return Result<int>.Failure("Case not found", 404);

            var isAssignedLawyer = string.Equals(caseEntity.AssignedTo, userId, StringComparison.OrdinalIgnoreCase);
            var isClient = await context.Clients.AsNoTracking().AnyAsync(c => c.ClientId == caseEntity.ClientId && c.UserId == userId, cancellationToken);
            var isStaffForAssignedLawyer = false;
            if (!isAssignedLawyer && !isClient && userAccessor.IsInRole("Staff"))
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

            if (!isAssignedLawyer && !isClient && !isStaffForAssignedLawyer)
                return Result<int>.Failure("Forbidden", 403);

            var isPrivate = isAssignedLawyer ? request.Note.IsPrivate : false;

            var entity = new Note
            {
                CaseId = caseEntity.CaseId,
                UserId = userId,
                Content = request.Note.Content.Trim(),
                IsPrivate = isPrivate,
                CreatedAt = DateTime.Now
            };

            context.Notes.Add(entity);
            var saved = await context.SaveChangesAsync(cancellationToken);
            if (saved == 0) return Result<int>.Failure("Failed to add note", 400);

            return Result<int>.Success(entity.NoteId);
        }
    }
}
