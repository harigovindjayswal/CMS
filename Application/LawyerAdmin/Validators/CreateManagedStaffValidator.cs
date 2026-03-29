using Application.LawyerAdmin.Commands;
using FluentValidation;

namespace Application.LawyerAdmin.Validators;

public class CreateManagedStaffValidator : AbstractValidator<CreateManagedStaff.Command>
{
    public CreateManagedStaffValidator()
    {
        RuleFor(x => x.Staff).NotNull();
        RuleFor(x => x.Staff.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Staff.DisplayName).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Staff.Password).NotEmpty().MinimumLength(6);
        RuleFor(x => x.Staff.LawyerId).GreaterThan(0);
    }
}

