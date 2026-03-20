using Application.Interfaces;
using Application.MasterData.CaseTypes.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.MasterData.CaseTypes.Commands;

public class EditCaseType
{
    public class Command : IRequest<Result<Unit>>
    {
        public required CaseTypeDto CaseType { get; set; }
    }

    public class Handler(CmsContext context, IUserAccessor userAccessor) : IRequestHandler<Command, Result<Unit>>
    {
        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var entity = await context.CaseTypes.FirstOrDefaultAsync(
                x => x.CaseTypeId == request.CaseType.CaseTypeId,
                cancellationToken);

            if (entity == null) return Result<Unit>.Failure("Case type not found", 404);

            entity.TypeName = request.CaseType.TypeName.Trim();
            entity.IsActive = request.CaseType.IsActive;
            entity.UpdatedBy = userAccessor.GetUserId();
            entity.UpdatedDate = DateTime.Now;

            var saved = await context.SaveChangesAsync(cancellationToken);
            if (saved == 0) return Result<Unit>.Failure("Failed to update case type", 400);
            return Result<Unit>.Success(Unit.Value);
        }
    }
}

