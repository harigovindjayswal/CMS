using Application.LawyerAdmin.Commands;
using FluentValidation;

namespace Application.LawyerAdmin.Validators;

public class CreateManagedClientValidator : AbstractValidator<CreateManagedClient.Command>
{
    public CreateManagedClientValidator()
    {
        RuleFor(x => x.Client.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Client.DisplayName).NotEmpty().MaximumLength(120);
        RuleFor(x => x.Client.Password).NotEmpty().MinimumLength(6).MaximumLength(100);
        RuleFor(x => x.Client.FirstName).MaximumLength(50);
        RuleFor(x => x.Client.MiddleName).MaximumLength(50);
        RuleFor(x => x.Client.LastName).MaximumLength(50);
        RuleFor(x => x.Client.MobileNo).MaximumLength(20);
    }
}

