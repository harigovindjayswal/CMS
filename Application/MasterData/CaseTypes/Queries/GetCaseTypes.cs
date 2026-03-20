using Application.MasterData.CaseTypes.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.MasterData.CaseTypes.Queries;

public class GetCaseTypes
{
    public class Query : IRequest<List<CaseTypeDto>> { }

    public class Handler(CmsContext context) : IRequestHandler<Query, List<CaseTypeDto>>
    {
        public async Task<List<CaseTypeDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await context.CaseTypes
                .AsNoTracking()
                .OrderBy(x => x.TypeName)
                .Select(x => new CaseTypeDto
                {
                    CaseTypeId = x.CaseTypeId,
                    TypeName = x.TypeName,
                    IsActive = x.IsActive
                })
                .ToListAsync(cancellationToken);
        }
    }
}

