using Application.Clients.DTO;
using AutoMapper;
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

namespace Application.Clients.Commands
{
    public class EditClient
    {
        public class Command : IRequest<Result<Unit>>
        {
            public required EditClientDTO client { get; set; }
        }

        public class Handler(CmsContext context, IMapper mapper) : IRequestHandler<Command, Result<Unit>>
        {
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var client = mapper.Map<Client>(request.client);
                if (client == null)
                {
                    return Result<Unit>.Failure("Client Not Found !", 404);
                }
                client.UpdatedBy="defaultUser";
                client.UpdatedDate=DateTime.Now;
                context.Clients.Update(client);
                var res = await context.SaveChangesAsync(cancellationToken);
                if (res == 0) return Result<Unit>.Failure("Failed to update client !", 400);
                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
