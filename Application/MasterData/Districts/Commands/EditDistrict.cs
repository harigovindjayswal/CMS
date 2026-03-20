using Application.Interfaces;
using Application.MasterData.Districts.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.MasterData.Districts.Commands;

public class EditDistrict
{
    public class Command : IRequest<Result<Unit>>
    {
        public required DistrictDto District { get; set; }
    }

    public class Handler(CmsContext context, IUserAccessor userAccessor) : IRequestHandler<Command, Result<Unit>>
    {
        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var entity = await context.Districts.FirstOrDefaultAsync(
                x => x.DistrictId == request.District.DistrictId,
                cancellationToken);

            if (entity == null) return Result<Unit>.Failure("District not found", 404);

            entity.StateId = request.District.StateId;
            entity.Name = request.District.Name.Trim();
            entity.IsActive = request.District.IsActive;
            entity.UpdatedBy = userAccessor.GetUserId();
            entity.UpdatedDate = DateTime.Now;

            var saved = await context.SaveChangesAsync(cancellationToken);
            if (saved == 0) return Result<Unit>.Failure("Failed to update district", 400);
            return Result<Unit>.Success(Unit.Value);
        }
    }
}

