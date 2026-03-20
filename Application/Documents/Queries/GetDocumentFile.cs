using System.IO;
using Application.Documents.DTO;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.Documents.Queries;

public class GetDocumentFile
{
    public class Query : IRequest<Result<DocumentFileDto>>
    {
        public required int DocumentId { get; set; }
    }

    public class Handler(CmsContext context, IUserAccessor userAccessor) : IRequestHandler<Query, Result<DocumentFileDto>>
    {
        public async Task<Result<DocumentFileDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var userId = userAccessor.GetUserId();
            if (string.IsNullOrWhiteSpace(userId))
                return Result<DocumentFileDto>.Failure("Unauthorised", 401);

            var document = await context.Documents
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.DocumentId == request.DocumentId, cancellationToken);

            if (document == null)
                return Result<DocumentFileDto>.Failure("Document not found", 404);

            var caseEntity = await context.Cases
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.CaseId == document.CaseId, cancellationToken);

            if (caseEntity == null)
                return Result<DocumentFileDto>.Failure("Case not found", 404);

            var isAssignedLawyer = string.Equals(caseEntity.AssignedTo, userId, StringComparison.OrdinalIgnoreCase);
            var isClient = await context.Clients.AsNoTracking().AnyAsync(c => c.ClientId == caseEntity.ClientId && c.UserId == userId, cancellationToken);
            var isManagingLawyerAdmin = false;
            if (!isAssignedLawyer && !isClient && userAccessor.IsInRole("LawyerAdmin"))
            {
                isManagingLawyerAdmin = await context.Lawyers
                    .AsNoTracking()
                    .AnyAsync(l => l.UserId == caseEntity.AssignedTo && l.RegisteredByUserId == userId, cancellationToken);
            }

            if (!isAssignedLawyer && !isClient && !isManagingLawyerAdmin)
                return Result<DocumentFileDto>.Failure("Forbidden", 403);

            var title = string.IsNullOrWhiteSpace(document.Title) ? "document" : document.Title.Trim();
            var extension = Path.GetExtension(document.FilePath);
            var downloadName = extension.Length > 0 ? $"{title}{extension}" : title;

            return Result<DocumentFileDto>.Success(new DocumentFileDto
            {
                FilePath = document.FilePath,
                DownloadName = downloadName
            });
        }
    }
}
