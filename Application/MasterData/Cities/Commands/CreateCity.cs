using Application.Interfaces;
using Application.MasterData.Cities.DTO;
using Domain.AppEntities;
using MediatR;
using Persistence.Context;

namespace Application.MasterData.Cities.Commands;

public class CreateCity
{
    public class Command : IRequest<int>
    {
        public required CityDto City { get; set; }
    }

    public class Handler(CmsContext context, IUserAccessor userAccessor) : IRequestHandler<Command, int>
    {
        public async Task<int> Handle(Command request, CancellationToken cancellationToken)
        {
            var entity = new CityMst
            {
                DistrictId = request.City.DistrictId,
                Name = request.City.Name.Trim(),
                IsActive = request.City.IsActive,
                CreatedBy = userAccessor.GetUserId(),
                CreatedDate = DateTime.Now
            };

            context.Cities.Add(entity);
            await context.SaveChangesAsync(cancellationToken);
            return entity.CityId;
        }
    }
}

