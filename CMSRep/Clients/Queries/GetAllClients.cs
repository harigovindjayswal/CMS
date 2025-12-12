using CMSDb.DbModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSApplication.Clients.Queries
{
    public class GetAllClients
    {
        public class Query : IRequest<List<Client>> { }

        public class Handler(CmsContext context) : IRequestHandler<Query, List<Client>>
        {
            public async Task<List<Client>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await context.Clients.ToListAsync(cancellationToken);
            }
        }
    }
}
