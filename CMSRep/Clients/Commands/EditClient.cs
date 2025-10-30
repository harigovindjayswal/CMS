using AutoMapper;
using CMSDb.DbModels;
using CMSRep.DbModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSApplication.Clients.Commands
{
    public class EditClient
    {
        public class Command : IRequest<int>
        {
            public required ClientDTO client { get; set; }
        }

        public class Handler(CmsContext context, IMapper mapper) : IRequestHandler<Command, int>
        {
            public async Task<int> Handle(Command request, CancellationToken cancellationToken)
            {
                var client = mapper.Map<Client>(request.client);
                context.Clients.Update(client);
                return await context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
