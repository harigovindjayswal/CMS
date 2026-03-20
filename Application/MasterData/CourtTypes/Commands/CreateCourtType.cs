using Application.Interfaces;
using Application.MasterData.CourtTypes.DTO;
using Domain.AppEntities;
using MediatR;
using Persistence.Context;

namespace Application.MasterData.CourtTypes.Commands;

public class CreateCourtType
{
    public class Command : IRequest<int>
    {
        public required CourtTypeDto CourtType { get; set; }
    }

    public class Handler(CmsContext context, IUserAccessor userAccessor) : IRequestHandler<Command, int>
    {
        public async Task<int> Handle(Command request, CancellationToken cancellationToken)
        {
            var entity = new CourtTypeMst
            {
                TypeName = request.CourtType.TypeName.Trim(),
                IsActive = request.CourtType.IsActive,
                CreatedBy = userAccessor.GetUserId(),
                CreatedDate = DateTime.Now
            };

            context.CourtTypes.Add(entity);
            await context.SaveChangesAsync(cancellationToken);
            return entity.CourtTypeId;
        }
    }
}

