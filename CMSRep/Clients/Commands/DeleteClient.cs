using CMSDb.DbModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSApplication.Clients.Commands
{
    public class DeleteClient
    {
        public class Command : IRequest<int>
        {
            public required int Id { get; set; }
        }

        public class Handler(CmsContext context) : IRequestHandler<Command,int>
        {
            public async Task<int> Handle(Command request, CancellationToken cancellationToken)
            {
                var client = await context.Clients
                    .FindAsync([request.Id], cancellationToken)
                        ?? throw new Exception("Cannot find activity");

                context.Remove(client);

               return await context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
