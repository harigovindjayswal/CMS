using Application.Clients.DTO;
using Application.Interfaces;
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

        public class Handler(CmsContext context, IUserAccessor userAccessor) : IRequestHandler<Command, Result<Unit>>
        {
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var userId = userAccessor.GetUserId();
                if (string.IsNullOrWhiteSpace(userId))
                    return Result<Unit>.Failure("Unauthorised", 401);

                var id = request.client.ClientId ?? 0;
                var entity = await context.Clients.FirstOrDefaultAsync(x => x.ClientId == id, cancellationToken);
                if (entity == null)
                    return Result<Unit>.Failure("Client not found", 404);

                if (userAccessor.IsInRole("LawyerAdmin") && entity.RegisteredByUserId != userId)
                    return Result<Unit>.Failure("Forbidden", 403);

                entity.FirstName = request.client.FirstName?.Trim();
                entity.MiddleName = request.client.MiddleName?.Trim();
                entity.LastName = request.client.LastName?.Trim();
                entity.EmailId = request.client.EmailId?.Trim();
                entity.MobileNo = request.client.MobileNo?.Trim();
                entity.Address = request.client.Address?.Trim();
                entity.State = request.client.State;
                entity.District = request.client.District;
                entity.City = request.client.City?.Trim();
                entity.PinCode = request.client.PinCode?.Trim();
                entity.Notes = request.client.Notes?.Trim();
                entity.IsActive = request.client.IsActive;
                entity.UpdatedBy = userId;
                entity.UpdatedDate = DateTime.Now;

                var res = await context.SaveChangesAsync(cancellationToken);
                if (res == 0) return Result<Unit>.Failure("Failed to update client !", 400);
                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
