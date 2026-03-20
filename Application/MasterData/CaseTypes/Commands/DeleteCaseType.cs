using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.MasterData.CaseTypes.Commands;

public class DeleteCaseType
{
    public class Command : IRequest<Result<Unit>>
    {
        public required int Id { get; set; }
    }

    public class Handler(CmsContext context) : IRequestHandler<Command, Result<Unit>>
    {
        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var entity = await context.CaseTypes.FirstOrDefaultAsync(x => x.CaseTypeId == request.Id, cancellationToken);
            if (entity == null) return Result<Unit>.Failure("Case type not found", 404);

            context.CaseTypes.Remove(entity);
            var saved = await context.SaveChangesAsync(cancellationToken);
            if (saved == 0) return Result<Unit>.Failure("Failed to delete case type", 400);
            return Result<Unit>.Success(Unit.Value);
        }
    }
}

