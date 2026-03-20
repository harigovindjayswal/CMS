using Application.Clients.DTO;
using Domain.AppEntities;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Clients.Queries
{
    public class GetAllClients
    {
        public class Query : IRequest<List<ClientDTO>> { }

        public class Handler(CmsContext context, IUserAccessor userAccessor) : IRequestHandler<Query, List<ClientDTO>>
        {
            public async Task<List<ClientDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                var userId = userAccessor.GetUserId();
                var query = context.Clients.AsNoTracking().AsQueryable();

                // Admin sees all; LawyerAdmin sees only their registrations.
                if (userAccessor.IsInRole("LawyerAdmin") && !string.IsNullOrWhiteSpace(userId))
                {
                    query = query.Where(x => x.RegisteredByUserId == userId);
                }

                return await query
       .Select(c => new ClientDTO
       {
           ClientId = c.ClientId,
           UserId = c.UserId,
           FirstName = c.FirstName,
           MiddleName = c.MiddleName,
           LastName = c.LastName,
           EmailId = c.EmailId,
           MobileNo = c.MobileNo,
           Address = c.Address,
           State = c.State,
           District = c.District,
           City = c.City,
           PinCode = c.PinCode,
           Notes = c.Notes,
           UpdatedBy = c.UpdatedBy,
           UpdatedDate = c.UpdatedDate,
           CreatedBy = c.CreatedBy,
           CreatedDate = c.CreatedDate,
           IsActive = c.IsActive

       })
       .ToListAsync(cancellationToken);
            }
        }
    }
}
