using Application.MasterData.Districts.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.MasterData.Districts.Queries;

public class GetDistricts
{
    public class Query : IRequest<List<DistrictDto>>
    {
        public int? StateId { get; set; }
    }

    public class Handler(CmsContext context) : IRequestHandler<Query, List<DistrictDto>>
    {
        public async Task<List<DistrictDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var query = context.Districts.AsNoTracking();

            if (request.StateId.HasValue)
                query = query.Where(x => x.StateId == request.StateId.Value);

            return await query
                .OrderBy(x => x.Name)
                .Select(x => new DistrictDto
                {
                    DistrictId = x.DistrictId,
                    StateId = x.StateId,
                    Name = x.Name,
                    IsActive = x.IsActive
                })
                .ToListAsync(cancellationToken);
        }
    }
}

