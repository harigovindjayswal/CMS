using Application.Profile.Staff.Commands;
using FluentValidation;

namespace Application.Profile.Staff.Validators;

public class UpsertMyStaffProfileValidator : AbstractValidator<UpsertMyStaffProfile.Command>
{
    public UpsertMyStaffProfileValidator()
    {
        RuleFor(x => x.Profile).NotNull();
        RuleFor(x => x.Profile.EmailId).NotEmpty().EmailAddress();
        RuleFor(x => x.Profile.MobileNo).NotEmpty().MaximumLength(20);
        RuleFor(x => x.Profile.FirstName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Profile.LastName).NotEmpty().MaximumLength(50);
    }
}

