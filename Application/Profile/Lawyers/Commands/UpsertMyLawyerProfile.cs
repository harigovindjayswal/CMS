using Application.Interfaces;
using Application.Profile.Lawyers.DTO;
using Domain.AppEntities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.Profile.Lawyers.Commands;

public class UpsertMyLawyerProfile
{
    public class Command : IRequest<Result<Unit>>
    {
        public required LawyerProfileDto Profile { get; set; }
    }

    public class Handler(CmsContext context, IUserAccessor userAccessor) : IRequestHandler<Command, Result<Unit>>
    {
        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var userId = userAccessor.GetUserId();
            if (string.IsNullOrWhiteSpace(userId))
                return Result<Unit>.Failure("Unauthorised", 401);

            var lawyer = await context.Lawyers.FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);

            if (lawyer == null)
            {
                lawyer = new Lawyer
                {
                    UserId = userId,
                    CreatedBy = userId,
                    CreatedDate = DateTime.Now,
                    IsActive = true
                };
                context.Lawyers.Add(lawyer);
            }
            else
            {
                lawyer.UpdatedBy = userId;
                lawyer.UpdatedDate = DateTime.Now;
            }

            lawyer.FirstName = request.Profile.FirstName?.Trim();
            lawyer.MiddleName = request.Profile.MiddleName?.Trim();
            lawyer.LastName = request.Profile.LastName?.Trim();
            lawyer.DateOfBirth = request.Profile.DateOfBirth;
            lawyer.EmailId = request.Profile.EmailId?.Trim();
            lawyer.MobileNo = request.Profile.MobileNo?.Trim();
            lawyer.Address = request.Profile.Address?.Trim();
            lawyer.StateId = request.Profile.StateId;
            lawyer.CityId = request.Profile.CityId;
            lawyer.BarLicenseNumber = request.Profile.BarLicenseNumber?.Trim();
            lawyer.YearsOfExperience = request.Profile.YearsOfExperience;
            lawyer.CourtDetails = request.Profile.CourtDetails?.Trim();

            var saved = await context.SaveChangesAsync(cancellationToken);
            if (saved == 0) return Result<Unit>.Failure("Failed to save profile", 400);

            return Result<Unit>.Success(Unit.Value);
        }
    }
}

