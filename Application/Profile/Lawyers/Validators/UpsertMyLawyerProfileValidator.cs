using Application.Profile.Lawyers.Commands;
using FluentValidation;

namespace Application.Profile.Lawyers.Validators;

public class UpsertMyLawyerProfileValidator : AbstractValidator<UpsertMyLawyerProfile.Command>
{
    public UpsertMyLawyerProfileValidator()
    {
        RuleFor(x => x.Profile.FirstName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Profile.LastName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Profile.EmailId).NotEmpty().EmailAddress();
        RuleFor(x => x.Profile.MobileNo).NotEmpty().MaximumLength(20);
        RuleFor(x => x.Profile.BarLicenseNumber).NotEmpty().MaximumLength(80);
        RuleFor(x => x.Profile.YearsOfExperience).GreaterThanOrEqualTo(0).LessThanOrEqualTo(70);
    }
}

