using Application.Interfaces;
using Application.MasterData.Cities.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.MasterData.Cities.Commands;

public class EditCity
{
    public class Command : IRequest<Result<Unit>>
    {
        public required CityDto City { get; set; }
    }

    public class Handler(CmsContext context, IUserAccessor userAccessor) : IRequestHandler<Command, Result<Unit>>
    {
        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var entity = await context.Cities.FirstOrDefaultAsync(
                x => x.CityId == request.City.CityId,
                cancellationToken);

            if (entity == null) return Result<Unit>.Failure("City not found", 404);

            entity.DistrictId = request.City.DistrictId;
            entity.Name = request.City.Name.Trim();
            entity.IsActive = request.City.IsActive;
            entity.UpdatedBy = userAccessor.GetUserId();
            entity.UpdatedDate = DateTime.Now;

            var saved = await context.SaveChangesAsync(cancellationToken);
            if (saved == 0) return Result<Unit>.Failure("Failed to update city", 400);
            return Result<Unit>.Success(Unit.Value);
        }
    }
}

