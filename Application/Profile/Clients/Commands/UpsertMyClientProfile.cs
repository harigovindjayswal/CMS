using Application.Interfaces;
using Application.Profile.Clients.DTO;
using Domain.AppEntities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.Profile.Clients.Commands;

public class UpsertMyClientProfile
{
    public class Command : IRequest<Result<Unit>>
    {
        public required ClientProfileDto Profile { get; set; }
    }

    public class Handler(CmsContext context, IUserAccessor userAccessor) : IRequestHandler<Command, Result<Unit>>
    {
        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var userId = userAccessor.GetUserId();
            if (string.IsNullOrWhiteSpace(userId))
                return Result<Unit>.Failure("Unauthorised", 401);

            var client = await context.Clients.FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);

            if (client == null)
            {
                client = new Client
                {
                    UserId = userId,
                    CreatedBy = userId,
                    CreatedDate = DateTime.Now,
                    IsActive = true
                };
                context.Clients.Add(client);
            }
            else
            {
                client.UpdatedBy = userId;
                client.UpdatedDate = DateTime.Now;
            }

            client.FirstName = request.Profile.FirstName?.Trim();
            client.MiddleName = request.Profile.MiddleName?.Trim();
            client.LastName = request.Profile.LastName?.Trim();
            client.EmailId = request.Profile.EmailId?.Trim();
            client.MobileNo = request.Profile.MobileNo?.Trim();
            client.Address = request.Profile.Address?.Trim();
            client.State = request.Profile.State;
            client.District = request.Profile.District;
            client.City = request.Profile.City?.Trim();
            client.PinCode = request.Profile.PinCode?.Trim();
            client.Notes = request.Profile.Notes?.Trim();

            var saved = await context.SaveChangesAsync(cancellationToken);
            if (saved == 0) return Result<Unit>.Failure("Failed to save profile", 400);

            return Result<Unit>.Success(Unit.Value);
        }
    }
}

