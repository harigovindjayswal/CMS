using Application.Interfaces;
using Application.MasterData.States.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.MasterData.States.Commands;

public class EditState
{
    public class Command : IRequest<Result<Unit>>
    {
        public required StateDto State { get; set; }
    }

    public class Handler(CmsContext context, IUserAccessor userAccessor) : IRequestHandler<Command, Result<Unit>>
    {
        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var entity = await context.States.FirstOrDefaultAsync(
                x => x.StateId == request.State.StateId,
                cancellationToken);

            if (entity == null) return Result<Unit>.Failure("State not found", 404);

            entity.Name = request.State.Name.Trim();
            entity.IsActive = request.State.IsActive;
            entity.UpdatedBy = userAccessor.GetUserId();
            entity.UpdatedDate = DateTime.Now;

            var saved = await context.SaveChangesAsync(cancellationToken);
            if (saved == 0) return Result<Unit>.Failure("Failed to update state", 400);
            return Result<Unit>.Success(Unit.Value);
        }
    }
}

