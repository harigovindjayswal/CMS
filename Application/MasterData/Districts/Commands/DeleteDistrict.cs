using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.MasterData.Districts.Commands;

public class DeleteDistrict
{
    public class Command : IRequest<Result<Unit>>
    {
        public required int Id { get; set; }
    }

    public class Handler(CmsContext context) : IRequestHandler<Command, Result<Unit>>
    {
        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var entity = await context.Districts.FirstOrDefaultAsync(x => x.DistrictId == request.Id, cancellationToken);
            if (entity == null) return Result<Unit>.Failure("District not found", 404);

            context.Districts.Remove(entity);
            var saved = await context.SaveChangesAsync(cancellationToken);
            if (saved == 0) return Result<Unit>.Failure("Failed to delete district", 400);
            return Result<Unit>.Success(Unit.Value);
        }
    }
}

