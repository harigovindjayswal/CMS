using Application.Interfaces;
using Application.LawyerRequests.DTO;
using Domain.AppEntities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.LawyerRequests.Commands;

public class CreateLawyerRequest
{
    public class Command : IRequest<Result<int>>
    {
        public required CreateLawyerRequestDto Request { get; set; }
    }

    public class Handler(CmsContext context, IUserAccessor userAccessor) : IRequestHandler<Command, Result<int>>
    {
        public async Task<Result<int>> Handle(Command request, CancellationToken cancellationToken)
        {
            var userId = userAccessor.GetUserId();
            if (string.IsNullOrWhiteSpace(userId))
                return Result<int>.Failure("Unauthorised", 401);

            var client = await context.Clients
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);

            if (client == null)
                return Result<int>.Failure("Client profile not found. Please complete profile first.", 400);

            var lawyerExists = await context.Lawyers
                .AsNoTracking()
                .AnyAsync(x => x.LawyerId == request.Request.LawyerId && x.IsActive, cancellationToken);

            if (!lawyerExists)
                return Result<int>.Failure("Selected lawyer not found", 400);

            var caseTypeExists = await context.CaseTypes
                .AsNoTracking()
                .AnyAsync(x => x.CaseTypeId == request.Request.CaseTypeId && x.IsActive, cancellationToken);

            if (!caseTypeExists)
                return Result<int>.Failure("Invalid case type selected", 400);

            var entity = new LawyerRequest
            {
                ClientId = client.ClientId,
                LawyerId = request.Request.LawyerId,
                CaseTypeId = request.Request.CaseTypeId,
                StateId = request.Request.StateId,
                DistrictId = request.Request.DistrictId,
                CityId = request.Request.CityId,
                CaseDescription = request.Request.CaseDescription.Trim(),
                Status = LawyerRequestStatus.Pending,
                CreatedBy = userId,
                CreatedDate = DateTime.Now,
                IsActive = true
            };

            context.LawyerRequests.Add(entity);
            await context.SaveChangesAsync(cancellationToken);

            return Result<int>.Success(entity.LawyerRequestId);
        }
    }
}

