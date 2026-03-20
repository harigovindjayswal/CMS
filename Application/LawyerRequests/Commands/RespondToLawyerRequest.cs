using Application.Interfaces;
using Application.LawyerRequests.DTO;
using Domain.AppEntities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.LawyerRequests.Commands;

public class RespondToLawyerRequest
{
    public class Command : IRequest<Result<Unit>>
    {
        public required RespondLawyerRequestDto Response { get; set; }
    }

    public class Handler(CmsContext context, IUserAccessor userAccessor) : IRequestHandler<Command, Result<Unit>>
    {
        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var userId = userAccessor.GetUserId();
            if (string.IsNullOrWhiteSpace(userId))
                return Result<Unit>.Failure("Unauthorised", 401);

            var lawyer = await context.Lawyers
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserId == userId && x.IsActive, cancellationToken);

            if (lawyer == null)
                return Result<Unit>.Failure("Lawyer profile not found", 400);

            var entity = await context.LawyerRequests
                .FirstOrDefaultAsync(x => x.LawyerRequestId == request.Response.LawyerRequestId, cancellationToken);

            if (entity == null) return Result<Unit>.Failure("Request not found", 404);

            if (entity.LawyerId != lawyer.LawyerId)
                return Result<Unit>.Failure("Forbidden", 403);

            if (request.Response.Status != LawyerRequestStatus.Accepted &&
                request.Response.Status != LawyerRequestStatus.Rejected)
                return Result<Unit>.Failure("Invalid status transition", 400);

            entity.Status = request.Response.Status;
            entity.LawyerRemark = request.Response.LawyerRemark.Trim();
            entity.UpdatedBy = userId;
            entity.UpdatedDate = DateTime.Now;

            var saved = await context.SaveChangesAsync(cancellationToken);
            if (saved == 0) return Result<Unit>.Failure("Failed to update request", 400);

            return Result<Unit>.Success(Unit.Value);
        }
    }
}

