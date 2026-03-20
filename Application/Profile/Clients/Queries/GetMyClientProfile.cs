using Application.Interfaces;
using Application.Profile.Clients.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.Profile.Clients.Queries;

public class GetMyClientProfile
{
    public class Query : IRequest<Result<ClientProfileDto>> { }

    public class Handler(CmsContext context, IUserAccessor userAccessor) : IRequestHandler<Query, Result<ClientProfileDto>>
    {
        public async Task<Result<ClientProfileDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var userId = userAccessor.GetUserId();
            if (string.IsNullOrWhiteSpace(userId))
                return Result<ClientProfileDto>.Failure("Unauthorised", 401);

            var client = await context.Clients
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);

            if (client == null)
                return Result<ClientProfileDto>.Failure("Client profile not found", 404);

            var dto = new ClientProfileDto
            {
                FirstName = client.FirstName,
                MiddleName = client.MiddleName,
                LastName = client.LastName,
                EmailId = client.EmailId,
                MobileNo = client.MobileNo,
                Address = client.Address,
                State = client.State,
                District = client.District,
                City = client.City,
                PinCode = client.PinCode,
                Notes = client.Notes
            };

            return Result<ClientProfileDto>.Success(dto);
        }
    }
}

