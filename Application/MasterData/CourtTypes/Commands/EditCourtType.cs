using Application.Interfaces;
using Application.MasterData.CourtTypes.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.MasterData.CourtTypes.Commands;

public class EditCourtType
{
    public class Command : IRequest<Result<Unit>>
    {
        public required CourtTypeDto CourtType { get; set; }
    }

    public class Handler(CmsContext context, IUserAccessor userAccessor) : IRequestHandler<Command, Result<Unit>>
    {
        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var entity = await context.CourtTypes.FirstOrDefaultAsync(
                x => x.CourtTypeId == request.CourtType.CourtTypeId,
                cancellationToken);

            if (entity == null) return Result<Unit>.Failure("Court type not found", 404);

            entity.TypeName = request.CourtType.TypeName.Trim();
            entity.IsActive = request.CourtType.IsActive;
            entity.UpdatedBy = userAccessor.GetUserId();
            entity.UpdatedDate = DateTime.Now;

            var saved = await context.SaveChangesAsync(cancellationToken);
            if (saved == 0) return Result<Unit>.Failure("Failed to update court type", 400);
            return Result<Unit>.Success(Unit.Value);
        }
    }
}

