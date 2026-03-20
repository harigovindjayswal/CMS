using Application.MasterData.CaseTypes.Commands;
using FluentValidation;

namespace Application.MasterData.CaseTypes.Validators;

public class CreateCaseTypeValidator : AbstractValidator<CreateCaseType.Command>
{
    public CreateCaseTypeValidator()
    {
        RuleFor(x => x.CaseType.TypeName).NotEmpty().MaximumLength(120);
    }
}

