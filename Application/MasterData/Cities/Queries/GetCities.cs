using Application.MasterData.Cities.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.MasterData.Cities.Queries;

public class GetCities
{
    public class Query : IRequest<List<CityDto>>
    {
        public int? DistrictId { get; set; }
    }

    public class Handler(CmsContext context) : IRequestHandler<Query, List<CityDto>>
    {
        public async Task<List<CityDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var query = context.Cities.AsNoTracking();

            if (request.DistrictId.HasValue)
                query = query.Where(x => x.DistrictId == request.DistrictId.Value);

            return await query
                .OrderBy(x => x.Name)
                .Select(x => new CityDto
                {
                    CityId = x.CityId,
                    DistrictId = x.DistrictId,
                    Name = x.Name,
                    IsActive = x.IsActive
                })
                .ToListAsync(cancellationToken);
        }
    }
}

