using Application.Profile.Clients.Commands;
using FluentValidation;

namespace Application.Profile.Clients.Validators;

public class UpsertMyClientProfileValidator : AbstractValidator<UpsertMyClientProfile.Command>
{
    public UpsertMyClientProfileValidator()
    {
        RuleFor(x => x.Profile.FirstName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Profile.LastName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Profile.EmailId).NotEmpty().EmailAddress();
        RuleFor(x => x.Profile.MobileNo).NotEmpty().MaximumLength(20);
        RuleFor(x => x.Profile.Address).NotEmpty().MaximumLength(255);
        RuleFor(x => x.Profile.PinCode).MaximumLength(6);
    }
}

