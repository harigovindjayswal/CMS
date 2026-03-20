using Application.MasterData.CourtTypes.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.MasterData.CourtTypes.Queries;

public class GetCourtTypes
{
    public class Query : IRequest<List<CourtTypeDto>> { }

    public class Handler(CmsContext context) : IRequestHandler<Query, List<CourtTypeDto>>
    {
        public async Task<List<CourtTypeDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await context.CourtTypes
                .AsNoTracking()
                .OrderBy(x => x.TypeName)
                .Select(x => new CourtTypeDto
                {
                    CourtTypeId = x.CourtTypeId,
                    TypeName = x.TypeName,
                    IsActive = x.IsActive
                })
                .ToListAsync(cancellationToken);
        }
    }
}

