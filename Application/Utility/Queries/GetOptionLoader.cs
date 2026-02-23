using System;
using Application.Common.Enums;
using Application.Utility.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Identity;

namespace Application.Utility.Queries;

public class GetOtpionLoader
{
    public class Query : IRequest<Result<List<OptionLoaderDto>>>
    {
        public OptionType Type { get; set; }
    }

    public class Handler(
        CmsContext appContext,
        CmsIdentityContext identityContext)
        : IRequestHandler<Query, Result<List<OptionLoaderDto>>>
    {
        public async Task<Result<List<OptionLoaderDto>>> Handle(
            Query request,
            CancellationToken cancellationToken)
        {
            List<OptionLoaderDto> result = request.Type switch
            {
                OptionType.Role => await identityContext.Roles
                    .AsNoTracking()
                    .Where(r => r.Name == "Client" || r.Name == "Lawyer")
                    .Select(r => new OptionLoaderDto
                    {
                        Id = r.Id,
                        Name = r.Name!
                    })
                    .ToListAsync(cancellationToken),

                // OptionType.State => await appContext.states
                //     .AsNoTracking()
                //     .Select(s => new OptionLoaderDto
                //     {
                //         Id = s.Id.ToString(),
                //         Name = s.Name
                //     })
                //     .ToListAsync(cancellationToken),

                // OptionType.CaseTypes => await appContext.CaseTypes
                //     .AsNoTracking()
                //     .Select(c => new OptionLoaderDto
                //     {
                //         Id = c.Id.ToString(),
                //         Name = c.TypeName
                //     })
                //     .ToListAsync(cancellationToken),

                // _ => new List<OptionLoaderDto>()
            };

            return Result<List<OptionLoaderDto>>.Success(result);
        }
    }
}