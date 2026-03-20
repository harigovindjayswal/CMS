using Application.Interfaces;
using Application.MasterData.States.DTO;
using Domain.AppEntities;
using MediatR;
using Persistence.Context;

namespace Application.MasterData.States.Commands;

public class CreateState
{
    public class Command : IRequest<int>
    {
        public required StateDto State { get; set; }
    }

    public class Handler(CmsContext context, IUserAccessor userAccessor) : IRequestHandler<Command, int>
    {
        public async Task<int> Handle(Command request, CancellationToken cancellationToken)
        {
            var entity = new StateMst
            {
                Name = request.State.Name.Trim(),
                IsActive = request.State.IsActive,
                CreatedBy = userAccessor.GetUserId(),
                CreatedDate = DateTime.Now
            };

            context.States.Add(entity);
            await context.SaveChangesAsync(cancellationToken);
            return entity.StateId;
        }
    }
}

