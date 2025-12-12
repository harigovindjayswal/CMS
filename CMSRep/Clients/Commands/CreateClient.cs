using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CMSApplication.Clients.DTO;
using CMSDb.DbModels;
using FluentValidation;
using MediatR;

namespace CMSApplication.Clients.Commands
{
    public class CreateClient
    {
        public class Command : IRequest<int>
        {
            public required CreateClientDTO clientDto { get; set; }
        }

        public class Handler(CmsContext context,IMapper _mapper) : IRequestHandler<Command, int>
        {
            public async Task<int> Handle(Command request, CancellationToken cancellationToken)
            {
                var client = _mapper.Map<Client>(request.clientDto);
                context.Clients.Add(client);
                await context.SaveChangesAsync(cancellationToken);
                return client.ClientId;
            }
        }
    }
}
