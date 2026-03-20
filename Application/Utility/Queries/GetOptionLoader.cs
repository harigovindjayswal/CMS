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
        public int? ParentId { get; set; }
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
                    //.Where(r => r.Name == "Client" || r.Name == "Lawyer")
                    .Select(r => new OptionLoaderDto
                    {
                        Id = r.Id,
                        Name = r.Name!
                    })
                    .ToListAsync(cancellationToken),

                OptionType.State => await appContext.States
                    .AsNoTracking()
                    .Where(s => s.IsActive)
                    .OrderBy(s => s.Name)
                    .Select(s => new OptionLoaderDto
                    {
                        Id = s.StateId.ToString(),
                        Name = s.Name
                    })
                    .ToListAsync(cancellationToken),

                OptionType.District => request.ParentId.HasValue
                    ? await appContext.Districts
                        .AsNoTracking()
                        .Where(d => d.IsActive && d.StateId == request.ParentId.Value)
                        .OrderBy(d => d.Name)
                        .Select(d => new OptionLoaderDto
                        {
                            Id = d.DistrictId.ToString(),
                            Name = d.Name
                        })
                        .ToListAsync(cancellationToken)
                    : new List<OptionLoaderDto>(),

                OptionType.City => request.ParentId.HasValue
                    ? await appContext.Cities
                        .AsNoTracking()
                        .Where(c => c.IsActive && c.DistrictId == request.ParentId.Value)
                        .OrderBy(c => c.Name)
                        .Select(c => new OptionLoaderDto
                        {
                            Id = c.CityId.ToString(),
                            Name = c.Name
                        })
                        .ToListAsync(cancellationToken)
                    : new List<OptionLoaderDto>(),

                OptionType.CaseTypes => await appContext.CaseTypes
                    .AsNoTracking()
                    .Where(c => c.IsActive)
                    .OrderBy(c => c.TypeName)
                    .Select(c => new OptionLoaderDto
                    {
                        Id = c.CaseTypeId.ToString(),
                        Name = c.TypeName
                    })
                    .ToListAsync(cancellationToken),

                OptionType.CourtTypes => await appContext.CourtTypes
                    .AsNoTracking()
                    .Where(c => c.IsActive)
                    .OrderBy(c => c.TypeName)
                    .Select(c => new OptionLoaderDto
                    {
                        Id = c.CourtTypeId.ToString(),
                        Name = c.TypeName
                    })
                    .ToListAsync(cancellationToken),

                OptionType.Courts => request.ParentId.HasValue
                    ? await appContext.Courts
                        .AsNoTracking()
                        .Where(c => c.IsActive && c.CityId == request.ParentId.Value)
                        .OrderBy(c => c.Name)
                        .Select(c => new OptionLoaderDto
                        {
                            Id = c.CourtId.ToString(),
                            Name = c.Name
                        })
                        .ToListAsync(cancellationToken)
                    : new List<OptionLoaderDto>(),

                OptionType.Lawyers => request.ParentId.HasValue
                    ? await appContext.Lawyers
                        .AsNoTracking()
                        .Where(l => l.IsActive && l.CityId == request.ParentId.Value)
                        .OrderBy(l => l.FirstName)
                        .Select(l => new OptionLoaderDto
                        {
                            Id = l.LawyerId.ToString(),
                            Name = ((l.FirstName ?? "") + " " + (l.LastName ?? "")).Trim()
                        })
                        .ToListAsync(cancellationToken)
                    : new List<OptionLoaderDto>(),

                _ => new List<OptionLoaderDto>()
            };

            return Result<List<OptionLoaderDto>>.Success(result);
        }
    }
}
