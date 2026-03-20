
using MediatR;
using Application.Interfaces;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Clients.Commands
{
    public class DeleteClient
    {
        public class Command : IRequest<Result<Unit>>
        {
            public required int Id { get; set; }
        }

        public class Handler(CmsContext context, IUserAccessor userAccessor) : IRequestHandler<Command, Result<Unit>>
        {
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var userId = userAccessor.GetUserId();
                if (string.IsNullOrWhiteSpace(userId))
                    return Result<Unit>.Failure("Unauthorised", 401);

                var client = await context.Clients
                    .FindAsync(request.Id, cancellationToken);
                if (client == null) return Result<Unit>.Failure("Client not found", 404);

                if (userAccessor.IsInRole("LawyerAdmin") && client.RegisteredByUserId != userId)
                    return Result<Unit>.Failure("Forbidden", 403);

                context.Remove(client);

                var res = await context.SaveChangesAsync(cancellationToken);
                if (res == 0) return Result<Unit>.Failure("Failed to delete client !", 400);
                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
