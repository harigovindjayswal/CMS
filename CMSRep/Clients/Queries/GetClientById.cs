using CMSDb.DbModels;
using CMSRep.DbModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSApplication.Clients.Queries
{
    public class GetClientById
    {
        public class Query : IRequest<Client>
        {
            public required int Id { get; set; }
        }

        public class Handler(CmsContext context) : IRequestHandler<Query, Client>
        {
            public async Task<Client> Handle(Query request, CancellationToken cancellationToken)
            {
                var client = await context.Clients.FindAsync([request.Id], cancellationToken);

                if (client == null) throw new Exception("Client not found");

                return client;
            }
        }
    }
}
