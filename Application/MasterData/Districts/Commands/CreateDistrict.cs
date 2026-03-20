using Application.Interfaces;
using Application.MasterData.Districts.DTO;
using Domain.AppEntities;
using MediatR;
using Persistence.Context;

namespace Application.MasterData.Districts.Commands;

public class CreateDistrict
{
    public class Command : IRequest<int>
    {
        public required DistrictDto District { get; set; }
    }

    public class Handler(CmsContext context, IUserAccessor userAccessor) : IRequestHandler<Command, int>
    {
        public async Task<int> Handle(Command request, CancellationToken cancellationToken)
        {
            var entity = new DistrictMst
            {
                StateId = request.District.StateId,
                Name = request.District.Name.Trim(),
                IsActive = request.District.IsActive,
                CreatedBy = userAccessor.GetUserId(),
                CreatedDate = DateTime.Now
            };

            context.Districts.Add(entity);
            await context.SaveChangesAsync(cancellationToken);
            return entity.DistrictId;
        }
    }
}

