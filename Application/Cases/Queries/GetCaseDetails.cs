using Application.Cases.DTO;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.Cases.Queries;

public class GetCaseDetails
{
    public class Query : IRequest<Result<CaseDetailsDto>>
    {
        public required int Id { get; set; }
    }

    public class Handler(CmsContext context, IUserAccessor userAccessor) : IRequestHandler<Query, Result<CaseDetailsDto>>
    {
        public async Task<Result<CaseDetailsDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var userId = userAccessor.GetUserId();
            if (string.IsNullOrWhiteSpace(userId))
                return Result<CaseDetailsDto>.Failure("Unauthorised", 401);

            var entity = await context.Cases
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.CaseId == request.Id, cancellationToken);

            if (entity == null) return Result<CaseDetailsDto>.Failure("Case not found", 404);

            var isAssignedLawyer = string.Equals(entity.AssignedTo, userId, StringComparison.OrdinalIgnoreCase);
            var isClient = await context.Clients.AsNoTracking().AnyAsync(c => c.ClientId == entity.ClientId && c.UserId == userId, cancellationToken);
            var isManagingLawyerAdmin = false;
            if (!isAssignedLawyer && !isClient && userAccessor.IsInRole("LawyerAdmin"))
            {
                isManagingLawyerAdmin = await context.Lawyers
                    .AsNoTracking()
                    .AnyAsync(l => l.UserId == entity.AssignedTo && l.RegisteredByUserId == userId, cancellationToken);
            }

            if (!isAssignedLawyer && !isClient && !isManagingLawyerAdmin)
                return Result<CaseDetailsDto>.Failure("Forbidden", 403);

            var notesQuery = context.Notes.AsNoTracking().Where(n => n.CaseId == entity.CaseId);
            if (!isAssignedLawyer && !isManagingLawyerAdmin)
                notesQuery = notesQuery.Where(n => n.IsPrivate != true);

            var notes = await notesQuery
                .OrderByDescending(n => n.CreatedAt)
                .Select(n => new NoteDto
                {
                    NoteId = n.NoteId,
                    UserId = n.UserId,
                    Content = n.Content,
                    IsPrivate = n.IsPrivate,
                    CreatedAt = n.CreatedAt
                })
                .ToListAsync(cancellationToken);

            var documents = await context.Documents
                .AsNoTracking()
                .Where(d => d.CaseId == entity.CaseId)
                .OrderByDescending(d => d.UploadedAt)
                .Select(d => new DocumentDto
                {
                    DocumentId = d.DocumentId,
                    Title = d.Title,
                    Category = d.Category,
                    UploadedAt = d.UploadedAt,
                    UploadedBy = d.UploadedBy
                })
                .ToListAsync(cancellationToken);

            var dto = new CaseDetailsDto
            {
                CaseId = entity.CaseId,
                ClientId = entity.ClientId,
                ClientName = await context.Clients.AsNoTracking()
                    .Where(c => c.ClientId == entity.ClientId)
                    .Select(c => ((c.FirstName ?? "") + " " + (c.LastName ?? "")).Trim())
                    .FirstOrDefaultAsync(cancellationToken),
                LawyerRequestId = entity.LawyerRequestId,
                Title = entity.Title,
                Description = entity.Description,
                CaseType = entity.CaseType,
                CourtName = entity.CourtName,
                CaseNumber = entity.CaseNumber,
                Purpose = entity.Purpose,
                FilingDate = entity.FilingDate,
                Status = entity.Status,
                Stage = entity.Stage,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                Notes = notes,
                Documents = documents
            };

            return Result<CaseDetailsDto>.Success(dto);
        }
    }
}
