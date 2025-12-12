using CMSDb.DbModels;
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
        public class Query : IRequest<Result<Client>>
        {
            public required int Id { get; set; }
        }

        public class Handler(CmsContext context) : IRequestHandler<Query, Result<Client>>
        {
            public async Task<Result<Client>> Handle(Query request, CancellationToken cancellationToken)
            {
                var client = await context.Clients.FindAsync(request.Id, cancellationToken);

                if (client == null) return Result<Client>.Failure("Client not found", 404);

                return Result<Client>.Success(client);
            }
        }
    }
}
