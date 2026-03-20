using Application.MasterData.CaseTypes.Commands;
using FluentValidation;

namespace Application.MasterData.CaseTypes.Validators;

public class EditCaseTypeValidator : AbstractValidator<EditCaseType.Command>
{
    public EditCaseTypeValidator()
    {
        RuleFor(x => x.CaseType.CaseTypeId).GreaterThan(0);
        RuleFor(x => x.CaseType.TypeName).NotEmpty().MaximumLength(120);
    }
}

