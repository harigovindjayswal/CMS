using Application.Interfaces;
using Application.MasterData.Courts.DTO;
using Domain.AppEntities;
using MediatR;
using Persistence.Context;

namespace Application.MasterData.Courts.Commands;

public class CreateCourt
{
    public class Command : IRequest<int>
    {
        public required CourtDto Court { get; set; }
    }

    public class Handler(CmsContext context, IUserAccessor userAccessor) : IRequestHandler<Command, int>
    {
        public async Task<int> Handle(Command request, CancellationToken cancellationToken)
        {
            var entity = new CourtMst
            {
                CourtTypeId = request.Court.CourtTypeId,
                CityId = request.Court.CityId,
                Name = request.Court.Name.Trim(),
                IsActive = request.Court.IsActive,
                CreatedBy = userAccessor.GetUserId(),
                CreatedDate = DateTime.Now
            };

            context.Courts.Add(entity);
            await context.SaveChangesAsync(cancellationToken);
            return entity.CourtId;
        }
    }
}

