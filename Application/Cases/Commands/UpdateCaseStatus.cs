using Application.Cases.DTO;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.Cases.Commands;

public class UpdateCaseStatus
{
    public class Command : IRequest<Result<Unit>>
    {
        public required UpdateCaseStatusDto Status { get; set; }
    }

    public class Handler(CmsContext context, IUserAccessor userAccessor) : IRequestHandler<Command, Result<Unit>>
    {
        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var userId = userAccessor.GetUserId();
            if (string.IsNullOrWhiteSpace(userId))
                return Result<Unit>.Failure("Unauthorised", 401);

            var entity = await context.Cases.FirstOrDefaultAsync(x => x.CaseId == request.Status.CaseId, cancellationToken);
            if (entity == null) return Result<Unit>.Failure("Case not found", 404);

            if (!string.Equals(entity.AssignedTo, userId, StringComparison.OrdinalIgnoreCase))
                return Result<Unit>.Failure("Forbidden", 403);

            entity.Status = request.Status.Status.Trim();
            entity.Stage = request.Status.Stage.Trim();
            entity.UpdatedAt = DateTime.Now;

            var saved = await context.SaveChangesAsync(cancellationToken);
            if (saved == 0) return Result<Unit>.Failure("Failed to update case", 400);
            return Result<Unit>.Success(Unit.Value);
        }
    }
}

