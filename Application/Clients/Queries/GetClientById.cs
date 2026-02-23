
using Application.Clients.DTO;
using Domain.AppEntities;
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
    public class GetClientById
    {
        public class Query : IRequest<Result<ClientDTO>>
        {
            public required int Id { get; set; }
        }

        public class Handler(CmsContext context) : IRequestHandler<Query, Result<ClientDTO>>
        {
            public async Task<Result<ClientDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                var client = await context.Clients
                .AsNoTracking()
                .Where(c => c.ClientId == request.Id)
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
                .FirstOrDefaultAsync(cancellationToken);

                if (client == null)
                    return Result<ClientDTO>.Failure("Client not found", 404);

                return Result<ClientDTO>.Success(client);
            }
        }
    }
}
