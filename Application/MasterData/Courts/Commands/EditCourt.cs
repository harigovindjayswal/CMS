using Application.Interfaces;
using Application.MasterData.Courts.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.MasterData.Courts.Commands;

public class EditCourt
{
    public class Command : IRequest<Result<Unit>>
    {
        public required CourtDto Court { get; set; }
    }

    public class Handler(CmsContext context, IUserAccessor userAccessor) : IRequestHandler<Command, Result<Unit>>
    {
        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var entity = await context.Courts.FirstOrDefaultAsync(
                x => x.CourtId == request.Court.CourtId,
                cancellationToken);

            if (entity == null) return Result<Unit>.Failure("Court not found", 404);

            entity.CourtTypeId = request.Court.CourtTypeId;
            entity.CityId = request.Court.CityId;
            entity.Name = request.Court.Name.Trim();
            entity.IsActive = request.Court.IsActive;
            entity.UpdatedBy = userAccessor.GetUserId();
            entity.UpdatedDate = DateTime.Now;

            var saved = await context.SaveChangesAsync(cancellationToken);
            if (saved == 0) return Result<Unit>.Failure("Failed to update court", 400);
            return Result<Unit>.Success(Unit.Value);
        }
    }
}

