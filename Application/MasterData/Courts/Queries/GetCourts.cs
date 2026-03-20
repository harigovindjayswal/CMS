using Application.MasterData.Courts.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.MasterData.Courts.Queries;

public class GetCourts
{
    public class Query : IRequest<List<CourtDto>>
    {
        public int? CityId { get; set; }
    }

    public class Handler(CmsContext context) : IRequestHandler<Query, List<CourtDto>>
    {
        public async Task<List<CourtDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var query = context.Courts.AsNoTracking();

            if (request.CityId.HasValue)
                query = query.Where(x => x.CityId == request.CityId.Value);

            return await query
                .OrderBy(x => x.Name)
                .Select(x => new CourtDto
                {
                    CourtId = x.CourtId,
                    CourtTypeId = x.CourtTypeId,
                    CityId = x.CityId,
                    Name = x.Name,
                    IsActive = x.IsActive
                })
                .ToListAsync(cancellationToken);
        }
    }
}

