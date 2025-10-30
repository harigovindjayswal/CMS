using AutoMapper;
using CMSDb.DbModels;
using CMSRep.DbModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSApplication.Clients.Commands
{
    public class CreateClient
    {
        public class Command : IRequest<int>
        {
            public required ClientDTO clientDto { get; set; }
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
