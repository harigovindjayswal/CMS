using Application.Interfaces;
using Application.Profile.Lawyers.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.Profile.Lawyers.Queries;

public class GetMyLawyerProfile
{
    public class Query : IRequest<Result<LawyerProfileDto>> { }

    public class Handler(CmsContext context, IUserAccessor userAccessor) : IRequestHandler<Query, Result<LawyerProfileDto>>
    {
        public async Task<Result<LawyerProfileDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var userId = userAccessor.GetUserId();
            if (string.IsNullOrWhiteSpace(userId))
                return Result<LawyerProfileDto>.Failure("Unauthorised", 401);

            var lawyer = await context.Lawyers
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);

            if (lawyer == null)
                return Result<LawyerProfileDto>.Failure("Lawyer profile not found", 404);

            var dto = new LawyerProfileDto
            {
                FirstName = lawyer.FirstName,
                MiddleName = lawyer.MiddleName,
                LastName = lawyer.LastName,
                DateOfBirth = lawyer.DateOfBirth,
                EmailId = lawyer.EmailId,
                MobileNo = lawyer.MobileNo,
                Address = lawyer.Address,
                StateId = lawyer.StateId,
                CityId = lawyer.CityId,
                BarLicenseNumber = lawyer.BarLicenseNumber,
                YearsOfExperience = lawyer.YearsOfExperience,
                CourtDetails = lawyer.CourtDetails
            };

            return Result<LawyerProfileDto>.Success(dto);
        }
    }
}

