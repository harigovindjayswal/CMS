using Application.Cases.Commands;
using FluentValidation;

namespace Application.Cases.Validatators;

public class AddCaseDocumentValidator : AbstractValidator<AddCaseDocument.Command>
{
    public AddCaseDocumentValidator()
    {
        RuleFor(x => x.CaseId).GreaterThan(0);
        RuleFor(x => x.FilePath).NotEmpty().MaximumLength(1000);
        RuleFor(x => x.Title).MaximumLength(200);
        RuleFor(x => x.Category).MaximumLength(100);
    }
}

