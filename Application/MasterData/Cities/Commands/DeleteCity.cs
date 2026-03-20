using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.MasterData.Cities.Commands;

public class DeleteCity
{
    public class Command : IRequest<Result<Unit>>
    {
        public required int Id { get; set; }
    }

    public class Handler(CmsContext context) : IRequestHandler<Command, Result<Unit>>
    {
        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var entity = await context.Cities.FirstOrDefaultAsync(x => x.CityId == request.Id, cancellationToken);
            if (entity == null) return Result<Unit>.Failure("City not found", 404);

            context.Cities.Remove(entity);
            var saved = await context.SaveChangesAsync(cancellationToken);
            if (saved == 0) return Result<Unit>.Failure("Failed to delete city", 400);
            return Result<Unit>.Success(Unit.Value);
        }
    }
}

