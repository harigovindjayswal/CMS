using Application.LawyerAdmin.Commands;
using FluentValidation;

namespace Application.LawyerAdmin.Validators;

public class CreateManagedLawyerValidator : AbstractValidator<CreateManagedLawyer.Command>
{
    public CreateManagedLawyerValidator()
    {
        RuleFor(x => x.Lawyer.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Lawyer.DisplayName).NotEmpty().MaximumLength(120);
        RuleFor(x => x.Lawyer.Password).NotEmpty().MinimumLength(6).MaximumLength(100);
        RuleFor(x => x.Lawyer.FirstName).MaximumLength(50);
        RuleFor(x => x.Lawyer.MiddleName).MaximumLength(50);
        RuleFor(x => x.Lawyer.LastName).MaximumLength(50);
        RuleFor(x => x.Lawyer.MobileNo).MaximumLength(20);
        RuleFor(x => x.Lawyer.BarLicenseNumber).MaximumLength(80);
        RuleFor(x => x.Lawyer.StateId).GreaterThan(0).When(x => x.Lawyer.StateId.HasValue);
        RuleFor(x => x.Lawyer.CityId).GreaterThan(0).When(x => x.Lawyer.CityId.HasValue);
    }
}

