using Application.MasterData.States.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.MasterData.States.Queries;

public class GetStates
{
    public class Query : IRequest<List<StateDto>> { }

    public class Handler(CmsContext context) : IRequestHandler<Query, List<StateDto>>
    {
        public async Task<List<StateDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await context.States
                .AsNoTracking()
                .OrderBy(x => x.Name)
                .Select(x => new StateDto
                {
                    StateId = x.StateId,
                    Name = x.Name,
                    IsActive = x.IsActive
                })
                .ToListAsync(cancellationToken);
        }
    }
}

