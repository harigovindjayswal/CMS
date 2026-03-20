using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Clients.DTO;
using Application.Interfaces;
using AutoMapper;
using Domain.AppEntities;
using FluentValidation;
using MediatR;
using Persistence.Context;

namespace Application.Clients.Commands
{
    public class CreateClient
    {
        public class Command : IRequest<int>
        {
            public required CreateClientDTO clientDto { get; set; }
        }

        public class Handler(CmsContext context, IMapper mapper, IUserAccessor userAccessor) : IRequestHandler<Command, int>
        {
            public async Task<int> Handle(Command request, CancellationToken cancellationToken)
            {
                var client = mapper.Map<Client>(request.clientDto);
                if (client == null) return 0;

                // This endpoint is role-protected at controller level; we still set audit fields safely.
                var userId = userAccessor.GetUserId();
                client.CreatedBy = userId;
                client.CreatedDate = DateTime.Now;
                client.IsActive = true;

                if (userAccessor.IsInRole("LawyerAdmin") && !string.IsNullOrWhiteSpace(userId))
                {
                    client.RegisteredByUserId = userId;
                }

                context.Clients.Add(client);
                await context.SaveChangesAsync(cancellationToken);
                return client.ClientId;
            }
        }
    }
}
