using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.MasterData.CourtTypes.Commands;

public class DeleteCourtType
{
    public class Command : IRequest<Result<Unit>>
    {
        public required int Id { get; set; }
    }

    public class Handler(CmsContext context) : IRequestHandler<Command, Result<Unit>>
    {
        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var entity = await context.CourtTypes.FirstOrDefaultAsync(x => x.CourtTypeId == request.Id, cancellationToken);
            if (entity == null) return Result<Unit>.Failure("Court type not found", 404);

            context.CourtTypes.Remove(entity);
            var saved = await context.SaveChangesAsync(cancellationToken);
            if (saved == 0) return Result<Unit>.Failure("Failed to delete court type", 400);
            return Result<Unit>.Success(Unit.Value);
        }
    }
}

